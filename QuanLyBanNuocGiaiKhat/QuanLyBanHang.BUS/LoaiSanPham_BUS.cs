using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class LoaiSanPham_BUS
    {
        LoaiSanPham_DAO lspDAO = new LoaiSanPham_DAO();

        public DataTable LayDanhSach()
        {
            return lspDAO.LayDanhSach();
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return lspDAO.TimKiem(tuKhoa);
        }

        public bool Them(LoaiSanPham_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenLoai)) return false;
            return lspDAO.Them(dto);
        }

        public bool Sua(LoaiSanPham_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenLoai)) return false;
            return lspDAO.Sua(dto);
        }

        public bool Xoa(int id)
        {
            return lspDAO.Xoa(id);
        }
    }
}
