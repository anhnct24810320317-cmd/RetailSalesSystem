
IF DB_ID('SalesDB') IS NULL
    CREATE DATABASE SalesDB;
GO

USE SalesDB;
GO

-- =============================================
-- 2. TAO BANG NHAN VIEN
-- =============================================
CREATE TABLE NhanVien
(
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE,
    ChucVu NVARCHAR(50)
);
GO

-- =============================================
-- 3. TAO BANG KHACH HANG
-- =============================================
CREATE TABLE KhachHang
(
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) UNIQUE,
    DiemTichLuy INT DEFAULT 0
);
GO

-- =============================================
-- 4. TAO BANG SAN PHAM
-- =============================================
CREATE TABLE SanPham
(
    MaSP VARCHAR(20) PRIMARY KEY,
    TenSP NVARCHAR(150) NOT NULL,
    GiaBan DECIMAL(18,2) CHECK (GiaBan > 0),
    SoLuongTon INT CHECK (SoLuongTon >= 0)
);
GO

-- =============================================
-- 5. TAO BANG HOA DON
-- =============================================
CREATE TABLE HoaDon
(
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    MaNV INT NOT NULL,
    MaKH INT NULL,
    TongTien DECIMAL(18,2),

    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);
GO

-- =============================================
-- 6. TAO BANG CHI TIET HOA DON
-- =============================================
CREATE TABLE ChiTietHoaDon
(
    MaHD INT,
    MaSP VARCHAR(20),
    SoLuong INT CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2),

    PRIMARY KEY (MaHD, MaSP),

    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
GO

-- =============================================
-- 7. THEM DU LIEU NHAN VIEN
-- =============================================
INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu)
VALUES
(N'Nguyen Van An', '0911111111', N'Thu ngan'),
(N'Tran Thi Binh', '0922222222', N'Quan ly');
GO

-- =============================================
-- 8. THEM DU LIEU KHACH HANG
-- =============================================
INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy)
VALUES
(N'Nguyen Van Nam', '0933333333', 100),
(N'Tran Thi Hoa', '0944444444', 50);
GO

-- =============================================
-- 9. THEM DU LIEU SAN PHAM
-- =============================================
INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon)
VALUES
('SP001', N'Banh quy', 25000, 100),
('SP002', N'Sua tuoi', 12000, 200),
('SP003', N'Mi tom', 5000, 300),
('SP004', N'Nuoc ngot', 10000, 150),
('SP005', N'Dau an', 45000, 50);
GO

-- =============================================
-- 10. TIM KIEM SAN PHAM
-- =============================================
SELECT *
FROM SanPham
WHERE MaSP LIKE '%SP001%'
   OR TenSP LIKE N'%sua%';
GO

-- =============================================
-- 11. TINH DOANH THU TRONG NGAY
-- =============================================
SELECT ISNULL(SUM(TongTien), 0) AS DoanhThuNgay
FROM HoaDon
WHERE CAST(NgayLap AS DATE) = CAST(GETDATE() AS DATE);
GO

-- =============================================
-- 12. TINH DOANH THU TRONG THANG
-- =============================================
SELECT ISNULL(SUM(TongTien), 0) AS DoanhThuThang
FROM HoaDon
WHERE MONTH(NgayLap) = MONTH(GETDATE())
  AND YEAR(NgayLap) = YEAR(GETDATE());
GO

-- =============================================
-- 13. TOP 5 SAN PHAM BAN CHAY
-- =============================================
SELECT TOP 5
    SP.MaSP,
    SP.TenSP,
    SUM(CT.SoLuong) AS TongSoLuongBan
FROM SanPham SP
JOIN ChiTietHoaDon CT ON SP.MaSP = CT.MaSP
GROUP BY SP.MaSP, SP.TenSP
ORDER BY SUM(CT.SoLuong) DESC;
GO

-- =============================================
-- 14. KIEM TRA TON KHO
-- =============================================
SELECT MaSP, TenSP, GiaBan, SoLuongTon
FROM SanPham
ORDER BY SoLuongTon ASC;
GO

-- =============================================
-- 15. KIEM TRA CAC BANG
-- =============================================
SELECT * FROM NhanVien;
SELECT * FROM KhachHang;
SELECT * FROM SanPham;
SELECT * FROM HoaDon;
SELECT * FROM ChiTietHoaDon;
GO
