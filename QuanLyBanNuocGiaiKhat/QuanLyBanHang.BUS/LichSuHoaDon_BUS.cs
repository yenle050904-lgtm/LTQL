using System;
using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class LichSuHoaDon_BUS
    {
        LichSuHoaDon_DAO lsDAO = new LichSuHoaDon_DAO();

        public DataTable LayDanhSachHoaDon(DateTime tuNgay, DateTime denNgay)
        {
            return lsDAO.LayDanhSachHoaDon(tuNgay, denNgay);
        }

        public DataTable TimKiemTheoMa(int maHD)
        {
            return lsDAO.TimKiemTheoMa(maHD);
        }

        public DataTable LayChiTietHoaDon(int maHD)
        {
            return lsDAO.LayChiTietHoaDon(maHD);
        }

        public DataRow LayThongTinHoaDon(int maHD)
        {
            return lsDAO.LayThongTinHoaDon(maHD);
        }
    }
}
