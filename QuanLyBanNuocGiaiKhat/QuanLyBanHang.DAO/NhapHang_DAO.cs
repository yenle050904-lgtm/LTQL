using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class NhapHang_DAO
    {
        public bool LuuPhieuNhap_Transaction(DateTime ngayLap, int maNCC, int maNhanVien, string ghiChu, decimal tongTien, DataTable dtChiTiet)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Tạo Phiếu Nhập — dùng SqlParameter để tránh SQL Injection và lỗi locale
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

                        // 2. Duyệt qua giỏ hàng để lưu chi tiết & cộng kho
                        foreach (DataRow row in dtChiTiet.Rows)
                        {
                            int maSP = Convert.ToInt32(row["MaSP"]);
                            int soLuong = Convert.ToInt32(row["SoLuong"]);
                            decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);
                            decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                            // 2.1 Lưu vào bảng Chi Tiết Phiếu Nhập
                            string sqlChiTiet = @"
                                INSERT INTO PhieuNhap_ChiTiets (PhieuNhapID, SanPhamID, SoLuong, GiaNhap, ThanhTien) 
                                VALUES (@PhieuNhapID, @SanPhamID, @SoLuong, @GiaNhap, @ThanhTien)";

                            SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, trans);
                            cmdChiTiet.Parameters.AddWithValue("@PhieuNhapID", maPhieuVuaTao);
                            cmdChiTiet.Parameters.AddWithValue("@SanPhamID", maSP);
                            cmdChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmdChiTiet.Parameters.AddWithValue("@GiaNhap", giaNhap);
                            cmdChiTiet.Parameters.AddWithValue("@ThanhTien", thanhTien);
                            cmdChiTiet.ExecuteNonQuery();

                            // 2.2 Cập nhật Tăng số lượng tồn kho cho bảng Sản Phẩm
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
        }

        // Hàm lấy danh sách Nhà cung cấp
        public DataTable LayDanhSachNhaCungCap()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "SELECT ID, TenNhaCungCap FROM NhaCungCaps";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        // Hàm lấy danh sách Sản phẩm
        public DataTable LayDanhSachSanPham()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "SELECT ID, TenSanPham FROM SanPhams";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

    }
}
