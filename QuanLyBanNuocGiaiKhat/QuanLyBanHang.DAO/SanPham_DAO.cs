using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DAO
{
    public class SanPham_DAO
    {
        // Hàm này chuyên chui xuống SQL lấy dữ liệu lên
        public DataTable LayDanhSachSP_BanHang()
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = @"SELECT ID AS [Mã SP], TenSanPham AS [Tên SP], SoLuongTon AS [Tồn kho], DonGia AS [Đơn giá] 
                           FROM SanPhams WHERE SoLuongTon > 0";

            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }

        // Bạn có thể viết luôn hàm Tìm kiếm ở đây
        public DataTable TimKiemSP_BanHang(string tuKhoa)
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = $@"SELECT ID AS [Mã SP], TenSanPham AS [Tên SP], SoLuongTon AS [Tồn kho], DonGia AS [Đơn giá] 
                            FROM SanPhams WHERE SoLuongTon > 0 AND TenSanPham LIKE N'%{tuKhoa}%'";

            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }
    }
}

