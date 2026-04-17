using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class LoaiSanPham_DAO
    {
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "SELECT ID AS [Mã Loại], TenLoai AS [Tên Loại], MoTa AS [Mô Tả] FROM LoaiSanPhams ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã Loại], TenLoai AS [Tên Loại], MoTa AS [Mô Tả] 
                               FROM LoaiSanPhams 
                               WHERE TenLoai LIKE N'%' + @TuKhoa + N'%' OR MoTa LIKE N'%' + @TuKhoa + N'%'
                               ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        public bool Them(LoaiSanPham_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "INSERT INTO LoaiSanPhams (TenLoai, MoTa) VALUES (@TenLoai, @MoTa)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@TenLoai", (object)dto.TenLoai ?? System.DBNull.Value),
                    new SqlParameter("@MoTa", (object)dto.MoTa ?? System.DBNull.Value));
                return kq > 0;
            }
        }

        public bool Sua(LoaiSanPham_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "UPDATE LoaiSanPhams SET TenLoai = @TenLoai, MoTa = @MoTa WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@TenLoai", (object)dto.TenLoai ?? System.DBNull.Value),
                    new SqlParameter("@MoTa", (object)dto.MoTa ?? System.DBNull.Value),
                    new SqlParameter("@ID", dto.ID));
                return kq > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM LoaiSanPhams WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }
    }
}
