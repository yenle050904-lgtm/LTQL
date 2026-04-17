using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class SanPham_BUS
    {
        SanPham_DAO spDAO = new SanPham_DAO();

        public DataTable LayDanhSachDayDu()
        {
            return spDAO.LayDanhSachDayDu();
        }

        public DataTable TimKiemDayDu(string tuKhoa)
        {
            return spDAO.TimKiemDayDu(tuKhoa);
        }

        public bool ThemSanPham(SanPham_DTO sp)
        {
            // Kiểm tra nghiệp vụ: tên không rỗng, đơn giá không âm, số lượng không âm
            if (string.IsNullOrWhiteSpace(sp.TenSanPham)) return false;
            if (sp.DonGia < 0) return false;
            if (sp.SoLuongTon < 0) return false;
            return spDAO.Them(sp);
        }

        public bool SuaSanPham(SanPham_DTO sp)
        {
            if (string.IsNullOrWhiteSpace(sp.TenSanPham)) return false;
            if (sp.DonGia < 0) return false;
            if (sp.SoLuongTon < 0) return false;
            return spDAO.Sua(sp);
        }

        public bool XoaSanPham(int id)
        {
            return spDAO.Xoa(id);
        }

        public DataTable LayDanhSachLoaiSP()
        {
            return spDAO.LayDanhSachLoaiSP();
        }

        public DataTable LayDanhSachHangSX()
        {
            return spDAO.LayDanhSachHangSX();
        }
    }
}
