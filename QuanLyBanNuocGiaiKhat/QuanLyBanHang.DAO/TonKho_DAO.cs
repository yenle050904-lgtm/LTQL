using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DAO
{
    public class TonKho_DAO
    {
        // === Hàm lấy danh sách Loại sản phẩm ===
        public DataTable LayDanhSachLoaiSP()
        {
            SqlConnection conn = DataProvider.MoKetNoi();
            string sql = "SELECT ID, TenLoai FROM LoaiSanPhams";

            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }

        // === Hàm lọc và lấy dữ liệu tồn kho ===
        public DataTable LayDuLieuTonKho(int maLoai, string tenSP, int loaiLoc)
        {
            SqlConnection conn = DataProvider.MoKetNoi();

            // Đã sửa 'sp.GiaNhap' thành 'sp.DonGia'
            // Đã sửa 'sp.LoaiID' thành 'sp.LoaiSanPhamID'
            string sql = @"SELECT sp.ID, sp.TenSanPham, l.TenLoai, sp.SoLuongTon, sp.DonGia 
                   FROM SanPhams sp
                   LEFT JOIN LoaiSanPhams l ON sp.LoaiSanPhamID = l.ID
                   WHERE 1=1 ";

            if (!string.IsNullOrEmpty(tenSP))
            {
                sql += $" AND sp.TenSanPham LIKE N'%{tenSP}%' ";
            }

            if (maLoai > 0)
            {
                // Đã sửa 'sp.LoaiID' thành 'sp.LoaiSanPhamID'
                sql += $" AND sp.LoaiSanPhamID = {maLoai} ";
            }

            if (loaiLoc == 1) // Sắp hết hàng
            {
                sql += " AND sp.SoLuongTon < 10 ";
            }
            else if (loaiLoc == 2) // Còn nhiều hàng
            {
                sql += " AND sp.SoLuongTon >= 10 ";
            }

            DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
            DataProvider.DongKetNoi(conn);
            return dt;
        }
    }
}