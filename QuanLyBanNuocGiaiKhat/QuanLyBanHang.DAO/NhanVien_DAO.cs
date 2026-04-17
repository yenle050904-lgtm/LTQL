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
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã NV], HoVaTen AS [Họ và Tên], DienThoai AS [Điện thoại], 
                                      TenDangNhap AS [Tên đăng nhập], MatKhau AS [Mật khẩu], QuyenHan AS [Admin] 
                               FROM NhanViens";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        // 2. Thêm nhân viên
        public bool Them(NhanVien_DTO nv)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"INSERT INTO NhanViens (HoVaTen, DienThoai, TenDangNhap, MatKhau, QuyenHan) 
                               VALUES (@HoVaTen, @DienThoai, @TenDangNhap, @MatKhau, @QuyenHan)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@HoVaTen", (object)nv.HoVaTen ?? System.DBNull.Value),
                    new SqlParameter("@DienThoai", (object)nv.DienThoai ?? System.DBNull.Value),
                    new SqlParameter("@TenDangNhap", (object)nv.TenDangNhap ?? System.DBNull.Value),
                    new SqlParameter("@MatKhau", (object)nv.MatKhau ?? System.DBNull.Value),
                    new SqlParameter("@QuyenHan", nv.QuyenHan));
                return kq > 0;
            }
        }

        // 3. Sửa nhân viên
        public bool Sua(NhanVien_DTO nv)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"UPDATE NhanViens 
                               SET HoVaTen = @HoVaTen, DienThoai = @DienThoai, TenDangNhap = @TenDangNhap, 
                                   MatKhau = @MatKhau, QuyenHan = @QuyenHan 
                               WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@HoVaTen", (object)nv.HoVaTen ?? System.DBNull.Value),
                    new SqlParameter("@DienThoai", (object)nv.DienThoai ?? System.DBNull.Value),
                    new SqlParameter("@TenDangNhap", (object)nv.TenDangNhap ?? System.DBNull.Value),
                    new SqlParameter("@MatKhau", (object)nv.MatKhau ?? System.DBNull.Value),
                    new SqlParameter("@QuyenHan", nv.QuyenHan),
                    new SqlParameter("@ID", nv.ID));
                return kq > 0;
            }
        }

        // 4. Xóa nhân viên
        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM NhanViens WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }

        // 5. Tìm kiếm
        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã NV], HoVaTen AS [Họ và Tên], DienThoai AS [Điện thoại], 
                                      TenDangNhap AS [Tên đăng nhập], MatKhau AS [Mật khẩu], QuyenHan AS [Admin] 
                               FROM NhanViens 
                               WHERE HoVaTen LIKE N'%' + @TuKhoa + N'%' OR TenDangNhap LIKE '%' + @TuKhoa + '%'";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        // 6. Kiểm tra đăng nhập
        public DataTable KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "SELECT * FROM NhanViens WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TenDangNhap", tenDangNhap ?? string.Empty),
                    new SqlParameter("@MatKhau", matKhau ?? string.Empty));
            }
        }

        // 7. Đổi mật khẩu
        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            // Xác minh mật khẩu cũ
            DataTable dt = KiemTraDangNhap(tenDangNhap, matKhauCu);
            if (dt == null || dt.Rows.Count == 0)
                return false;

            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "UPDATE NhanViens SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@MatKhauMoi", matKhauMoi ?? string.Empty),
                    new SqlParameter("@TenDangNhap", tenDangNhap ?? string.Empty));
                return kq > 0;
            }
        }

        // 8. Kiểm tra tên đăng nhập đã tồn tại chưa (phục vụ cho thêm mới)
        public bool KiemTraTonTaiTenDangNhap(string tenDangNhap, int idBoQua = 0)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                // idBoQua > 0 dùng khi sửa: bỏ qua chính bản ghi đang sửa
                string sql = "SELECT COUNT(*) FROM NhanViens WHERE TenDangNhap = @TenDangNhap AND ID <> @ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap ?? string.Empty);
                    cmd.Parameters.AddWithValue("@ID", idBoQua);
                    int count = System.Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
