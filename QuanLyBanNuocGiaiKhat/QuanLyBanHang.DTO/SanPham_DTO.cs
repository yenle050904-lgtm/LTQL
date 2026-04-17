namespace QuanLyBanHang.DTO
{
    public class SanPham_DTO
    {
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
        public int LoaiSanPhamID { get; set; }
        public int HangSanXuatID { get; set; }

        // Thuộc tính hiển thị (không có trong bảng, join ra cho tiện)
        public string TenLoai { get; set; }
        public string TenHang { get; set; }
    }
}
