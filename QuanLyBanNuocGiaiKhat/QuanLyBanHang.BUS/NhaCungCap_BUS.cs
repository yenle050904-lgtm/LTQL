using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class NhaCungCap_BUS
    {
        NhaCungCap_DAO nccDAO = new NhaCungCap_DAO();

        public DataTable LayDanhSach()
        {
            return nccDAO.LayDanhSach();
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return nccDAO.TimKiem(tuKhoa);
        }

        public bool Them(NhaCungCap_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenNhaCungCap)) return false;
            return nccDAO.Them(dto);
        }

        public bool Sua(NhaCungCap_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenNhaCungCap)) return false;
            return nccDAO.Sua(dto);
        }

        public bool Xoa(int id)
        {
            return nccDAO.Xoa(id);
        }
    }
}
