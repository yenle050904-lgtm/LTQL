using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    public class HanSuDung_DAO
    {
        /// <summary>
        /// Lấy các lô hàng nhập kèm HSD, lọc theo trạng thái.
        /// loaiLoc: 0 = tất cả, 1 = sắp hết hạn (trong soNgayCanhBao ngày), 2 = đã hết hạn
        /// </summary>
        public DataTable LayDanhSachTheoHSD(int loaiLoc, int soNgayCanhBao)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT pnct.ID AS [ID CT],
                                      pn.ID AS [Mã Phiếu],
                                      pn.NgayNhap AS [Ngày Nhập],
                                      sp.TenSanPham AS [Tên SP],
                                      pnct.SoLuong AS [Số Lượng],
                                      pnct.GiaNhap AS [Giá Nhập],
                                      pnct.HanSuDung AS [Hạn Sử Dụng],
                                      DATEDIFF(DAY, GETDATE(), pnct.HanSuDung) AS [Còn Lại (ngày)]
                               FROM PhieuNhap_ChiTiets pnct
                               INNER JOIN PhieuNhaps pn ON pnct.PhieuNhapID = pn.ID
                               INNER JOIN SanPhams sp ON pnct.SanPhamID = sp.ID
                               WHERE pnct.HanSuDung IS NOT NULL ";

                if (loaiLoc == 1)       // Sắp hết hạn
                    sql += " AND pnct.HanSuDung >= CAST(GETDATE() AS DATE) AND pnct.HanSuDung <= DATEADD(DAY, @SoNgay, GETDATE()) ";
                else if (loaiLoc == 2)  // Đã hết hạn
                    sql += " AND pnct.HanSuDung < CAST(GETDATE() AS DATE) ";

                sql += " ORDER BY pnct.HanSuDung ASC";

                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@SoNgay", soNgayCanhBao));
            }
        }

        /// <summary>
        /// Đếm số lô sắp/đã hết hạn. Dùng để hiển thị cảnh báo khi khởi động.
        /// </summary>
        public void DemCanhBao(int soNgayCanhBao, out int soLoSapHet, out int soLoDaHet)
        {
            soLoSapHet = 0;
            soLoDaHet = 0;

            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT 
                                 SUM(CASE WHEN HanSuDung >= CAST(GETDATE() AS DATE) 
                                          AND HanSuDung <= DATEADD(DAY, @SoNgay, GETDATE()) 
                                        THEN 1 ELSE 0 END) AS SapHet,
                                 SUM(CASE WHEN HanSuDung < CAST(GETDATE() AS DATE) 
                                        THEN 1 ELSE 0 END) AS DaHet
                               FROM PhieuNhap_ChiTiets
                               WHERE HanSuDung IS NOT NULL";

                DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@SoNgay", soNgayCanhBao));

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    soLoSapHet = r["SapHet"] == DBNull.Value ? 0 : Convert.ToInt32(r["SapHet"]);
                    soLoDaHet = r["DaHet"] == DBNull.Value ? 0 : Convert.ToInt32(r["DaHet"]);
                }
            }
        }
    }
}
