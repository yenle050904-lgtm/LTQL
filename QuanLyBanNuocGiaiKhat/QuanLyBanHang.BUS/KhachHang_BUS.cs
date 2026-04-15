using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class KhachHang_BUS
    {
        KhachHang_DAO khDAO = new KhachHang_DAO();

        public DataTable LayDanhSachKhachHang()
        {
            return khDAO.LayDanhSach();
        }

        public bool ThemKhachHang(KhachHang_DTO kh)
        {
            return khDAO.Them(kh);
        }

        public bool SuaKhachHang(KhachHang_DTO kh)
        {
            return khDAO.Sua(kh);
        }

        public bool XoaKhachHang(int id)
        {
            return khDAO.Xoa(id);
        }

        public DataTable TimKiemKhachHang(string tuKhoa)
        {
            return khDAO.TimKiem(tuKhoa);
        }
    }
}
