using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class KhachHang_DAO
    {
        // 1. Lấy danh sách
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "SELECT ID, HoTen AS [Họ Tên], SoDienThoai AS [Số ĐT], DiaChi AS [Địa chỉ] FROM KhachHangs";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        // 2. Thêm khách hàng (dùng SqlParameter để chống SQL Injection)
        public bool Them(KhachHang_DTO kh)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "INSERT INTO KhachHangs (HoTen, SoDienThoai, DiaChi) VALUES (@HoTen, @SDT, @DiaChi)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@HoTen", (object)kh.HoTen ?? System.DBNull.Value),
                    new SqlParameter("@SDT", (object)kh.SoDienThoai ?? System.DBNull.Value),
                    new SqlParameter("@DiaChi", (object)kh.DiaChi ?? System.DBNull.Value));
                return kq > 0;
            }
        }

        // 3. Sửa khách hàng
        public bool Sua(KhachHang_DTO kh)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "UPDATE KhachHangs SET HoTen = @HoTen, SoDienThoai = @SDT, DiaChi = @DiaChi WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@HoTen", (object)kh.HoTen ?? System.DBNull.Value),
                    new SqlParameter("@SDT", (object)kh.SoDienThoai ?? System.DBNull.Value),
                    new SqlParameter("@DiaChi", (object)kh.DiaChi ?? System.DBNull.Value),
                    new SqlParameter("@ID", kh.ID));
                return kq > 0;
            }
        }

        // 4. Xóa khách hàng
        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM KhachHangs WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }

        // 5. Tìm kiếm (match cả prefix lẫn giữa chuỗi)
        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID, HoTen AS [Họ Tên], SoDienThoai AS [Số ĐT], DiaChi AS [Địa chỉ] 
                               FROM KhachHangs 
                               WHERE HoTen LIKE N'%' + @TuKhoa + N'%' OR SoDienThoai LIKE '%' + @TuKhoa + '%'";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        // 6. Lấy theo ID (phục vụ BanHang combo)
        public DataTable LayDanhSachChoComboBox()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                // Trả về tên cột không dấu để bind cho chắc chắn
                string sql = "SELECT ID, HoTen FROM KhachHangs ORDER BY ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }
    }
}
