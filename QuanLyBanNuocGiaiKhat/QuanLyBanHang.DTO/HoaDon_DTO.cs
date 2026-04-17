using System;

namespace QuanLyBanHang.DTO
{
    public class HoaDon_DTO
    {
        public int ID { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public int NhanVienID { get; set; }
        public int? KhachHangID { get; set; }

        // Thuộc tính hiển thị
        public string TenNhanVien { get; set; }
        public string TenKhachHang { get; set; }
    }
}
