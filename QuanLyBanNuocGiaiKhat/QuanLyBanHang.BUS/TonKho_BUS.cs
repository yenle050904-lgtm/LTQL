using System;
using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class TonKho_BUS
    {
        TonKho_DAO tkDAO = new TonKho_DAO();

        // Đã sửa lại đúng tên hàm: Gọi hàm LayDanhSachLoaiSP từ DAO
        public DataTable LayDanhSachLoaiSP()
        {
            return tkDAO.LayDanhSachLoaiSP();
        }

        public DataTable LayDuLieuTonKho(int maLoai, string tenSP, int loaiLoc)
        {
            return tkDAO.LayDuLieuTonKho(maLoai, tenSP, loaiLoc);
        }
    }
}