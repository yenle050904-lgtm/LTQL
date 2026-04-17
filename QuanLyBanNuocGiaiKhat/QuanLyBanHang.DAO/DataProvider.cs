using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang.DAO
{
    /// <summary>
    /// Lớp tiện ích kết nối CSDL.
    /// Đọc connection string từ App.config nếu có, không thì dùng chuỗi mặc định.
    /// </summary>
    public class DataProvider
    {
        // Chuỗi kết nối mặc định, phòng khi chưa cấu hình App.config
        private const string CONNECTION_STRING_MAC_DINH =
            @"Data Source=.\SQLEXPRESS01;Initial Catalog=QLBHang;Integrated Security=True;TrustServerCertificate=True";

        /// <summary>
        /// Lấy chuỗi kết nối từ App.config (key = "QLBHang"), không có thì trả về mặc định.
        /// </summary>
        public static string LayChuoiKetNoi()
        {
            try
            {
                var cfg = ConfigurationManager.ConnectionStrings["QLBHang"];
                if (cfg != null && !string.IsNullOrEmpty(cfg.ConnectionString))
                    return cfg.ConnectionString;
            }
            catch { /* nếu đọc config lỗi thì dùng mặc định */ }

            return CONNECTION_STRING_MAC_DINH;
        }

        /// <summary>
        /// Mở kết nối mới. Khuyến nghị đặt trong khối using để đảm bảo đóng đúng cách.
        /// </summary>
        public static SqlConnection MoKetNoi()
        {
            SqlConnection conn = new SqlConnection(LayChuoiKetNoi());
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Đóng kết nối (giữ lại cho tương thích với code cũ).
        /// </summary>
        public static void DongKetNoi(SqlConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
                conn.Close();
        }

        /// <summary>
        /// Chạy câu SELECT trả về DataTable. Dùng using nên luôn đóng connection.
        /// </summary>
        public static DataTable TruyVanLayDuLieu(string sTruyVan, SqlConnection conn)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sTruyVan, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Chạy SELECT có tham số (an toàn, dùng thay cho TruyVanLayDuLieu khi có biến).
        /// </summary>
        public static DataTable TruyVanLayDuLieu(string sTruyVan, SqlConnection conn, params SqlParameter[] thamSo)
        {
            using (SqlCommand cmd = new SqlCommand(sTruyVan, conn))
            {
                if (thamSo != null && thamSo.Length > 0)
                    cmd.Parameters.AddRange(thamSo);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Chạy INSERT/UPDATE/DELETE. Trả về số dòng bị ảnh hưởng.
        /// Không nuốt exception để debug dễ hơn.
        /// </summary>
        public static int TruyVanKhongLayDuLieu(string sTruyVan, SqlConnection conn, params SqlParameter[] thamSo)
        {
            using (SqlCommand cmd = new SqlCommand(sTruyVan, conn))
            {
                if (thamSo != null && thamSo.Length > 0)
                    cmd.Parameters.AddRange(thamSo);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
