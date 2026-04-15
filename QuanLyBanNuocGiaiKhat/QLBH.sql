-- 1. Tạo Database
CREATE DATABASE QLBHang;
GO

USE QLBHang;
GO

-- =============================================
-- 2. TẠO CÁC BẢNG DANH MỤC ĐỘC LẬP TRƯỚC
-- =============================================

-- Bảng Loại Sản Phẩm
CREATE TABLE LoaiSanPhams (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255)
);

-- Bảng Hãng Sản Xuất
CREATE TABLE HangSanXuats (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenHang NVARCHAR(100) NOT NULL,
    QuocGia NVARCHAR(100)
);
GO

-- Bảng Nhân Viên
CREATE TABLE NhanViens (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoVaTen NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(20),
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL, -- Để độ dài lớn để lưu mã băm BCrypt
    QuyenHan BIT NOT NULL DEFAULT 0 -- 1: Admin, 0: Nhân viên
);
GO

-- Bảng Khách Hàng
CREATE TABLE KhachHangs (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(20),
    DiaChi NVARCHAR(255)
);
GO

-- =============================================
-- 3. TẠO CÁC BẢNG CÓ KHÓA NGOẠI (PHỤ THUỘC)
-- =============================================

-- Bảng Sản Phẩm
CREATE TABLE SanPhams (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenSanPham NVARCHAR(200) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    SoLuongTon INT NOT NULL DEFAULT 0,
    LoaiSanPhamID INT NOT NULL,
    HangSanXuatID INT NOT NULL,
    
    -- Khóa ngoại
    FOREIGN KEY (LoaiSanPhamID) REFERENCES LoaiSanPhams(ID),
    FOREIGN KEY (HangSanXuatID) REFERENCES HangSanXuats(ID)
);
GO

-- Bảng Hóa Đơn
CREATE TABLE HoaDons (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    NhanVienID INT NOT NULL,
    KhachHangID INT NULL, -- Có thể NULL nếu khách vãng lai không lưu thông tin
    
    -- Khóa ngoại
    FOREIGN KEY (NhanVienID) REFERENCES NhanViens(ID),
    FOREIGN KEY (KhachHangID) REFERENCES KhachHangs(ID)
);
GO

-- Bảng Chi Tiết Hóa Đơn
CREATE TABLE HoaDon_ChiTiets (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoaDonID INT NOT NULL,
    SanPhamID INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1,
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    ThanhTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    
    -- Khóa ngoại
    FOREIGN KEY (HoaDonID) REFERENCES HoaDons(ID) ON DELETE CASCADE,
    FOREIGN KEY (SanPhamID) REFERENCES SanPhams(ID)
);
GO

-- Bảng Nhà Cung Cấp
CREATE TABLE NhaCungCaps (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenNhaCungCap NVARCHAR(200) NOT NULL,
    SoDienThoai VARCHAR(20),
    DiaChi NVARCHAR(255),
    GhiChu NVARCHAR(MAX)
);
GO

-- Bảng Phiếu Nhập
CREATE TABLE PhieuNhaps (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NgayNhap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    NhanVienID INT NOT NULL, -- Người lập phiếu nhập
    NhaCungCapID INT NOT NULL, -- Nhập từ nhà cung cấp nào
    
    -- Khóa ngoại
    FOREIGN KEY (NhanVienID) REFERENCES NhanViens(ID),
    FOREIGN KEY (NhaCungCapID) REFERENCES NhaCungCaps(ID)
);
GO

-- Bảng Chi Tiết Phiếu Nhập
CREATE TABLE PhieuNhap_ChiTiets (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    PhieuNhapID INT NOT NULL,
    SanPhamID INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1,
    GiaNhap DECIMAL(18,2) NOT NULL DEFAULT 0, -- Giá vốn lúc nhập vào
    ThanhTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    
    -- Khóa ngoại
    FOREIGN KEY (PhieuNhapID) REFERENCES PhieuNhaps(ID) ON DELETE CASCADE,
    FOREIGN KEY (SanPhamID) REFERENCES SanPhams(ID)
);
GO

-- =============================================
-- THÊM DỮ LIỆU MẪU (theo đúng thứ tự phụ thuộc)
-- =============================================


-- Thêm Loại Sản Phẩm
INSERT INTO LoaiSanPhams (TenLoai, MoTa) VALUES 
(N'Nước ngọt có gas', N'Các loại nước ngọt có gas đóng chai và lon'),
(N'Nước tinh khiết', N'Nước khoáng và nước tinh khiết đóng chai'),
(N'Trà đóng chai', N'Các loại trà xanh, trà ô long đóng chai'),
(N'Nước tăng lực', N'Nước uống tăng lực thể thao');

