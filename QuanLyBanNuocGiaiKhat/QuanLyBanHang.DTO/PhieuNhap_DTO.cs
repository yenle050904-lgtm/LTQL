using System;

namespace QuanLyBanHang.DTO
{
    public class PhieuNhap_DTO
    {
        public int ID { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
        public int NhanVienID { get; set; }
        public int NhaCungCapID { get; set; }

        // Hiển thị
        public string TenNhanVien { get; set; }
        public string TenNhaCungCap { get; set; }
    }
}
