using System;
using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class BaoCao_BUS
    {
        BaoCao_DAO bcDAO = new BaoCao_DAO();

        public DataTable TopSanPhamBanChay(DateTime tuNgay, DateTime denNgay, int topN, string tuyTheo)
        {
            if (topN <= 0) topN = 10;
            return bcDAO.TopSanPhamBanChay(tuNgay, denNgay, topN, tuyTheo);
        }

        public DataTable BaoCaoLaiLo(DateTime tuNgay, DateTime denNgay)
        {
            return bcDAO.BaoCaoLaiLo(tuNgay, denNgay);
        }

        public DataRow TongQuanLaiLo(DateTime tuNgay, DateTime denNgay)
        {
            return bcDAO.TongQuanLaiLo(tuNgay, denNgay);
        }
    }
}
