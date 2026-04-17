using System;

namespace QuanLyBanHang.DTO
{
    public class KhuyenMai_DTO
    {
        public int ID { get; set; }
        public string MaCode { get; set; }
        public string TenKhuyenMai { get; set; }
        public decimal PhanTramGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public decimal DonToiThieu { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool TrangThai { get; set; }
        public string MoTa { get; set; }
    }
}
