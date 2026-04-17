using System;
using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class LichSuPhieuNhap_BUS
    {
        LichSuPhieuNhap_DAO lsDAO = new LichSuPhieuNhap_DAO();

        public DataTable LayDanhSachPhieuNhap(DateTime tuNgay, DateTime denNgay)
        {
            return lsDAO.LayDanhSachPhieuNhap(tuNgay, denNgay);
        }

        public DataTable TimKiemTheoMa(int maPN)
        {
            return lsDAO.TimKiemTheoMa(maPN);
        }

        public DataTable LayChiTietPhieuNhap(int maPN)
        {
            return lsDAO.LayChiTietPhieuNhap(maPN);
        }
    }
}
