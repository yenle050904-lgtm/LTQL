using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHang.Data
{
    public class HoaDon
    {
        [Key]
        public int ID { get; set; }

        public DateTime NgayLap { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string GhiChu { get; set; } = string.Empty;

        public int? NhanVienID { get; set; }
        [ForeignKey("NhanVienID")]
        public virtual NhanVien? NhanVien { get; set; }

        public int? KhachHangID { get; set; }
        [ForeignKey("KhachHangID")]
        public virtual KhachHang? KhachHang { get; set; }

        public virtual ICollection<HoaDon_ChiTiet> HoaDon_ChiTiets { get; set; } = new List<HoaDon_ChiTiet>();
    }
}
