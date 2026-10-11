USE QuanLyNguoiDung;
GO

CREATE TABLE dbo.Users
(
    UserId INT IDENTITY(1,1)
        CONSTRAINT PK_Users PRIMARY KEY,

    TenDangNhap NVARCHAR(50) NOT NULL,

    MatKhauBam VARBINARY(32) NOT NULL,
    Salt VARBINARY(16) NOT NULL,

    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(254) NULL,

    NgayTao DATETIME2 NOT NULL
        CONSTRAINT DF_Users_NgayTao
        DEFAULT SYSUTCDATETIME(),

    LanDangNhapCuoi DATETIME2 NULL,

    CONSTRAINT UQ_Users_TenDangNhap
        UNIQUE (TenDangNhap)
);
GO