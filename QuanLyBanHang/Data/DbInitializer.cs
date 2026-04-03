using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Data
{
    /// <summary>
    /// Khởi tạo dữ liệu mẫu khi database trống.
    /// Gọi DbInitializer.Seed() một lần duy nhất trong Program.cs.
    /// Mật khẩu mặc định cho tất cả nhân viên: 123546
    /// </summary>
    public static class DbInitializer
    {
        public static void Seed()
        {
            using var db = new QLBHDbContext();

            // Đảm bảo database đã được tạo
            db.Database.Migrate();

            // Cập nhật lại toàn bộ mật khẩu hiện có thành 123456 (khắc phục lỗi gõ nhầm trước đó)
            string correctHash = BCrypt.Net.BCrypt.HashPassword("123456");
            var existingUsers = db.NhanViens.ToList();
            if (existingUsers.Any())
            {
                foreach (var user in existingUsers)
                {
                    user.MatKhau = correctHash;
                }
                db.SaveChanges();
                return; // Đã có dữ liệu thì cập nhật mật khẩu xong rồi thoát
            }

            // Mật khẩu chung: 123456 cho luồng tạo mới
            string hashedPassword = correctHash;

            // ==============================
            // 1. LOẠI SẢN PHẨM
            // ==============================
            var loai1 = new LoaiSanPham { TenLoai = "Nước ngọt" };
            var loai2 = new LoaiSanPham { TenLoai = "Nước ép trái cây" };
            var loai3 = new LoaiSanPham { TenLoai = "Trà" };
            var loai4 = new LoaiSanPham { TenLoai = "Cà phê" };
            var loai5 = new LoaiSanPham { TenLoai = "Nước suối" };
            db.LoaiSanPhams.AddRange(loai1, loai2, loai3, loai4, loai5);

            // ==============================
            // 2. HÃNG SẢN XUẤT
            // ==============================
            var hang1 = new HangSanXuat { TenHangSanXuat = "Coca-Cola" };
            var hang2 = new HangSanXuat { TenHangSanXuat = "PepsiCo" };
            var hang3 = new HangSanXuat { TenHangSanXuat = "Tân Hiệp Phát" };
            var hang4 = new HangSanXuat { TenHangSanXuat = "Trung Nguyên" };
            var hang5 = new HangSanXuat { TenHangSanXuat = "La Vie" };
            db.HangSanXuats.AddRange(hang1, hang2, hang3, hang4, hang5);

            db.SaveChanges(); // Lấy ID cho FK

            // ==============================
            // 3. SẢN PHẨM (15 sản phẩm mẫu)
            // ==============================
            db.SanPhams.AddRange(
                new SanPham { TenSanPham = "Coca-Cola 330ml", DonGia = 10000, SoLuong = 200, LoaiSanPhamID = loai1.ID, HangSanXuatID = hang1.ID },
                new SanPham { TenSanPham = "Coca-Cola Zero 330ml", DonGia = 12000, SoLuong = 150, LoaiSanPhamID = loai1.ID, HangSanXuatID = hang1.ID },
                new SanPham { TenSanPham = "Sprite 330ml", DonGia = 10000, SoLuong = 180, LoaiSanPhamID = loai1.ID, HangSanXuatID = hang1.ID },
                new SanPham { TenSanPham = "Pepsi 330ml", DonGia = 10000, SoLuong = 200, LoaiSanPhamID = loai1.ID, HangSanXuatID = hang2.ID },
                new SanPham { TenSanPham = "7Up 330ml", DonGia = 10000, SoLuong = 160, LoaiSanPhamID = loai1.ID, HangSanXuatID = hang2.ID },
                new SanPham { TenSanPham = "Nước ép cam Teppy 327ml", DonGia = 15000, SoLuong = 100, LoaiSanPhamID = loai2.ID, HangSanXuatID = hang1.ID },
                new SanPham { TenSanPham = "Nước ép táo Tropicana 350ml", DonGia = 18000, SoLuong = 80, LoaiSanPhamID = loai2.ID, HangSanXuatID = hang2.ID },
                new SanPham { TenSanPham = "Trà xanh Không Độ 500ml", DonGia = 10000, SoLuong = 250, LoaiSanPhamID = loai3.ID, HangSanXuatID = hang3.ID },
                new SanPham { TenSanPham = "Trà đào Dr Thanh 500ml", DonGia = 10000, SoLuong = 200, LoaiSanPhamID = loai3.ID, HangSanXuatID = hang3.ID },
                new SanPham { TenSanPham = "Trà sữa Macchiato 250ml", DonGia = 20000, SoLuong = 120, LoaiSanPhamID = loai3.ID, HangSanXuatID = hang3.ID },
                new SanPham { TenSanPham = "Cà phê G7 3in1 (hộp 20 gói)", DonGia = 55000, SoLuong = 60, LoaiSanPhamID = loai4.ID, HangSanXuatID = hang4.ID },
                new SanPham { TenSanPham = "Cà phê lon Birdy 250ml", DonGia = 12000, SoLuong = 140, LoaiSanPhamID = loai4.ID, HangSanXuatID = hang4.ID },
                new SanPham { TenSanPham = "Nước suối La Vie 500ml", DonGia = 5000, SoLuong = 500, LoaiSanPhamID = loai5.ID, HangSanXuatID = hang5.ID },
                new SanPham { TenSanPham = "Nước suối La Vie 1.5L", DonGia = 10000, SoLuong = 300, LoaiSanPhamID = loai5.ID, HangSanXuatID = hang5.ID },
                new SanPham { TenSanPham = "Nước khoáng Vĩnh Hảo 500ml", DonGia = 7000, SoLuong = 200, LoaiSanPhamID = loai5.ID, HangSanXuatID = hang5.ID }
            );

            // ==============================
            // 4. NHÂN VIÊN (Admin + User)
            // ==============================
            db.NhanViens.AddRange(
                new NhanVien
                {
                    HoVaTen = "Nguyễn Văn Admin",
                    DienThoai = "0901234567",
                    TenDangNhap = "admin",
                    MatKhau = hashedPassword, // 123546
                    QuyenHan = true           // Admin
                },
                new NhanVien
                {
                    HoVaTen = "Trần Thị Nhân Viên",
                    DienThoai = "0912345678",
                    TenDangNhap = "user",
                    MatKhau = hashedPassword, // 123546
                    QuyenHan = false          // User
                },
                new NhanVien
                {
                    HoVaTen = "Lê Văn Bán Hàng",
                    DienThoai = "0923456789",
                    TenDangNhap = "banhang",
                    MatKhau = hashedPassword, // 123546
                    QuyenHan = false          // User
                }
            );

            // ==============================
            // 5. KHÁCH HÀNG
            // ==============================
            db.KhachHangs.AddRange(
                new KhachHang { HoVaTen = "Phạm Minh Tuấn", DienThoai = "0987654321", DiaChi = "123 Nguyễn Huệ, Q1, TP.HCM" },
                new KhachHang { HoVaTen = "Ngô Thanh Hà", DienThoai = "0976543210", DiaChi = "456 Lê Lai, Q5, TP.HCM" },
                new KhachHang { HoVaTen = "Võ Hoàng Dũng", DienThoai = "0965432109", DiaChi = "789 Trần Hưng Đạo, Q10, TP.HCM" },
                new KhachHang { HoVaTen = "Đặng Thu Thảo", DienThoai = "0954321098", DiaChi = "12 Hai Bà Trưng, Q3, TP.HCM" },
                new KhachHang { HoVaTen = "Khách Lẻ", DienThoai = "", DiaChi = "" }
            );

            db.SaveChanges();
        }
    }
}
