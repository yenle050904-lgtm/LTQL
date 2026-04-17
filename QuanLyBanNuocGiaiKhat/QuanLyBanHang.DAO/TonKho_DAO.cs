using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class TonKho_DAO
    {
        public DataTable LayDanhSachLoaiSP()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                return DataProvider.TruyVanLayDuLieu("SELECT ID, TenLoai FROM LoaiSanPhams ORDER BY TenLoai", conn);
            }
        }

        /// <summary>
        /// Lọc tồn kho theo loại SP, tên SP, trạng thái (0: tất cả, 1: sắp hết <10, 2: còn nhiều >=10).
        /// </summary>
        public DataTable LayDuLieuTonKho(int maLoai, string tenSP, int loaiLoc)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                // Dùng biến params, thêm điều kiện động nhưng không ghép string với dữ liệu người dùng
                string sql = @"SELECT sp.ID, sp.TenSanPham, l.TenLoai, sp.SoLuongTon, sp.DonGia 
                               FROM SanPhams sp
                               LEFT JOIN LoaiSanPhams l ON sp.LoaiSanPhamID = l.ID
                               WHERE 1=1 ";

                if (!string.IsNullOrEmpty(tenSP))
                    sql += " AND sp.TenSanPham LIKE N'%' + @TenSP + N'%' ";

                if (maLoai > 0)
                    sql += " AND sp.LoaiSanPhamID = @MaLoai ";

                if (loaiLoc == 1)       // Sắp hết hàng
                    sql += " AND sp.SoLuongTon < 10 ";
                else if (loaiLoc == 2)  // Còn nhiều
                    sql += " AND sp.SoLuongTon >= 10 ";

                sql += " ORDER BY sp.ID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenSP", tenSP ?? string.Empty);
                    cmd.Parameters.AddWithValue("@MaLoai", maLoai);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
