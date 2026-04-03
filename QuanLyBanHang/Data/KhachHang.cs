using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHang.Data
{
    public class KhachHang
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string HoVaTen { get; set; } = string.Empty;

        [MaxLength(20)]
        public string DienThoai { get; set; } = string.Empty;

        [MaxLength(500)]
        public string DiaChi { get; set; } = string.Empty;

        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
