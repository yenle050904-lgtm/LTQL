using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class BanHang_DAO
    {
        /// <summary>
        /// Lưu hóa đơn + chi tiết + trừ tồn kho trong một transaction.
        /// Có kiểm tra tồn kho: nếu không đủ thì rollback + báo lỗi.
        /// Trả về maHoaDon của hóa đơn vừa tạo (> 0 là thành công, -1 là lỗi).
        /// dtGioHang phải có cột: MaSP (int), TenSP (string), SoLuong (int), DonGia (decimal), ThanhTien (decimal).
        /// </summary>
        public int LuuHoaDon_Transaction(int maNhanVien, int maKhachHang, DataTable dtGioHang, decimal tongTien)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // 1. Tạo hóa đơn
                    string sqlHoaDon = @"
                        INSERT INTO HoaDons (NgayLap, TongTien, NhanVienID, KhachHangID)
                        VALUES (GETDATE(), @TongTien, @MaNV, @MaKH);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdHD = new SqlCommand(sqlHoaDon, conn, trans);
                    cmdHD.Parameters.AddWithValue("@TongTien", tongTien);
                    cmdHD.Parameters.AddWithValue("@MaNV", maNhanVien);
                    if (maKhachHang > 0)
                        cmdHD.Parameters.AddWithValue("@MaKH", maKhachHang);
                    else
                        cmdHD.Parameters.AddWithValue("@MaKH", DBNull.Value);

                    int maHoaDon = Convert.ToInt32(cmdHD.ExecuteScalar());

                    // 2. Duyệt giỏ hàng
                    foreach (DataRow row in dtGioHang.Rows)
                    {
                        int maSP = Convert.ToInt32(row["MaSP"]);
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        decimal donGia = Convert.ToDecimal(row["DonGia"]);
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                        // 2.1. Lưu chi tiết hóa đơn
                        string sqlChiTiet = @"
                            INSERT INTO HoaDon_ChiTiets (HoaDonID, SanPhamID, SoLuong, DonGia, ThanhTien)
                            VALUES (@HoaDonID, @SanPhamID, @SoLuong, @DonGia, @ThanhTien)";

                        SqlCommand cmdCT = new SqlCommand(sqlChiTiet, conn, trans);
                        cmdCT.Parameters.AddWithValue("@HoaDonID", maHoaDon);
                        cmdCT.Parameters.AddWithValue("@SanPhamID", maSP);
                        cmdCT.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdCT.Parameters.AddWithValue("@DonGia", donGia);
                        cmdCT.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmdCT.ExecuteNonQuery();

                        // 2.2. Trừ tồn kho, chỉ trừ khi còn đủ
                        string sqlTruKho = @"UPDATE SanPhams 
                                             SET SoLuongTon = SoLuongTon - @SoLuong 
                                             WHERE ID = @MaSP AND SoLuongTon >= @SoLuong";

                        SqlCommand cmdKho = new SqlCommand(sqlTruKho, conn, trans);
                        cmdKho.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdKho.Parameters.AddWithValue("@MaSP", maSP);
                        int soDongCapNhat = cmdKho.ExecuteNonQuery();

                        if (soDongCapNhat == 0)
                        {
                            string tenSP = (row.Table.Columns.Contains("TenSP") && row["TenSP"] != DBNull.Value)
                                ? row["TenSP"].ToString()
                                : ("ID=" + maSP);
                            throw new Exception($"Sản phẩm '{tenSP}' không đủ tồn kho (cần {soLuong}). Có thể đã có người khác vừa mua.");
                        }
                    }

                    trans.Commit();
                    return maHoaDon;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Lỗi khi thanh toán: " + ex.Message);
                }
            }
        }
    }
}
