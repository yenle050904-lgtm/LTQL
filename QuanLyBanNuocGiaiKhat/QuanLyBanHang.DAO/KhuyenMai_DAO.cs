using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class KhuyenMai_DAO
    {
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã KM], MaCode AS [Mã Code], TenKhuyenMai AS [Tên KM],
                                      PhanTramGiam AS [% Giảm], GiamToiDa AS [Giảm Tối Đa],
                                      DonToiThieu AS [Đơn Tối Thiểu], NgayBatDau AS [Ngày BĐ],
                                      NgayKetThuc AS [Ngày KT], TrangThai AS [Hoạt Động], MoTa AS [Mô Tả]
                               FROM KhuyenMais
                               ORDER BY ID DESC";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã KM], MaCode AS [Mã Code], TenKhuyenMai AS [Tên KM],
                                      PhanTramGiam AS [% Giảm], GiamToiDa AS [Giảm Tối Đa],
                                      DonToiThieu AS [Đơn Tối Thiểu], NgayBatDau AS [Ngày BĐ],
                                      NgayKetThuc AS [Ngày KT], TrangThai AS [Hoạt Động], MoTa AS [Mô Tả]
                               FROM KhuyenMais
                               WHERE MaCode LIKE '%' + @TuKhoa + '%' OR TenKhuyenMai LIKE N'%' + @TuKhoa + N'%'
                               ORDER BY ID DESC";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        public bool Them(KhuyenMai_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"INSERT INTO KhuyenMais (MaCode, TenKhuyenMai, PhanTramGiam, GiamToiDa, 
                                                        DonToiThieu, NgayBatDau, NgayKetThuc, TrangThai, MoTa)
                               VALUES (@MaCode, @Ten, @Phan, @GiamMax, @DonMin, @BatDau, @KetThuc, @TrangThai, @MoTa)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@MaCode", (object)dto.MaCode ?? DBNull.Value),
                    new SqlParameter("@Ten", (object)dto.TenKhuyenMai ?? DBNull.Value),
                    new SqlParameter("@Phan", dto.PhanTramGiam),
                    new SqlParameter("@GiamMax", (object)dto.GiamToiDa ?? DBNull.Value),
                    new SqlParameter("@DonMin", dto.DonToiThieu),
                    new SqlParameter("@BatDau", dto.NgayBatDau),
                    new SqlParameter("@KetThuc", dto.NgayKetThuc),
                    new SqlParameter("@TrangThai", dto.TrangThai),
                    new SqlParameter("@MoTa", (object)dto.MoTa ?? DBNull.Value));
                return kq > 0;
            }
        }

        public bool Sua(KhuyenMai_DTO dto)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"UPDATE KhuyenMais 
                               SET MaCode = @MaCode, TenKhuyenMai = @Ten, PhanTramGiam = @Phan,
                                   GiamToiDa = @GiamMax, DonToiThieu = @DonMin,
                                   NgayBatDau = @BatDau, NgayKetThuc = @KetThuc,
                                   TrangThai = @TrangThai, MoTa = @MoTa
                               WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@MaCode", (object)dto.MaCode ?? DBNull.Value),
                    new SqlParameter("@Ten", (object)dto.TenKhuyenMai ?? DBNull.Value),
                    new SqlParameter("@Phan", dto.PhanTramGiam),
                    new SqlParameter("@GiamMax", (object)dto.GiamToiDa ?? DBNull.Value),
                    new SqlParameter("@DonMin", dto.DonToiThieu),
                    new SqlParameter("@BatDau", dto.NgayBatDau),
                    new SqlParameter("@KetThuc", dto.NgayKetThuc),
                    new SqlParameter("@TrangThai", dto.TrangThai),
                    new SqlParameter("@MoTa", (object)dto.MoTa ?? DBNull.Value),
                    new SqlParameter("@ID", dto.ID));
                return kq > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM KhuyenMais WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }

        /// <summary>
        /// Kiểm tra code: có tồn tại, đang hoạt động, trong khoảng ngày, đơn đủ tối thiểu.
        /// Trả về DTO nếu hợp lệ, null nếu không.
        /// </summary>
        public KhuyenMai_DTO KiemTraApDung(string maCode, decimal tongDonHang)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT * FROM KhuyenMais 
                               WHERE MaCode = @MaCode 
                                 AND TrangThai = 1 
                                 AND @NgayHienTai BETWEEN NgayBatDau AND NgayKetThuc 
                                 AND DonToiThieu <= @TongDon";
                DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@MaCode", maCode ?? string.Empty),
                    new SqlParameter("@NgayHienTai", DateTime.Now.Date),
                    new SqlParameter("@TongDon", tongDonHang));

                if (dt.Rows.Count == 0) return null;

                DataRow r = dt.Rows[0];
                KhuyenMai_DTO km = new KhuyenMai_DTO();
                km.ID = Convert.ToInt32(r["ID"]);
                km.MaCode = r["MaCode"].ToString();
                km.TenKhuyenMai = r["TenKhuyenMai"].ToString();
                km.PhanTramGiam = Convert.ToDecimal(r["PhanTramGiam"]);
                km.GiamToiDa = r["GiamToiDa"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(r["GiamToiDa"]);
                km.DonToiThieu = Convert.ToDecimal(r["DonToiThieu"]);
                km.NgayBatDau = Convert.ToDateTime(r["NgayBatDau"]);
                km.NgayKetThuc = Convert.ToDateTime(r["NgayKetThuc"]);
                km.TrangThai = Convert.ToBoolean(r["TrangThai"]);
                km.MoTa = r["MoTa"] == DBNull.Value ? null : r["MoTa"].ToString();
                return km;
            }
        }
    }
}
