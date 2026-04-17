using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class BaoCao_DAO
    {
        /// <summary>
        /// Top N sản phẩm bán chạy trong khoảng ngày.
        /// Sắp xếp theo số lượng hoặc doanh thu (tuyTheo = "SoLuong" hoặc "DoanhThu")
        /// </summary>
        public DataTable TopSanPhamBanChay(DateTime tuNgay, DateTime denNgay, int topN, string tuyTheo)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                // Whitelist để tránh SQL injection qua tuyTheo
                string orderBy = (tuyTheo == "DoanhThu") ? "DoanhThu" : "TongSoLuong";

                string sql = $@"SELECT TOP (@TopN) 
                                      sp.ID AS [Mã SP],
                                      sp.TenSanPham AS [Tên SP],
                                      SUM(ct.SoLuong) AS TongSoLuong,
                                      SUM(ct.ThanhTien) AS DoanhThu
                               FROM HoaDon_ChiTiets ct
                               INNER JOIN HoaDons hd ON ct.HoaDonID = hd.ID
                               INNER JOIN SanPhams sp ON ct.SanPhamID = sp.ID
                               WHERE hd.NgayLap >= @TuNgay AND hd.NgayLap <= @DenNgay
                               GROUP BY sp.ID, sp.TenSanPham
                               ORDER BY {orderBy} DESC";

                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TopN", topN));
            }
        }

        /// <summary>
        /// Báo cáo lãi/lỗ trong khoảng ngày.
        /// Giá vốn lấy từ giá nhập gần nhất (hoặc trung bình tất cả lần nhập) của SP.
        /// Cách đơn giản: dùng giá nhập trung bình của SP trong tất cả phiếu nhập.
        /// </summary>
        public DataTable BaoCaoLaiLo(DateTime tuNgay, DateTime denNgay)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"
                    WITH GiaNhapTB AS (
                        -- Giá nhập trung bình có trọng số theo số lượng nhập
                        SELECT SanPhamID, 
                               CASE WHEN SUM(SoLuong) > 0 
                                    THEN SUM(GiaNhap * SoLuong) / SUM(SoLuong) 
                                    ELSE 0 
                               END AS GiaVonTB
                        FROM PhieuNhap_ChiTiets
                        GROUP BY SanPhamID
                    )
                    SELECT sp.ID AS [Mã SP],
                           sp.TenSanPham AS [Tên SP],
                           SUM(ct.SoLuong) AS [Số Lượng Bán],
                           SUM(ct.ThanhTien) AS [Doanh Thu],
                           ISNULL(gn.GiaVonTB, 0) AS [Giá Vốn TB],
                           SUM(ct.SoLuong) * ISNULL(gn.GiaVonTB, 0) AS [Chi Phí],
                           SUM(ct.ThanhTien) - (SUM(ct.SoLuong) * ISNULL(gn.GiaVonTB, 0)) AS [Lợi Nhuận]
                    FROM HoaDon_ChiTiets ct
                    INNER JOIN HoaDons hd ON ct.HoaDonID = hd.ID
                    INNER JOIN SanPhams sp ON ct.SanPhamID = sp.ID
                    LEFT JOIN GiaNhapTB gn ON sp.ID = gn.SanPhamID
                    WHERE hd.NgayLap >= @TuNgay AND hd.NgayLap <= @DenNgay
                    GROUP BY sp.ID, sp.TenSanPham, gn.GiaVonTB
                    ORDER BY [Lợi Nhuận] DESC";

                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay));
            }
        }

        /// <summary>
        /// Tổng quan báo cáo lãi/lỗ: trả về 1 dòng tổng hợp.
        /// </summary>
        public DataRow TongQuanLaiLo(DateTime tuNgay, DateTime denNgay)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"
                    WITH GiaNhapTB AS (
                        SELECT SanPhamID, 
                               CASE WHEN SUM(SoLuong) > 0 
                                    THEN SUM(GiaNhap * SoLuong) / SUM(SoLuong) 
                                    ELSE 0 
                               END AS GiaVonTB
                        FROM PhieuNhap_ChiTiets
                        GROUP BY SanPhamID
                    )
                    SELECT 
                        ISNULL(SUM(ct.ThanhTien), 0) AS TongDoanhThu,
                        ISNULL(SUM(ct.SoLuong * ISNULL(gn.GiaVonTB, 0)), 0) AS TongChiPhi,
                        ISNULL(SUM(ct.ThanhTien) - SUM(ct.SoLuong * ISNULL(gn.GiaVonTB, 0)), 0) AS TongLoiNhuan,
                        COUNT(DISTINCT hd.ID) AS SoHoaDon
                    FROM HoaDon_ChiTiets ct
                    INNER JOIN HoaDons hd ON ct.HoaDonID = hd.ID
                    LEFT JOIN GiaNhapTB gn ON ct.SanPhamID = gn.SanPhamID
                    WHERE hd.NgayLap >= @TuNgay AND hd.NgayLap <= @DenNgay";

                DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay));

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }
    }
}
