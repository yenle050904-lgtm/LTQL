using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class HangSanXuat_BUS
    {
        HangSanXuat_DAO hsxDAO = new HangSanXuat_DAO();

        public DataTable LayDanhSach()
        {
            return hsxDAO.LayDanhSach();
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return hsxDAO.TimKiem(tuKhoa);
        }

        public bool Them(HangSanXuat_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenHang)) return false;
            return hsxDAO.Them(dto);
        }

        public bool Sua(HangSanXuat_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenHang)) return false;
            return hsxDAO.Sua(dto);
        }

        public bool Xoa(int id)
        {
            return hsxDAO.Xoa(id);
        }
    }
}
