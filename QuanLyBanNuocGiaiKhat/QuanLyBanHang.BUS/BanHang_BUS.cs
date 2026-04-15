using QuanLyBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.BUS
{
    public class BanHang_BUS
    {
        SanPham_DAO spDAO = new SanPham_DAO();
        KhachHang_DAO khDAO = new KhachHang_DAO();
        BanHang_DAO bhDAO = new BanHang_DAO();

        // Lấy danh sách sản phẩm còn hàng
        public DataTable LayDanhSachSanPham()
        {
            return spDAO.LayDanhSachSP_BanHang();
        }

        // Tìm kiếm sản phẩm theo tên
        public DataTable TimKiemSanPham(string tuKhoa)
        {
            return spDAO.TimKiemSP_BanHang(tuKhoa);
        }

        // Lấy danh sách khách hàng cho ComboBox
        public DataTable LayDanhSachKhachHang()
        {
            return khDAO.LayDanhSach();
        }

        // Lưu hóa đơn và trừ tồn kho
        // dtGioHang phải có cột: MaSP (int), SoLuong (int), DonGia (decimal), ThanhTien (decimal)
        public bool ThanhToan(int maNhanVien, int maKhachHang, DataTable dtGioHang, decimal tongTien)
        {
            return bhDAO.LuuHoaDon_Transaction(maNhanVien, maKhachHang, dtGioHang, tongTien);
        }
    }
}

