using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class LichSuPhieuNhap_DAO
    {
        public DataTable LayDanhSachPhieuNhap(DateTime tuNgay, DateTime denNgay)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT pn.ID AS [Mã Phiếu], 
                                      pn.NgayNhap AS [Ngày Nhập], 
                                      nv.HoVaTen AS [Nhân Viên Lập], 
                                      ncc.TenNhaCungCap AS [Nhà Cung Cấp], 
                                      pn.TongTien AS [Tổng Tiền]
                               FROM PhieuNhaps pn
                               LEFT JOIN NhanViens nv ON pn.NhanVienID = nv.ID
                               LEFT JOIN NhaCungCaps ncc ON pn.NhaCungCapID = ncc.ID
                               WHERE pn.NgayNhap >= @TuNgay AND pn.NgayNhap <= @DenNgay
                               ORDER BY pn.NgayNhap DESC";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay));
            }
        }

        public DataTable TimKiemTheoMa(int maPN)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT pn.ID AS [Mã Phiếu], 
                                      pn.NgayNhap AS [Ngày Nhập], 
                                      nv.HoVaTen AS [Nhân Viên Lập], 
                                      ncc.TenNhaCungCap AS [Nhà Cung Cấp], 
                                      pn.TongTien AS [Tổng Tiền]
                               FROM PhieuNhaps pn
                               LEFT JOIN NhanViens nv ON pn.NhanVienID = nv.ID
                               LEFT JOIN NhaCungCaps ncc ON pn.NhaCungCapID = ncc.ID
                               WHERE pn.ID = @MaPN";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaPN", maPN));
            }
        }

        public DataTable LayChiTietPhieuNhap(int maPN)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT sp.ID AS [Mã SP], 
                                      sp.TenSanPham AS [Tên SP], 
                                      ct.SoLuong AS [Số Lượng], 
                                      ct.GiaNhap AS [Giá Nhập], 
                                      ct.ThanhTien AS [Thành Tiền]
                               FROM PhieuNhap_ChiTiets ct
                               INNER JOIN SanPhams sp ON ct.SanPhamID = sp.ID
                               WHERE ct.PhieuNhapID = @MaPN";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaPN", maPN));
            }
        }
    }
}
