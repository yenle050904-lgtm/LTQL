using System;

namespace QuanLyBanHang.GUI
{
    /// <summary>
    /// Lưu thông tin nhân viên đang đăng nhập vào biến static toàn cục.
    /// Dùng ở bất kỳ form nào bằng cách gọi UserSession.HoTen, UserSession.IsAdmin, v.v.
    /// </summary>
    public static class UserSession
    {
        public static int    ID          { get; set; }
        public static string HoTen       { get; set; }
        public static string TenDangNhap { get; set; }
        public static bool   IsAdmin     { get; set; } // true = Admin, false = Nhân viên

        // THÊM DÒNG NÀY VÀO:
        public static DateTime ThoiGianVao { get; set; }

        public static void DangNhap(int id, string hoTen, string tenDangNhap, bool isAdmin)
        {
            ID          = id;
            HoTen       = hoTen;
            TenDangNhap = tenDangNhap;
            IsAdmin     = isAdmin;
        }

        public static void DangXuat()
        {
            ID          = 0;
            HoTen       = string.Empty;
            TenDangNhap = string.Empty;
            IsAdmin     = false;
        }
    }
}
