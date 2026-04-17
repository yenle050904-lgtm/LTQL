using System;

namespace QuanLyBanHang.DTO
{
    public class TraHang_DTO
    {
        public int ID { get; set; }
        public DateTime NgayTra { get; set; }
        public int HoaDonID { get; set; }
        public int NhanVienID { get; set; }
        public string LyDo { get; set; }
        public decimal TongTienHoan { get; set; }
    }

    public class TraHang_ChiTiet_DTO
    {
        public int ID { get; set; }
        public int PhieuTraHangID { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuongTra { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        // Hiển thị
        public string TenSanPham { get; set; }
    }
}
