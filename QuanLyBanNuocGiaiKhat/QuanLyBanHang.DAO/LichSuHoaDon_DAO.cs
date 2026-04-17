using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class LichSuHoaDon_DAO
    {
        /// <summary>
        /// Lấy danh sách hóa đơn theo khoảng ngày. Dùng cho form lịch sử hóa đơn.
        /// </summary>
        public DataTable LayDanhSachHoaDon(DateTime tuNgay, DateTime denNgay)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT hd.ID AS [Mã HĐ], 
                                      hd.NgayLap AS [Ngày Lập], 
                                      nv.HoVaTen AS [Nhân Viên], 
                                      ISNULL(kh.HoTen, N'Khách vãng lai') AS [Khách Hàng], 
                                      hd.TongTien AS [Tổng Tiền]
                               FROM HoaDons hd
                               LEFT JOIN NhanViens nv ON hd.NhanVienID = nv.ID
                               LEFT JOIN KhachHangs kh ON hd.KhachHangID = kh.ID
                               WHERE hd.NgayLap >= @TuNgay AND hd.NgayLap <= @DenNgay
                               ORDER BY hd.NgayLap DESC";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay));
            }
        }

        /// <summary>
        /// Tìm nhanh theo mã hóa đơn.
        /// </summary>
        public DataTable TimKiemTheoMa(int maHD)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT hd.ID AS [Mã HĐ], 
                                      hd.NgayLap AS [Ngày Lập], 
                                      nv.HoVaTen AS [Nhân Viên], 
                                      ISNULL(kh.HoTen, N'Khách vãng lai') AS [Khách Hàng], 
                                      hd.TongTien AS [Tổng Tiền]
                               FROM HoaDons hd
                               LEFT JOIN NhanViens nv ON hd.NhanVienID = nv.ID
                               LEFT JOIN KhachHangs kh ON hd.KhachHangID = kh.ID
                               WHERE hd.ID = @MaHD";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaHD", maHD));
            }
        }

        /// <summary>
        /// Lấy chi tiết các sản phẩm trong 1 hóa đơn.
        /// </summary>
        public DataTable LayChiTietHoaDon(int maHD)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT sp.ID AS [Mã SP], 
                                      sp.TenSanPham AS [Tên SP], 
                                      ct.SoLuong AS [Số Lượng], 
                                      ct.DonGia AS [Đơn Giá], 
                                      ct.ThanhTien AS [Thành Tiền]
                               FROM HoaDon_ChiTiets ct
                               INNER JOIN SanPhams sp ON ct.SanPhamID = sp.ID
                               WHERE ct.HoaDonID = @MaHD";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaHD", maHD));
            }
        }

        /// <summary>
        /// Lấy thông tin đầy đủ của 1 hóa đơn (để phục vụ in hóa đơn).
        /// </summary>
        public DataRow LayThongTinHoaDon(int maHD)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT hd.ID, hd.NgayLap, hd.TongTien,
                                      nv.HoVaTen AS TenNhanVien, 
                                      ISNULL(kh.HoTen, N'Khách vãng lai') AS TenKhachHang,
                                      ISNULL(kh.SoDienThoai, '') AS SDTKhachHang
                               FROM HoaDons hd
                               LEFT JOIN NhanViens nv ON hd.NhanVienID = nv.ID
                               LEFT JOIN KhachHangs kh ON hd.KhachHangID = kh.ID
                               WHERE hd.ID = @MaHD";
                DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaHD", maHD));

                if (dt.Rows.Count == 0) return null;
                return dt.Rows[0];
            }
        }
    }
}
