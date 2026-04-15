using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class NhanVien_DAO
    {
        // 1. Lấy danh sách
        public DataTable LayDanhSach()
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = "SELECT ID AS [Mã NV], HoVaTen AS [Họ và Tên], DienThoai AS [Điện thoại], TenDangNhap AS [Tên đăng nhập], MatKhau AS [Mật khẩu], QuyenHan AS [Admin] FROM NhanViens";
            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }

        // 2. Thêm nhân viên
        public bool Them(NhanVien_DTO nv)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $"INSERT INTO NhanViens (HoVaTen, DienThoai, TenDangNhap, MatKhau, QuyenHan) VALUES (N'{nv.HoVaTen}', '{nv.DienThoai}', '{nv.TenDangNhap}', '{nv.MatKhau}', {nv.QuyenHan})";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery();
            DataProvider.DongKetNoi(conn);
            return rows > 0;
        }

        // 3. Sửa nhân viên
        public bool Sua(NhanVien_DTO nv)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $"UPDATE NhanViens SET HoVaTen = N'{nv.HoVaTen}', DienThoai = '{nv.DienThoai}', TenDangNhap = '{nv.TenDangNhap}', MatKhau = '{nv.MatKhau}', QuyenHan = {nv.QuyenHan} WHERE ID = {nv.ID}";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery();
            DataProvider.DongKetNoi(conn);
            return rows > 0;
        }

        // 4. Xóa nhân viên
        public void Xoa(int id)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $"DELETE FROM NhanViens WHERE ID = {id}";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            DataProvider.DongKetNoi(conn);
        }

        // 5. Tìm kiếm
        public DataTable TimKiem(string tuKhoa)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $"SELECT ID AS [Mã NV], HoVaTen AS [Họ và Tên], DienThoai AS [Điện thoại], TenDangNhap AS [Tên đăng nhập], MatKhau AS [Mật khẩu], QuyenHan AS [Admin] FROM NhanViens WHERE HoVaTen LIKE N'{tuKhoa}%' OR TenDangNhap LIKE '{tuKhoa}%'";
            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }
        public DataTable KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = "SELECT * FROM NhanViens WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", matKhau);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataProvider.DongKetNoi(conn);
            return dt;
        }

        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            // Xác minh mật khẩu cũ đúng không
            DataTable dt = KiemTraDangNhap(tenDangNhap, matKhauCu);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Lưu mật khẩu mới dạng plain text
                string sql = "UPDATE NhanViens SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
                SqlConnection conn = DataProvider.MoKetNoi();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                int kq = cmd.ExecuteNonQuery();
                DataProvider.DongKetNoi(conn);
                return kq > 0;
            }
            return false;
        }
    }
}
