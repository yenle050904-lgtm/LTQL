using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class SanPham_DAO
    {
        // ====== Các hàm phục vụ BanHang (đã có sẵn) ======

        public DataTable LayDanhSachSP_BanHang()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã SP], TenSanPham AS [Tên SP], SoLuongTon AS [Tồn kho], DonGia AS [Đơn giá] 
                               FROM SanPhams WHERE SoLuongTon > 0";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        public DataTable TimKiemSP_BanHang(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT ID AS [Mã SP], TenSanPham AS [Tên SP], SoLuongTon AS [Tồn kho], DonGia AS [Đơn giá] 
                               FROM SanPhams 
                               WHERE SoLuongTon > 0 AND TenSanPham LIKE N'%' + @TuKhoa + N'%'";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        // ====== CRUD dành cho frmSanPham ======

        public DataTable LayDanhSachDayDu()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT sp.ID AS [Mã SP], 
                                      sp.TenSanPham AS [Tên SP], 
                                      lsp.TenLoai AS [Loại SP], 
                                      sp.SoLuongTon AS [Số lượng], 
                                      sp.DonGia AS [Đơn giá], 
                                      hsx.TenHang AS [Hãng SX],
                                      sp.LoaiSanPhamID,
                                      sp.HangSanXuatID
                               FROM SanPhams sp
                               INNER JOIN LoaiSanPhams lsp ON sp.LoaiSanPhamID = lsp.ID
                               INNER JOIN HangSanXuats hsx ON sp.HangSanXuatID = hsx.ID
                               ORDER BY sp.ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn);
            }
        }

        public DataTable TimKiemDayDu(string tuKhoa)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"SELECT sp.ID AS [Mã SP], sp.TenSanPham AS [Tên SP], lsp.TenLoai AS [Loại SP], 
                                      sp.SoLuongTon AS [Số lượng], sp.DonGia AS [Đơn giá], hsx.TenHang AS [Hãng SX],
                                      sp.LoaiSanPhamID, sp.HangSanXuatID
                               FROM SanPhams sp
                               INNER JOIN LoaiSanPhams lsp ON sp.LoaiSanPhamID = lsp.ID
                               INNER JOIN HangSanXuats hsx ON sp.HangSanXuatID = hsx.ID
                               WHERE sp.TenSanPham LIKE N'%' + @TuKhoa + N'%'
                               ORDER BY sp.ID";
                return DataProvider.TruyVanLayDuLieu(sql, conn,
                    new SqlParameter("@TuKhoa", tuKhoa ?? string.Empty));
            }
        }

        public bool Them(SanPham_DTO sp)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"INSERT INTO SanPhams (TenSanPham, DonGia, SoLuongTon, LoaiSanPhamID, HangSanXuatID) 
                               VALUES (@TenSP, @DonGia, @SoLuong, @LoaiID, @HangID)";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@TenSP", (object)sp.TenSanPham ?? System.DBNull.Value),
                    new SqlParameter("@DonGia", sp.DonGia),
                    new SqlParameter("@SoLuong", sp.SoLuongTon),
                    new SqlParameter("@LoaiID", sp.LoaiSanPhamID),
                    new SqlParameter("@HangID", sp.HangSanXuatID));
                return kq > 0;
            }
        }

        public bool Sua(SanPham_DTO sp)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = @"UPDATE SanPhams 
                               SET TenSanPham = @TenSP, DonGia = @DonGia, SoLuongTon = @SoLuong, 
                                   LoaiSanPhamID = @LoaiID, HangSanXuatID = @HangID 
                               WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@TenSP", (object)sp.TenSanPham ?? System.DBNull.Value),
                    new SqlParameter("@DonGia", sp.DonGia),
                    new SqlParameter("@SoLuong", sp.SoLuongTon),
                    new SqlParameter("@LoaiID", sp.LoaiSanPhamID),
                    new SqlParameter("@HangID", sp.HangSanXuatID),
                    new SqlParameter("@ID", sp.ID));
                return kq > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                string sql = "DELETE FROM SanPhams WHERE ID = @ID";
                int kq = DataProvider.TruyVanKhongLayDuLieu(sql, conn,
                    new SqlParameter("@ID", id));
                return kq > 0;
            }
        }

        // ====== Combo hỗ trợ ======

        public DataTable LayDanhSachLoaiSP()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                return DataProvider.TruyVanLayDuLieu("SELECT ID, TenLoai FROM LoaiSanPhams ORDER BY TenLoai", conn);
            }
        }

        public DataTable LayDanhSachHangSX()
        {
            using (SqlConnection conn = DataProvider.MoKetNoi())
            {
                return DataProvider.TruyVanLayDuLieu("SELECT ID, TenHang FROM HangSanXuats ORDER BY TenHang", conn);
            }
        }
    }
}
