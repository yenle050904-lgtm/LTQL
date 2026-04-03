using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanHang.Data
{
    public class HoaDon_ChiTiet
    {
        [Key]
        public int ID { get; set; }

        public int HoaDonID { get; set; }
        [ForeignKey("HoaDonID")]
        public virtual HoaDon HoaDon { get; set; } = null!;

        public int SanPhamID { get; set; }
        [ForeignKey("SanPhamID")]
        public virtual SanPham SanPham { get; set; } = null!;

        public short SoLuongBan { get; set; }
        public int DonGiaBan { get; set; }
    }
}
