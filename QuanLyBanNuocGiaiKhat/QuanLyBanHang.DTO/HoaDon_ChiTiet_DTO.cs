namespace QuanLyBanHang.DTO
{
    public class HoaDon_ChiTiet_DTO
    {
        public int ID { get; set; }
        public int HoaDonID { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        // Hiển thị
        public string TenSanPham { get; set; }
    }
}
