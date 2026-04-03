using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHang.Data
{
    public class SanPham
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string TenSanPham { get; set; } = string.Empty;

        public int DonGia { get; set; }
        public int SoLuong { get; set; }

        public string HinhAnh { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;

        public int? HangSanXuatID { get; set; }
        [ForeignKey("HangSanXuatID")]
        public virtual HangSanXuat? HangSanXuat { get; set; }

        public int? LoaiSanPhamID { get; set; }
        [ForeignKey("LoaiSanPhamID")]
        public virtual LoaiSanPham? LoaiSanPham { get; set; }

        public virtual ICollection<HoaDon_ChiTiet> HoaDon_ChiTiets { get; set; } = new List<HoaDon_ChiTiet>();
    }
}
