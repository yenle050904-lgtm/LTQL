using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DAO
{
    public class HoaDon_DAO
    {

        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay, string manv)
        {
            System.Data.SqlClient.SqlConnection conn = DataProvider.MoKetNoi();

            string sql = @"
        SELECT hd.ID AS [Mã HĐ], 
               hd.NgayLap AS [Ngày Lập], 
               nv.HoVaTen AS [Nhân Viên], 
               kh.HoTen AS [Khách Hàng], 
               hd.TongTien AS [Tổng Tiền]
        FROM HoaDons hd
        LEFT JOIN NhanViens nv ON hd.NhanVienID = nv.ID
        LEFT JOIN KhachHangs kh ON hd.KhachHangID = kh.ID
        WHERE hd.NgayLap >= @TuNgay AND hd.NgayLap <= @DenNgay";

            // Lọc theo nhân viên nếu không phải "0" (tất cả)
            if (manv != "0" && !string.IsNullOrEmpty(manv))
            {
                sql += " AND hd.NhanVienID = @MaNV";
            }

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@DenNgay", denNgay);

            if (manv != "0" && !string.IsNullOrEmpty(manv))
            {
                cmd.Parameters.AddWithValue("@MaNV", manv);
            }

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataProvider.DongKetNoi(conn);

            return dt;
        }
    }
}

