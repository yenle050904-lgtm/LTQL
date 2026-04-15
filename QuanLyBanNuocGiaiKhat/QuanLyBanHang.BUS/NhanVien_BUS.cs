using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class NhanVien_BUS
    {
        NhanVien_DAO nvDAO = new NhanVien_DAO();

        public DataTable LayDanhSachNhanVien()
        {
            return nvDAO.LayDanhSach();
        }

        public bool ThemNhanVien(NhanVien_DTO nv)
        {
            return nvDAO.Them(nv);
        }

        public bool SuaNhanVien(NhanVien_DTO nv)
        {
            return nvDAO.Sua(nv);
        }

        public void XoaNhanVien(int id)
        {
            nvDAO.Xoa(id);
        }

        public DataTable TimKiemNhanVien(string tuKhoa)
        {
            return nvDAO.TimKiem(tuKhoa);
        }
        // Hàm xử lý đăng nhập
        public DataTable KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            return nvDAO.KiemTraDangNhap(tenDangNhap, matKhau);
        }
        // Hàm xử lý đổi mật khẩu
        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            // Gọi lệnh đổi mật khẩu từ tầng DAO
            return nvDAO.DoiMatKhau(tenDangNhap, matKhauCu, matKhauMoi);
        }
    }
}