-- Thêm Hãng Sản Xuất
INSERT INTO HangSanXuats (TenHang, QuocGia) VALUES 
(N'Coca-Cola', N'Mỹ'),
(N'Suntory PepsiCo', N'Mỹ'),
(N'Tân Hiệp Phát', N'Việt Nam'),
(N'Red Bull', N'Thái Lan');

-- Thêm Nhân Viên TRƯỚC (vì PhieuNhaps cần NhanVienID)
-- (Mật khẩu cho cả 2 tài khoản đều là: 123456 - đã mã hóa BCrypt)
INSERT INTO NhanViens (HoVaTen, DienThoai, TenDangNhap, MatKhau, QuyenHan) VALUES 
(N'Quản Trị Viên', '0987654321', 'admin', '$2a$10$.5Elh8fgxypNUWhpUUr/xOa2sZm0VIaE0qWuGGl9otUfobb46T1Pq', 1),
(N'Nhân Viên Bán Hàng', '0912345678', 'nhanvien1', '$2a$10$.5Elh8fgxypNUWhpUUr/xOa2sZm0VIaE0qWuGGl9otUfobb46T1Pq', 0);
UPDATE NhanViens SET MatKhau = '123456' WHERE TenDangNhap IN ('admin', 'nhanvien1');


-- Thêm Sản Phẩm (Giả sử LoaiSanPhamID và HangSanXuatID chạy từ 1)
INSERT INTO SanPhams (TenSanPham, DonGia, SoLuongTon, LoaiSanPhamID, HangSanXuatID) VALUES 
(N'Coca-Cola Vị Nguyên Bản 320ml', 10000, 500, 1, 1),
(N'Pepsi Không Calo 320ml', 10500, 300, 1, 2),
(N'Nước tinh khiết Aquafina 500ml', 7000, 1000, 2, 2),
(N'Trà Xanh Không Độ', 12000, 450, 3, 3),
(N'Nước Tăng Lực Red Bull 250ml', 15000, 200, 4, 4);

-- Thêm Nhà Cung Cấp mẫu
INSERT INTO NhaCungCaps (TenNhaCungCap, SoDienThoai, DiaChi) VALUES 
(N'Đại lý nước giải khát cấp 1 Tân Bình', '02838112233', N'Quận Tân Bình, TP.HCM'),
(N'Công ty TNHH Nước Giải Khát Nam Sài Gòn', '02839998888', N'Quận 7, TP.HCM');

-- Thêm Phiếu Nhập mẫu (NhanVienID=1 đã tồn tại ở trên)
INSERT INTO PhieuNhaps (NgayNhap, TongTien, NhanVienID, NhaCungCapID) VALUES 
('2024-04-25 10:00:00', 4500000, 1, 1);

-- Thêm Chi Tiết Phiếu Nhập mẫu
INSERT INTO PhieuNhap_ChiTiets (PhieuNhapID, SanPhamID, SoLuong, GiaNhap, ThanhTien) VALUES 
(1, 1, 500, 9000, 4500000);

-- Thêm Khách Hàng
INSERT INTO KhachHangs (HoTen, SoDienThoai, DiaChi) VALUES 
(N'Khách vãng lai', NULL, N'Không có địa chỉ'),
(N'Nguyễn Văn An', '0901112233', N'Quận 1, TP.HCM'),
(N'Trần Thị Bích', '0988777666', N'Quận 3, TP.HCM');

-- Thêm Hóa Đơn
INSERT INTO HoaDons (NgayLap, TongTien, NhanVienID, KhachHangID) VALUES 
('2024-05-01 08:30:00', 30000, 2, 1),
('2024-05-02 14:15:00', 150000, 2, 2);

-- Thêm Chi Tiết Hóa Đơn
-- Hóa đơn 1 (Mua 3 lon Coca)
INSERT INTO HoaDon_ChiTiets (HoaDonID, SanPhamID, SoLuong, DonGia, ThanhTien) VALUES 
(1, 1, 3, 10000, 30000);

-- Hóa đơn 2 (Mua 10 lon RedBull)
INSERT INTO HoaDon_ChiTiets (HoaDonID, SanPhamID, SoLuong, DonGia, ThanhTien) VALUES 
(2, 5, 10, 15000, 150000);

SELECT * FROM NhaCungCaps;