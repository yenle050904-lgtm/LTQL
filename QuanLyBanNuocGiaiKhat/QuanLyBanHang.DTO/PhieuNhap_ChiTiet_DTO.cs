namespace QuanLyBanHang.DTO
{
    public class PhieuNhap_ChiTiet_DTO
    {
        public int ID { get; set; }
        public int PhieuNhapID { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal ThanhTien { get; set; }

        // Hiển thị
        public string TenSanPham { get; set; }
    }
}
