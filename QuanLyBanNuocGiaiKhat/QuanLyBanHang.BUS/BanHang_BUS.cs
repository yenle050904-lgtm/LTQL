using System.Data;
using QuanLyBanHang.DAO;

namespace QuanLyBanHang.BUS
{
    public class BanHang_BUS
    {
        SanPham_DAO spDAO = new SanPham_DAO();
        KhachHang_DAO khDAO = new KhachHang_DAO();
        BanHang_DAO bhDAO = new BanHang_DAO();

        public DataTable LayDanhSachSanPham()
        {
            return spDAO.LayDanhSachSP_BanHang();
        }

        public DataTable TimKiemSanPham(string tuKhoa)
        {
            return spDAO.TimKiemSP_BanHang(tuKhoa);
        }

        public DataTable LayDanhSachKhachHang()
        {
            return khDAO.LayDanhSachChoComboBox();
        }

        /// <summary>
        /// Trả về mã hóa đơn vừa tạo. Gọi xong có thể mở frmInHoaDon(maHoaDon).
        /// </summary>
        public int ThanhToan(int maNhanVien, int maKhachHang, DataTable dtGioHang, decimal tongTien)
        {
            return bhDAO.LuuHoaDon_Transaction(maNhanVien, maKhachHang, dtGioHang, tongTien);
        }
    }
}
