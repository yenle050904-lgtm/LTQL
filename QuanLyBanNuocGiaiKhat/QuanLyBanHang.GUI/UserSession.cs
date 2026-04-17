using System;

namespace QuanLyBanHang.GUI
{
    /// <summary>
    /// Lưu thông tin phiên đăng nhập hiện tại (biến toàn cục cho toàn app).
    /// </summary>
    public static class UserSession
    {
        public static int ID { get; set; }
        public static string HoTen { get; set; }
        public static string TenDangNhap { get; set; }
        public static bool IsAdmin { get; set; }
        public static DateTime ThoiGianVao { get; set; }

        /// <summary>
        /// Gán thông tin sau khi đăng nhập thành công. Tự set ThoiGianVao để không phải gán riêng.
        /// </summary>
        public static void DangNhap(int id, string hoTen, string tenDangNhap, bool isAdmin)
        {
            ID = id;
            HoTen = hoTen;
            TenDangNhap = tenDangNhap;
            IsAdmin = isAdmin;
            ThoiGianVao = DateTime.Now;
        }

        public static void DangXuat()
        {
            ID = 0;
            HoTen = string.Empty;
            TenDangNhap = string.Empty;
            IsAdmin = false;
            ThoiGianVao = DateTime.MinValue;
        }
    }
}
