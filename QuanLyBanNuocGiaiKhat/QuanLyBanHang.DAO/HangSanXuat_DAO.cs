using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class HangSanXuat_DAO
    {
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "SELECT ID AS [Mã Hãng], TenHang AS [Tên Hãng], QuocGia AS [Quốc Gia] FROM HangSanXuats ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã Hãng], TenHang AS [Tên Hãng], QuocGia AS [Quốc Gia] 
                               FROM HangSanXuats 
                               WHERE TenHang LIKE N'%' + @TuKhoa + N'%' OR QuocGia LIKE N'%' + @TuKhoa + N'%'
                               ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        public bool Them(HangSanXuat_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "INSERT INTO HangSanXuats (TenHang, QuocGia) VALUES (@TenHang, @QuocGia)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@TenHang", (object)dto.TenHang ?? System.DBNull.Value),
                    new SqlParameter("@QuocGia", (object)dto.QuocGia ?? System.DBNull.Value));
                return kq > 0;
            }
        }

        public bool Sua(HangSanXuat_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "UPDATE HangSanXuats SET TenHang = @TenHang, QuocGia = @QuocGia WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@TenHang", (object)dto.TenHang ?? System.DBNull.Value),
                    new SqlParameter("@QuocGia", (object)dto.QuocGia ?? System.DBNull.Value),
                    new SqlParameter("@ID", dto.ID));
                return kq > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM HangSanXuats WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }
    }
}
