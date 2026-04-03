using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanHang.Data
{
    public class NhanVien
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string HoVaTen { get; set; } = string.Empty;

        [MaxLength(20)]
        public string DienThoai { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string MatKhau { get; set; } = string.Empty; // uses BCrypt

        public bool QuyenHan { get; set; } // true = Admin, false = User

        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
