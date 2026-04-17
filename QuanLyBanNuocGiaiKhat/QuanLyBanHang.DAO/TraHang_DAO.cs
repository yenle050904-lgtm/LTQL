using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class TraHang_DAO
    {
        /// <summary>
        /// Lấy chi tiết hóa đơn (dùng cho form trả hàng chọn SP muốn trả).
        /// </summary>
        public DataTable LayChiTietHoaDon(int maHD)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ct.SanPhamID AS [Mã SP],
                                      sp.TenSanPham AS [Tên SP],
                                      ct.SoLuong AS [Đã Mua],
                                      ct.DonGia AS [Đơn Giá],
                                      ISNULL(daTra.TongTra, 0) AS [Đã Trả],
                                      (ct.SoLuong - ISNULL(daTra.TongTra, 0)) AS [Còn Lại]
                               FROM HoaDon_ChiTiets ct
                               INNER JOIN SanPhams sp ON ct.SanPhamID = sp.ID
                               LEFT JOIN (
                                   SELECT pth.HoaDonID, ptct.SanPhamID, SUM(ptct.SoLuongTra) AS TongTra
                                   FROM PhieuTraHangs pth
                                   INNER JOIN PhieuTraHang_ChiTiets ptct ON pth.ID = ptct.PhieuTraHangID
                                   WHERE pth.HoaDonID = @MaHD
                                   GROUP BY pth.HoaDonID, ptct.SanPhamID
                               ) daTra ON daTra.HoaDonID = ct.HoaDonID AND daTra.SanPhamID = ct.SanPhamID
                               WHERE ct.HoaDonID = @MaHD";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaHD", maHD));
            }
        }

        /// <summary>
        /// Kiểm tra hóa đơn có tồn tại không. Trả về DataRow hoặc null.
        /// </summary>
        public DataRow KiemTraHoaDon(int maHD)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT hd.ID, hd.NgayLap, hd.TongTien, 
                                      ISNULL(kh.HoTen, N'Khách vãng lai') AS TenKhachHang
                               FROM HoaDons hd
                               LEFT JOIN KhachHangs kh ON hd.KhachHangID = kh.ID
                               WHERE hd.ID = @MaHD";
                DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaHD", maHD));
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        /// <summary>
        /// Lưu phiếu trả hàng + chi tiết + cộng lại tồn kho (transaction).
        /// dtChiTietTra: cột MaSP, TenSP, SoLuongTra, DonGia, ThanhTien
        /// </summary>
        public int LuuPhieuTraHang_Transaction(int hoaDonID, int nhanVienID, string lyDo, 
                                                decimal tongTienHoan, DataTable dtChiTietTra)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // 1. Tạo phiếu trả hàng
                    string sqlPhieu = @"
                        INSERT INTO PhieuTraHangs (NgayTra, HoaDonID, NhanVienID, LyDo, TongTienHoan)
                        VALUES (GETDATE(), @HoaDonID, @NVID, @LyDo, @TongHoan);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPhieu = new SqlCommand(sqlPhieu, conn, trans);
                    cmdPhieu.Parameters.AddWithValue("@HoaDonID", hoaDonID);
                    cmdPhieu.Parameters.AddWithValue("@NVID", nhanVienID);
                    cmdPhieu.Parameters.AddWithValue("@LyDo", (object)lyDo ?? DBNull.Value);
                    cmdPhieu.Parameters.AddWithValue("@TongHoan", tongTienHoan);

                    int maPhieuTra = Convert.ToInt32(cmdPhieu.ExecuteScalar());

                    // 2. Chi tiết + cộng lại kho
                    foreach (DataRow row in dtChiTietTra.Rows)
                    {
                        int maSP = Convert.ToInt32(row["MaSP"]);
                        int slTra = Convert.ToInt32(row["SoLuongTra"]);
                        decimal donGia = Convert.ToDecimal(row["DonGia"]);
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                        if (slTra <= 0) continue;  // Bỏ qua dòng không trả

                        // Ghi chi tiết
                        string sqlCT = @"INSERT INTO PhieuTraHang_ChiTiets 
                                         (PhieuTraHangID, SanPhamID, SoLuongTra, DonGia, ThanhTien)
                                         VALUES (@PTID, @SPID, @SL, @DG, @TT)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                        cmdCT.Parameters.AddWithValue("@PTID", maPhieuTra);
                        cmdCT.Parameters.AddWithValue("@SPID", maSP);
                        cmdCT.Parameters.AddWithValue("@SL", slTra);
                        cmdCT.Parameters.AddWithValue("@DG", donGia);
                        cmdCT.Parameters.AddWithValue("@TT", thanhTien);
                        cmdCT.ExecuteNonQuery();

                        // Cộng lại tồn kho
                        string sqlKho = "UPDATE SanPhams SET SoLuongTon = SoLuongTon + @SL WHERE ID = @SPID";
                        SqlCommand cmdKho = new SqlCommand(sqlKho, conn, trans);
                        cmdKho.Parameters.AddWithValue("@SL", slTra);
                        cmdKho.Parameters.AddWithValue("@SPID", maSP);
                        cmdKho.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return maPhieuTra;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Lỗi khi lưu phiếu trả hàng: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Lấy lịch sử các phiếu trả hàng.
        /// </summary>
        public DataTable LayLichSuTraHang(DateTime tuNgay, DateTime denNgay)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT pth.ID AS [Mã Phiếu Trả],
                                      pth.NgayTra AS [Ngày Trả],
                                      pth.HoaDonID AS [Mã HĐ Gốc],
                                      nv.HoVaTen AS [Nhân Viên],
                                      pth.LyDo AS [Lý Do],
                                      pth.TongTienHoan AS [Tổng Hoàn]
                               FROM PhieuTraHangs pth
                               LEFT JOIN NhanViens nv ON pth.NhanVienID = nv.ID
                               WHERE pth.NgayTra >= @TuNgay AND pth.NgayTra <= @DenNgay
                               ORDER BY pth.NgayTra DESC";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay));
            }
        }
    }
}
