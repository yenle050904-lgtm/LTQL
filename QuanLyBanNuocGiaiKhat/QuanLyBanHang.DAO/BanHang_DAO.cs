using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class BanHang_DAO
    {
        /// <summary>
        /// Lưu hóa đơn + chi tiết hóa đơn + trừ tồn kho trong một transaction.
        /// Dùng SqlParameter để tránh lỗi dấu phẩy thập phân theo locale hệ thống.
        /// dtGioHang phải có cột: MaSP (int), SoLuong (int), DonGia (decimal), ThanhTien (decimal)
        /// </summary>
        public bool LuuHoaDon_Transaction(int maNhanVien, int maKhachHang, DataTable dtGioHang, decimal tongTien)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Tạo hóa đơn — dùng parameter để tránh lỗi dấu phẩy locale
                        string sqlHoaDon = @"
                            INSERT INTO HoaDons (NgayLap, TongTien, NhanVienID, KhachHangID)
                            VALUES (GETDATE(), @TongTien, @MaNV, @MaKH);
                            SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdHD = new SqlCommand(sqlHoaDon, conn, trans);
                        cmdHD.Parameters.AddWithValue("@TongTien", tongTien);
                        cmdHD.Parameters.AddWithValue("@MaNV", maNhanVien);

                        // Nếu không có khách hàng (maKhachHang <= 0) thì lưu NULL
                        if (maKhachHang > 0)
                            cmdHD.Parameters.AddWithValue("@MaKH", maKhachHang);
                        else
                            cmdHD.Parameters.AddWithValue("@MaKH", DBNull.Value);

                        int maHoaDon = Convert.ToInt32(cmdHD.ExecuteScalar());

                        // 2. Duyệt giỏ hàng: lưu chi tiết + trừ kho
                        foreach (DataRow row in dtGioHang.Rows)
                        {
                            int maSP = Convert.ToInt32(row["MaSP"]);
                            int soLuong = Convert.ToInt32(row["SoLuong"]);
                            decimal donGia = Convert.ToDecimal(row["DonGia"]);
                            decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                            // Lưu chi tiết hóa đơn — dùng parameter, không dùng string interpolation
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

                            // Trừ tồn kho
                            string sqlTruKho = "UPDATE SanPhams SET SoLuongTon = SoLuongTon - @SoLuong WHERE ID = @MaSP";
                            SqlCommand cmdKho = new SqlCommand(sqlTruKho, conn, trans);
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
                        throw new Exception("Lỗi khi thanh toán: " + ex.Message);
                    }
                }
            }
        }

    }
}

