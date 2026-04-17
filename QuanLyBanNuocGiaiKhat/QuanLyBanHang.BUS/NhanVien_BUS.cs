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
            // Nghiệp vụ: kiểm tra trùng tên đăng nhập trước khi thêm
            if (nvDAO.KiemTraTonTaiTenDangNhap(nv.TenDangNhap))
                return false;

            return nvDAO.Them(nv);
        }

        public bool SuaNhanVien(NhanVien_DTO nv)
        {
            // Khi sửa, bỏ qua chính bản ghi đang sửa
            if (nvDAO.KiemTraTonTaiTenDangNhap(nv.TenDangNhap, nv.ID))
                return false;

            return nvDAO.Sua(nv);
        }

        public bool XoaNhanVien(int id)
        {
            return nvDAO.Xoa(id);
        }

        public DataTable TimKiemNhanVien(string tuKhoa)
        {
            return nvDAO.TimKiem(tuKhoa);
        }

        public DataTable KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            return nvDAO.KiemTraDangNhap(tenDangNhap, matKhau);
        }

        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            return nvDAO.DoiMatKhau(tenDangNhap, matKhauCu, matKhauMoi);
        }

        public bool KiemTraTonTaiTenDangNhap(string tenDangNhap, int idBoQua = 0)
        {
            return nvDAO.KiemTraTonTaiTenDangNhap(tenDangNhap, idBoQua);
        }
    }
}
