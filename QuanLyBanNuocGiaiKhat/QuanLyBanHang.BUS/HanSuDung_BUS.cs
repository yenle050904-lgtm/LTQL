using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class HanSuDung_BUS
    {
        HanSuDung_DAO hsdDAO = new HanSuDung_DAO();

        public DataTable LayDanhSachTheoHSD(int loaiLoc, int soNgayCanhBao)
        {
            return hsdDAO.LayDanhSachTheoHSD(loaiLoc, soNgayCanhBao);
        }

        public void DemCanhBao(int soNgayCanhBao, out int soLoSapHet, out int soLoDaHet)
        {
            hsdDAO.DemCanhBao(soNgayCanhBao, out soLoSapHet, out soLoDaHet);
        }
    }
}
