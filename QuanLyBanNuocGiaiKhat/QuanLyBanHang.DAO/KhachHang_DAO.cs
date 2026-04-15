using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO; // Gọi DTO để xài

namespace QuanLyBanHang.DAO
{
    public class KhachHang_DAO
    {
        // 1. Lấy danh sách
        public DataTable LayDanhSach()
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = "SELECT ID, HoTen AS [Họ Tên], SoDienThoai AS [Số ĐT], DiaChi AS [Địa chỉ] FROM KhachHangs";
            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }

        // 2. Thêm khách hàng
        public bool Them(KhachHang_DTO kh)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $"INSERT INTO KhachHangs (HoTen, SoDienThoai, DiaChi) VALUES (N'{kh.HoTen}', '{kh.SoDienThoai}', N'{kh.DiaChi}')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery();
            DataProvider.DongKetNoi(conn);
            return rows > 0;
        }

        // 3. Sửa khách hàng
        public bool Sua(KhachHang_DTO kh)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $"UPDATE KhachHangs SET HoTen = N'{kh.HoTen}', SoDienThoai = '{kh.SoDienThoai}', DiaChi = N'{kh.DiaChi}' WHERE ID = {kh.ID}";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery();
            DataProvider.DongKetNoi(conn);
            return rows > 0;
        }

        // 4. Xóa khách hàng
        public bool Xoa(int id)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = "DELETE FROM KhachHangs WHERE ID = " + id;
            bool ketQua = DataProvider.TruyVanKhongLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return ketQua;
        }

        // 5. Tìm kiếm
        public DataTable TimKiem(string tuKhoa)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = string.Format(
                "SELECT ID, HoTen AS [Họ Tên], SoDienThoai AS [Số ĐT], DiaChi AS [Địa chỉ] FROM KhachHangs WHERE HoTen LIKE N'{0}%' OR SoDienThoai LIKE '%{0}%'",
                tuKhoa);
            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }
    }
}
