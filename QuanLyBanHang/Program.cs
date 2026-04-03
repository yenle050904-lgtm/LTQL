namespace QuanLyBanHang
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi đầu của ứng dụng.
        /// Hiển thị frmLogin trước, thành công mới mở frmMain.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Seed dữ liệu mẫu khi database trống (chạy 1 lần)
            Data.DbInitializer.Seed();

            // Hiển thị Login Dialog trước
            using (var loginForm = new Forms.Systems.frmLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Đăng nhập thành công → mở hệ thống chính
                    Application.Run(new Forms.Systems.frmMain());
                }
                // Nếu Cancel → thoát ứng dụng
            }
        }
    }
}