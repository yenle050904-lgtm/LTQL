using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace QuanLyBanHang.Data
{
    public class QLBHDbContext : DbContext
    {
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<HangSanXuat> HangSanXuats { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoaDon_ChiTiet> HoaDon_ChiTiets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["QLBH_Connection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    // Fallback to default if not found
                    connectionString = @"Server=.\SQLEXPRESS;Database=QuanLyBanNuocGiaiKhatDB;Trusted_Connection=True;TrustServerCertificate=True;";
                }
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
