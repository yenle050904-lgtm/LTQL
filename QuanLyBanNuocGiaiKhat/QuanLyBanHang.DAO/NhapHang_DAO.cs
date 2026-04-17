using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class NhapHang_DAO
    {
        /// <summary>
        /// Lưu phiếu nhập. dtChiTiet cần có: MaSP, SoLuong, GiaNhap, ThanhTien, HanSuDung (DateTime? - có thể null).
        /// </summary>
        public bool LuuPhieuNhap_Transaction(DateTime ngayLap, int maNCC, int maNhanVien, string ghiChu, 
                                              decimal tongTien, DataTable dtChiTiet)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string sqlPhieuNhap = @"
                        INSERT INTO PhieuNhaps (NgayNhap, NhaCungCapID, NhanVienID, TongTien) 
                        VALUES (@NgayNhap, @MaNCC, @MaNV, @TongTien);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPhieu = new SqlCommand(sqlPhieuNhap, conn, trans);
                    cmdPhieu.Parameters.AddWithValue("@NgayNhap", ngayLap);
                    cmdPhieu.Parameters.AddWithValue("@MaNCC", maNCC);
                    cmdPhieu.Parameters.AddWithValue("@MaNV", maNhanVien);
                    cmdPhieu.Parameters.AddWithValue("@TongTien", tongTien);

                    int maPhieuVuaTao = Convert.ToInt32(cmdPhieu.ExecuteScalar());

                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        int maSP = Convert.ToInt32(row["MaSP"]);
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                        // HSD có thể không có trong dtChiTiet (khi gọi từ form cũ chưa update)
                        object hsdValue = DBNull.Value;
                        if (row.Table.Columns.Contains("HanSuDung") && row["HanSuDung"] != DBNull.Value)
                        {
                            DateTime? hsd = row["HanSuDung"] as DateTime?;
                            if (hsd.HasValue) hsdValue = hsd.Value;
                            else if (row["HanSuDung"] is DateTime dt) hsdValue = dt;
                        }

                        string sqlChiTiet = @"
                            INSERT INTO PhieuNhap_ChiTiets (PhieuNhapID, SanPhamID, SoLuong, GiaNhap, ThanhTien, HanSuDung) 
                            VALUES (@PhieuNhapID, @SanPhamID, @SoLuong, @GiaNhap, @ThanhTien, @HSD)";

                        SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, trans);
                        cmdChiTiet.Parameters.AddWithValue("@PhieuNhapID", maPhieuVuaTao);
                        cmdChiTiet.Parameters.AddWithValue("@SanPhamID", maSP);
                        cmdChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdChiTiet.Parameters.AddWithValue("@GiaNhap", giaNhap);
                        cmdChiTiet.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmdChiTiet.Parameters.AddWithValue("@HSD", hsdValue);
                        cmdChiTiet.ExecuteNonQuery();

                        // Cộng tồn kho
                        string sqlCapNhatKho = "UPDATE SanPhams SET SoLuongTon = SoLuongTon + @SoLuong WHERE ID = @MaSP";
                        SqlCommand cmdKho = new SqlCommand(sqlCapNhatKho, conn, trans);
                        cmdKho.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdKho.Parameters.AddWithValue("@MaSP", maSP);
                        cmdKho.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Lỗi khi lưu phiếu nhập: " + ex.Message);
                }
            }
        }

        public DataTable LayDanhSachNhaCungCap()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                return DataProvider.TruyVanLayDuLieu("SELECT ID, TenNhaCungCap FROM NhaCungCaps", conn);
            }
        }

        public DataTable LayDanhSachSanPham()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                return DataProvider.TruyVanLayDuLieu("SELECT ID, TenSanPham FROM SanPhams", conn);
            }
        }
    }
}
