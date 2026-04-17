using System;
using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class TraHang_BUS
    {
        TraHang_DAO thDAO = new TraHang_DAO();

        public DataRow KiemTraHoaDon(int maHD)
        {
            return thDAO.KiemTraHoaDon(maHD);
        }

        public DataTable LayChiTietHoaDon(int maHD)
        {
            return thDAO.LayChiTietHoaDon(maHD);
        }

        public int LuuPhieuTraHang(int hoaDonID, int nhanVienID, string lyDo, 
                                    decimal tongTienHoan, DataTable dtChiTietTra)
        {
            return thDAO.LuuPhieuTraHang_Transaction(hoaDonID, nhanVienID, lyDo, tongTienHoan, dtChiTietTra);
        }

        public DataTable LayLichSuTraHang(DateTime tuNgay, DateTime denNgay)
        {
            return thDAO.LayLichSuTraHang(tuNgay, denNgay);
        }
    }
}
