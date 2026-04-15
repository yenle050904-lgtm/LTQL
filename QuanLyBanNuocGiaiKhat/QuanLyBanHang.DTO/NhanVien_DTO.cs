namespace QuanLyBanHang.DTO
{
    public class NhanVien_DTO
    {
        public int ID { get; set; }
        public string HoVaTen { get; set; }
        public string DienThoai { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int QuyenHan { get; set; } // 1 là Admin, 0 là Nhân viên
    }
}