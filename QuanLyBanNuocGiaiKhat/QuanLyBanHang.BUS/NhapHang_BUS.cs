using System;
using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class NhapHang_BUS
    {
        NhapHang_DAO nhDAO = new NhapHang_DAO();

        public bool LuuPhieuNhap(DateTime ngayLap, int maNCC, int maNhanVien, string ghiChu, decimal tongTien, DataTable dtChiTiet)
        {
            return nhDAO.LuuPhieuNhap_Transaction(ngayLap, maNCC, maNhanVien, ghiChu, tongTien, dtChiTiet);
        }

        public DataTable LayDanhSachNhaCungCap()
        {
            return nhDAO.LayDanhSachNhaCungCap();
        }

        public DataTable LayDanhSachSanPham()
        {
            return nhDAO.LayDanhSachSanPham();
        }
    }
}