using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHang.Data
{
    public class LoaiSanPham
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string TenLoai { get; set; } = string.Empty;

        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}
