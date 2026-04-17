using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class NhaCungCap_DAO
    {
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã NCC], TenNhaCungCap AS [Tên NCC], 
                                      SoDienThoai AS [Số ĐT], DiaChi AS [Địa Chỉ], GhiChu AS [Ghi Chú] 
                               FROM NhaCungCaps ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã NCC], TenNhaCungCap AS [Tên NCC], 
                                      SoDienThoai AS [Số ĐT], DiaChi AS [Địa Chỉ], GhiChu AS [Ghi Chú] 
                               FROM NhaCungCaps 
                               WHERE TenNhaCungCap LIKE N'%' + @TuKhoa + N'%' 
                                  OR SoDienThoai LIKE '%' + @TuKhoa + '%'
                               ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        public bool Them(NhaCungCap_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"INSERT INTO NhaCungCaps (TenNhaCungCap, SoDienThoai, DiaChi, GhiChu) 
                               VALUES (@Ten, @SDT, @DiaChi, @GhiChu)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@Ten", (object)dto.TenNhaCungCap ?? System.DBNull.Value),
                    new SqlParameter("@SDT", (object)dto.SoDienThoai ?? System.DBNull.Value),
                    new SqlParameter("@DiaChi", (object)dto.DiaChi ?? System.DBNull.Value),
                    new SqlParameter("@GhiChu", (object)dto.GhiChu ?? System.DBNull.Value));
                return kq > 0;
            }
        }

        public bool Sua(NhaCungCap_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"UPDATE NhaCungCaps 
                               SET TenNhaCungCap = @Ten, SoDienThoai = @SDT, DiaChi = @DiaChi, GhiChu = @GhiChu 
                               WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@Ten", (object)dto.TenNhaCungCap ?? System.DBNull.Value),
                    new SqlParameter("@SDT", (object)dto.SoDienThoai ?? System.DBNull.Value),
                    new SqlParameter("@DiaChi", (object)dto.DiaChi ?? System.DBNull.Value),
                    new SqlParameter("@GhiChu", (object)dto.GhiChu ?? System.DBNull.Value),
                    new SqlParameter("@ID", dto.ID));
                return kq > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM NhaCungCaps WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }
    }
}
