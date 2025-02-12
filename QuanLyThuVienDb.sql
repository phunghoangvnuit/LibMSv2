USE [QuanLyThuViens2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTPhieuMuon]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTPhieuMuon](
	[ID_PhieuMuon] [int] NOT NULL,
	[ID_Sach] [int] NOT NULL,
	[TrangThai] [int] NOT NULL,
	[SoLuongMuon] [int] NOT NULL,
	[NgayTra] [datetime2](7) NULL,
	[GhiChuTra] [nvarchar](max) NOT NULL,
	[TongTienThue] [real] NOT NULL,
 CONSTRAINT [PK_CTPhieuMuon] PRIMARY KEY CLUSTERED 
(
	[ID_PhieuMuon] ASC,
	[ID_Sach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocGias]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocGias](
	[ID_DocGia] [int] IDENTITY(1,1) NOT NULL,
	[TenDocGia] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](max) NOT NULL,
	[NgaySinh] [datetime2](7) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[SDT] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[MaSV] [nvarchar](450) NOT NULL,
	[ID_TaiKhoan] [int] NOT NULL,
 CONSTRAINT [PK_DocGias] PRIMARY KEY CLUSTERED 
(
	[ID_DocGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LSTraSach]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LSTraSach](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ID_PhieuMuon] [int] NOT NULL,
	[ID_Sach] [int] NOT NULL,
	[TrangThai] [int] NOT NULL,
	[SoLuongTra] [int] NOT NULL,
	[NgayTra] [datetime2](7) NULL,
	[GhiChuTra] [nvarchar](max) NOT NULL,
	[SoTienPhat] [float] NOT NULL,
	[GhiChuPhat] [nvarchar](max) NOT NULL,
	[MucDo] [int] NULL,
	[TongTienThue] [float] NOT NULL,
 CONSTRAINT [PK_LSTraSach] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuMuons]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuMuons](
	[ID_PhieuMuon] [int] IDENTITY(1,1) NOT NULL,
	[NgayTaoPhieu] [datetime2](7) NOT NULL,
	[NgayHenTra] [datetime2](7) NOT NULL,
	[ID_ThuThu] [int] NULL,
	[ID_The] [int] NOT NULL,
	[GhiChuMuon] [nvarchar](max) NOT NULL,
	[HinhThucMuon] [int] NOT NULL,
 CONSTRAINT [PK_PhieuMuons] PRIMARY KEY CLUSTERED 
(
	[ID_PhieuMuon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Saches]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Saches](
	[ID_Sach] [int] IDENTITY(1,1) NOT NULL,
	[TenSach] [nvarchar](max) NOT NULL,
	[GiaTien] [float] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[UrlImg] [nvarchar](max) NOT NULL,
	[NgayNhap] [datetime2](7) NOT NULL,
	[ID_TheLoai] [int] NOT NULL,
	[ID_TacGia] [int] NOT NULL,
	[GiaThueTheoNgay] [float] NOT NULL,
 CONSTRAINT [PK_Saches] PRIMARY KEY CLUSTERED 
(
	[ID_Sach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TacGias]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TacGias](
	[ID_TacGia] [int] IDENTITY(1,1) NOT NULL,
	[TenTacGia] [nvarchar](max) NOT NULL,
	[QuocGia] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TacGias] PRIMARY KEY CLUSTERED 
(
	[ID_TacGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoans]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoans](
	[ID_TaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[TenDangNhap] [nvarchar](450) NOT NULL,
	[MatKhau] [nvarchar](max) NOT NULL,
	[VaiTro] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TaiKhoans] PRIMARY KEY CLUSTERED 
(
	[ID_TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheLoais]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheLoais](
	[ID_TheLoai] [int] IDENTITY(1,1) NOT NULL,
	[TenTheLoai] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_TheLoais] PRIMARY KEY CLUSTERED 
(
	[ID_TheLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheThuViens]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheThuViens](
	[ID_The] [int] IDENTITY(1,1) NOT NULL,
	[NgayBD] [datetime2](7) NOT NULL,
	[NgayHetHan] [datetime2](7) NOT NULL,
	[ID_DocGia] [int] NOT NULL,
	[SoThe] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TheThuViens] PRIMARY KEY CLUSTERED 
(
	[ID_The] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThuThus]    Script Date: 7/17/2024 01:40:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuThus](
	[ID_ThuThu] [int] IDENTITY(1,1) NOT NULL,
	[TenThuThu] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](max) NOT NULL,
	[NgaySinh] [datetime2](7) NOT NULL,
	[SDT] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[ID_TaiKhoan] [int] NOT NULL,
 CONSTRAINT [PK_ThuThus] PRIMARY KEY CLUSTERED 
(
	[ID_ThuThu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240627055124_Init', N'7.0.0-preview.7.22376.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240627073234_Add_Lich_Su_Tra_Sach', N'7.0.0-preview.7.22376.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240627180242_Add_MucDo', N'7.0.0-preview.7.22376.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240716063344_ThemGiaThue', N'7.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240716070507_SuaHinhThucMuon', N'7.0.20')
GO
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (17, 2, 1, 3, CAST(N'2024-06-28T15:04:43.7353690' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (18, 2, 1, 5, CAST(N'2024-06-28T15:14:00.4410430' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (21, 4, 1, 12, CAST(N'2024-06-28T15:25:35.5025606' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (22, 5, 1, 6, CAST(N'2024-06-28T15:26:12.4920761' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (23, 6, 1, 5, CAST(N'2024-06-28T15:27:39.5766009' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (24, 4, 1, 5, CAST(N'2024-06-28T15:29:50.4000650' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (26, 4, 1, 10, CAST(N'2024-06-28T21:17:40.6553264' AS DateTime2), N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (29, 5, 1, 5, CAST(N'2024-07-16T16:28:02.4209954' AS DateTime2), N'Trống', 2000)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (30, 6, 0, 4, NULL, N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (32, 2, 0, 2, NULL, N'Trống', 0)
INSERT [dbo].[CTPhieuMuon] ([ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongMuon], [NgayTra], [GhiChuTra], [TongTienThue]) VALUES (33, 4, 0, 1, NULL, N'Trống', 0)
GO
SET IDENTITY_INSERT [dbo].[DocGias] ON 

INSERT [dbo].[DocGias] ([ID_DocGia], [TenDocGia], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [MaSV], [ID_TaiKhoan]) VALUES (1, N'Hoàng', N'Nam', CAST(N'2024-07-03T00:00:00.0000000' AS DateTime2), N'Hà Nội', N'0979777999', N'hoang@gmail.com', N'sv001', 2)
INSERT [dbo].[DocGias] ([ID_DocGia], [TenDocGia], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [MaSV], [ID_TaiKhoan]) VALUES (2, N'Nguyễn Vinh', N'Nam', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), N'hcm vn q9', N'0987654321', N'vinh@gmail.com', N'225050387', 4)
INSERT [dbo].[DocGias] ([ID_DocGia], [TenDocGia], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [MaSV], [ID_TaiKhoan]) VALUES (3, N'Nguyễn Ái  Vân', N'Nam', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), N'hcm vn q9', N'0987654321', N'van@gmail.com', N'236757649', 5)
INSERT [dbo].[DocGias] ([ID_DocGia], [TenDocGia], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [MaSV], [ID_TaiKhoan]) VALUES (4, N'Trần Kim', N'Nam', CAST(N'2024-06-07T00:00:00.0000000' AS DateTime2), N'hcm vn q9', N'0987654321', N'bb@gmail.com', N'293030828', 6)
INSERT [dbo].[DocGias] ([ID_DocGia], [TenDocGia], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [MaSV], [ID_TaiKhoan]) VALUES (5, N'Demo doc gia', N'Nam', CAST(N'2024-06-21T00:00:00.0000000' AS DateTime2), N'hcm vn q9', N'0987654321', N'bb@gmail.com', N'205858584', 6)
SET IDENTITY_INSERT [dbo].[DocGias] OFF
GO
SET IDENTITY_INSERT [dbo].[LSTraSach] ON 

INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (15, 17, 2, 1, 3, CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), N'Bình thường', 0, N'', NULL, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (20, 18, 2, 3, 5, CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), N'Sách mất trang', 180000, N'Phạt 45% giá trị sách', 3, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (21, 21, 4, 2, 5, CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), N'Sách bị rách', 40000, N'Phạt 10% giá trị sách', 1, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (22, 21, 4, 1, 7, CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), N'Bình thường', 0, N'', NULL, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (23, 22, 5, 3, 6, CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), N'Sách mất trang', 144000, N'Phạt 30% giá trị sách', 2, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (24, 23, 6, 1, 5, CAST(N'2024-06-27T00:00:00.0000000' AS DateTime2), N'Bình thường', 0, N'', NULL, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (25, 24, 4, 1, 5, CAST(N'2024-06-26T00:00:00.0000000' AS DateTime2), N'Bình thường', 0, N'', NULL, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (26, 26, 4, 1, 10, CAST(N'2024-07-02T00:00:00.0000000' AS DateTime2), N'Quá hạn trả (20000) + ', 200000, N'', NULL, 0)
INSERT [dbo].[LSTraSach] ([Id], [ID_PhieuMuon], [ID_Sach], [TrangThai], [SoLuongTra], [NgayTra], [GhiChuTra], [SoTienPhat], [GhiChuPhat], [MucDo], [TongTienThue]) VALUES (27, 29, 5, 1, 5, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), N'Quá hạn trả (20000) + ', 100000, N'', NULL, 2000)
SET IDENTITY_INSERT [dbo].[LSTraSach] OFF
GO
SET IDENTITY_INSERT [dbo].[PhieuMuons] ON 

INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (3, CAST(N'2024-06-27T13:40:33.1283956' AS DateTime2), CAST(N'2024-06-19T00:00:00.0000000' AS DateTime2), 1, 1, N'1', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (4, CAST(N'2024-06-27T13:40:39.7027619' AS DateTime2), CAST(N'2024-06-19T00:00:00.0000000' AS DateTime2), 1, 1, N'1', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (5, CAST(N'2024-06-27T13:49:25.5695372' AS DateTime2), CAST(N'2024-06-25T00:00:00.0000000' AS DateTime2), 1, 1, N'1', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (6, CAST(N'2024-06-27T13:50:07.8581448' AS DateTime2), CAST(N'2024-07-03T00:00:00.0000000' AS DateTime2), 1, 1, N'1', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (9, CAST(N'2024-06-27T23:18:57.4835335' AS DateTime2), CAST(N'2024-07-03T00:00:00.0000000' AS DateTime2), 1, 1, N'1', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (11, CAST(N'2024-06-27T23:42:39.8744013' AS DateTime2), CAST(N'2024-06-27T00:00:00.0000000' AS DateTime2), 1, 1, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (12, CAST(N'2024-06-27T23:59:53.5175729' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 1, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (13, CAST(N'2024-06-28T00:15:12.1166405' AS DateTime2), CAST(N'2024-06-19T00:00:00.0000000' AS DateTime2), 1, 1, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (14, CAST(N'2024-06-28T00:33:52.8663498' AS DateTime2), CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), 1, 1, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (15, CAST(N'2024-06-28T01:19:21.9885476' AS DateTime2), CAST(N'2024-06-18T00:00:00.0000000' AS DateTime2), 1, 1, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (16, CAST(N'2024-06-28T01:23:12.0249128' AS DateTime2), CAST(N'2024-06-20T00:00:00.0000000' AS DateTime2), 1, 1, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (17, CAST(N'2024-06-28T15:04:18.9970127' AS DateTime2), CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), 1, 2, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (18, CAST(N'2024-06-28T15:06:16.9346068' AS DateTime2), CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), 1, 1, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (19, CAST(N'2024-06-28T15:07:29.9828018' AS DateTime2), CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), 1, 2, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (20, CAST(N'2024-06-28T15:10:02.3385431' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 1, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (21, CAST(N'2024-06-28T15:23:37.7600085' AS DateTime2), CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), 1, 3, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (22, CAST(N'2024-06-28T15:25:59.1889379' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (23, CAST(N'2024-06-28T15:27:25.5008460' AS DateTime2), CAST(N'2024-06-27T00:00:00.0000000' AS DateTime2), 1, 3, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (24, CAST(N'2024-06-28T15:29:36.9191983' AS DateTime2), CAST(N'2024-06-26T00:00:00.0000000' AS DateTime2), 1, 2, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (26, CAST(N'2024-06-28T21:14:27.9013872' AS DateTime2), CAST(N'2024-06-29T00:00:00.0000000' AS DateTime2), 1, 3, N'ok', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (29, CAST(N'2024-07-16T16:18:26.9062201' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (30, CAST(N'2024-07-17T01:38:16.2965398' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 3, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (32, CAST(N'2024-06-28T21:42:34.2246536' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'dfdf', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (33, CAST(N'2024-07-11T12:06:43.6992111' AS DateTime2), CAST(N'2024-07-22T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (39, CAST(N'2024-07-16T14:27:14.1857668' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 0)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (40, CAST(N'2024-07-16T14:29:21.4407307' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (41, CAST(N'2024-07-16T15:36:24.9385844' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (42, CAST(N'2024-07-16T15:44:05.3361135' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (43, CAST(N'2024-07-16T15:44:23.0324429' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (44, CAST(N'2024-07-16T15:50:21.3673305' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (45, CAST(N'2024-07-16T15:55:26.6499949' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (46, CAST(N'2024-07-16T15:56:25.3927734' AS DateTime2), CAST(N'2024-07-22T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
INSERT [dbo].[PhieuMuons] ([ID_PhieuMuon], [NgayTaoPhieu], [NgayHenTra], [ID_ThuThu], [ID_The], [GhiChuMuon], [HinhThucMuon]) VALUES (47, CAST(N'2024-07-16T15:58:00.0058513' AS DateTime2), CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 4, N'Trống', 2)
SET IDENTITY_INSERT [dbo].[PhieuMuons] OFF
GO
SET IDENTITY_INSERT [dbo].[Saches] ON 

INSERT [dbo].[Saches] ([ID_Sach], [TenSach], [GiaTien], [SoLuong], [UrlImg], [NgayNhap], [ID_TheLoai], [ID_TacGia], [GiaThueTheoNgay]) VALUES (2, N'SÁCH GIÁO KHOA', 80000, 102, N'https://intranphu.vn/wp-content/uploads/2016/04/SGK_final.jpg', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 1, 2, 5000)
INSERT [dbo].[Saches] ([ID_Sach], [TenSach], [GiaTien], [SoLuong], [UrlImg], [NgayNhap], [ID_TheLoai], [ID_TacGia], [GiaThueTheoNgay]) VALUES (3, N'DEMO QUA 2 NĂM', 80000, 99, N'https://intranphu.vn/wp-content/uploads/2016/04/SGK_final.jpg', CAST(N'2021-06-28T00:00:00.0000000' AS DateTime2), 1, 3, 8000)
INSERT [dbo].[Saches] ([ID_Sach], [TenSach], [GiaTien], [SoLuong], [UrlImg], [NgayNhap], [ID_TheLoai], [ID_TacGia], [GiaThueTheoNgay]) VALUES (4, N'CHÍ PHÈO', 80000, 125, N'https://product.hstatic.net/200000017360/product/chi-pheo_72e3f1370e484cf49b0fc94ee4ce0f80_master.jpg', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 2, 4, 9000)
INSERT [dbo].[Saches] ([ID_Sach], [TenSach], [GiaTien], [SoLuong], [UrlImg], [NgayNhap], [ID_TheLoai], [ID_TacGia], [GiaThueTheoNgay]) VALUES (5, N'NỖI BUỒN CHIẾN TRANH', 80000, 105, N'https://product.hstatic.net/200000017360/product/chi-pheo_72e3f1370e484cf49b0fc94ee4ce0f80_master.jpg', CAST(N'2024-06-27T00:00:00.0000000' AS DateTime2), 1, 5, 2000)
INSERT [dbo].[Saches] ([ID_Sach], [TenSach], [GiaTien], [SoLuong], [UrlImg], [NgayNhap], [ID_TheLoai], [ID_TacGia], [GiaThueTheoNgay]) VALUES (6, N'DEMO SACH NEW', 80000, 200, N'https://product.hstatic.net/200000017360/product/chi-pheo_72e3f1370e484cf49b0fc94ee4ce0f80_master.jpg', CAST(N'2024-06-28T00:00:00.0000000' AS DateTime2), 2, 5, 10000)
SET IDENTITY_INSERT [dbo].[Saches] OFF
GO
SET IDENTITY_INSERT [dbo].[TacGias] ON 

INSERT [dbo].[TacGias] ([ID_TacGia], [TenTacGia], [QuocGia]) VALUES (1, N'Test', N'Test')
INSERT [dbo].[TacGias] ([ID_TacGia], [TenTacGia], [QuocGia]) VALUES (2, N'Nguyen Van A', N'Viet Nam')
INSERT [dbo].[TacGias] ([ID_TacGia], [TenTacGia], [QuocGia]) VALUES (3, N'Nguyen Van B', N'Viet Nam')
INSERT [dbo].[TacGias] ([ID_TacGia], [TenTacGia], [QuocGia]) VALUES (4, N'Nam Cao', N'Viet Nam')
INSERT [dbo].[TacGias] ([ID_TacGia], [TenTacGia], [QuocGia]) VALUES (5, N'Bảo Ninh', N'Viet Nam')
INSERT [dbo].[TacGias] ([ID_TacGia], [TenTacGia], [QuocGia]) VALUES (6, N'test', N'tét')
SET IDENTITY_INSERT [dbo].[TacGias] OFF
GO
SET IDENTITY_INSERT [dbo].[TaiKhoans] ON 

INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (1, N'admin', N'admin', N'Admin')
INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (2, N'hoang@gmail.com', N'123456', N'Độc giả')
INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (3, N'thuthu', N'thuthu', N'Thủ thư')
INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (4, N'vinh@gmail.com', N'123456', N'Độc giả')
INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (5, N'van@gmail.com', N'123456', N'Độc giả')
INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (6, N'bb@gmail.com', N'123456', N'Độc giả')
INSERT [dbo].[TaiKhoans] ([ID_TaiKhoan], [TenDangNhap], [MatKhau], [VaiTro]) VALUES (7, N'bb@gmail.com', N'123456', N'Độc giả')
SET IDENTITY_INSERT [dbo].[TaiKhoans] OFF
GO
SET IDENTITY_INSERT [dbo].[TheLoais] ON 

INSERT [dbo].[TheLoais] ([ID_TheLoai], [TenTheLoai]) VALUES (1, N'Sách Giáo Khoa')
INSERT [dbo].[TheLoais] ([ID_TheLoai], [TenTheLoai]) VALUES (2, N'Sách Tham Khảo')
INSERT [dbo].[TheLoais] ([ID_TheLoai], [TenTheLoai]) VALUES (4, N'test')
INSERT [dbo].[TheLoais] ([ID_TheLoai], [TenTheLoai]) VALUES (5, N'abc')
SET IDENTITY_INSERT [dbo].[TheLoais] OFF
GO
SET IDENTITY_INSERT [dbo].[TheThuViens] ON 

INSERT [dbo].[TheThuViens] ([ID_The], [NgayBD], [NgayHetHan], [ID_DocGia], [SoThe]) VALUES (1, CAST(N'2024-06-27T12:55:22.2022293' AS DateTime2), CAST(N'2027-06-27T00:00:00.0000000' AS DateTime2), 1, N'sv001')
INSERT [dbo].[TheThuViens] ([ID_The], [NgayBD], [NgayHetHan], [ID_DocGia], [SoThe]) VALUES (2, CAST(N'2024-06-28T15:01:44.6161820' AS DateTime2), CAST(N'2027-06-28T00:00:00.0000000' AS DateTime2), 2, N'225050387')
INSERT [dbo].[TheThuViens] ([ID_The], [NgayBD], [NgayHetHan], [ID_DocGia], [SoThe]) VALUES (3, CAST(N'2024-06-28T15:18:54.5920414' AS DateTime2), CAST(N'2027-06-28T00:00:00.0000000' AS DateTime2), 3, N'236757649')
INSERT [dbo].[TheThuViens] ([ID_The], [NgayBD], [NgayHetHan], [ID_DocGia], [SoThe]) VALUES (4, CAST(N'2024-06-28T15:19:17.7003390' AS DateTime2), CAST(N'2027-06-28T00:00:00.0000000' AS DateTime2), 4, N'293030828')
INSERT [dbo].[TheThuViens] ([ID_The], [NgayBD], [NgayHetHan], [ID_DocGia], [SoThe]) VALUES (5, CAST(N'2024-06-28T15:22:21.1865604' AS DateTime2), CAST(N'2027-06-28T00:00:00.0000000' AS DateTime2), 5, N'205858584')
SET IDENTITY_INSERT [dbo].[TheThuViens] OFF
GO
SET IDENTITY_INSERT [dbo].[ThuThus] ON 

INSERT [dbo].[ThuThus] ([ID_ThuThu], [TenThuThu], [GioiTinh], [NgaySinh], [SDT], [Email], [ID_TaiKhoan]) VALUES (1, N'Hoàng', N'Nam', CAST(N'1997-01-01T00:00:00.0000000' AS DateTime2), N'029384384', N'hoang@gmail.com', 3)
SET IDENTITY_INSERT [dbo].[ThuThus] OFF
GO
ALTER TABLE [dbo].[CTPhieuMuon] ADD  DEFAULT (CONVERT([real],(0))) FOR [TongTienThue]
GO
ALTER TABLE [dbo].[LSTraSach] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [TongTienThue]
GO
ALTER TABLE [dbo].[PhieuMuons] ADD  DEFAULT ((0)) FOR [HinhThucMuon]
GO
ALTER TABLE [dbo].[Saches] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [GiaThueTheoNgay]
GO
ALTER TABLE [dbo].[CTPhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_CTPhieuMuon_PhieuMuons_ID_PhieuMuon] FOREIGN KEY([ID_PhieuMuon])
REFERENCES [dbo].[PhieuMuons] ([ID_PhieuMuon])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CTPhieuMuon] CHECK CONSTRAINT [FK_CTPhieuMuon_PhieuMuons_ID_PhieuMuon]
GO
ALTER TABLE [dbo].[CTPhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_CTPhieuMuon_Saches_ID_Sach] FOREIGN KEY([ID_Sach])
REFERENCES [dbo].[Saches] ([ID_Sach])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CTPhieuMuon] CHECK CONSTRAINT [FK_CTPhieuMuon_Saches_ID_Sach]
GO
ALTER TABLE [dbo].[DocGias]  WITH CHECK ADD  CONSTRAINT [FK_DocGias_TaiKhoans_ID_TaiKhoan] FOREIGN KEY([ID_TaiKhoan])
REFERENCES [dbo].[TaiKhoans] ([ID_TaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocGias] CHECK CONSTRAINT [FK_DocGias_TaiKhoans_ID_TaiKhoan]
GO
ALTER TABLE [dbo].[LSTraSach]  WITH CHECK ADD  CONSTRAINT [FK_LSTraSach_PhieuMuons_ID_PhieuMuon] FOREIGN KEY([ID_PhieuMuon])
REFERENCES [dbo].[PhieuMuons] ([ID_PhieuMuon])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LSTraSach] CHECK CONSTRAINT [FK_LSTraSach_PhieuMuons_ID_PhieuMuon]
GO
ALTER TABLE [dbo].[LSTraSach]  WITH CHECK ADD  CONSTRAINT [FK_LSTraSach_Saches_ID_Sach] FOREIGN KEY([ID_Sach])
REFERENCES [dbo].[Saches] ([ID_Sach])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LSTraSach] CHECK CONSTRAINT [FK_LSTraSach_Saches_ID_Sach]
GO
ALTER TABLE [dbo].[PhieuMuons]  WITH CHECK ADD  CONSTRAINT [FK_PhieuMuons_TheThuViens_ID_The] FOREIGN KEY([ID_The])
REFERENCES [dbo].[TheThuViens] ([ID_The])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuMuons] CHECK CONSTRAINT [FK_PhieuMuons_TheThuViens_ID_The]
GO
ALTER TABLE [dbo].[PhieuMuons]  WITH CHECK ADD  CONSTRAINT [FK_PhieuMuons_ThuThus_ID_ThuThu] FOREIGN KEY([ID_ThuThu])
REFERENCES [dbo].[ThuThus] ([ID_ThuThu])
GO
ALTER TABLE [dbo].[PhieuMuons] CHECK CONSTRAINT [FK_PhieuMuons_ThuThus_ID_ThuThu]
GO
ALTER TABLE [dbo].[Saches]  WITH CHECK ADD  CONSTRAINT [FK_Saches_TacGias_ID_TacGia] FOREIGN KEY([ID_TacGia])
REFERENCES [dbo].[TacGias] ([ID_TacGia])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Saches] CHECK CONSTRAINT [FK_Saches_TacGias_ID_TacGia]
GO
ALTER TABLE [dbo].[Saches]  WITH CHECK ADD  CONSTRAINT [FK_Saches_TheLoais_ID_TheLoai] FOREIGN KEY([ID_TheLoai])
REFERENCES [dbo].[TheLoais] ([ID_TheLoai])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Saches] CHECK CONSTRAINT [FK_Saches_TheLoais_ID_TheLoai]
GO
ALTER TABLE [dbo].[TheThuViens]  WITH CHECK ADD  CONSTRAINT [FK_TheThuViens_DocGias_ID_DocGia] FOREIGN KEY([ID_DocGia])
REFERENCES [dbo].[DocGias] ([ID_DocGia])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TheThuViens] CHECK CONSTRAINT [FK_TheThuViens_DocGias_ID_DocGia]
GO
ALTER TABLE [dbo].[ThuThus]  WITH CHECK ADD  CONSTRAINT [FK_ThuThus_TaiKhoans_ID_TaiKhoan] FOREIGN KEY([ID_TaiKhoan])
REFERENCES [dbo].[TaiKhoans] ([ID_TaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThuThus] CHECK CONSTRAINT [FK_ThuThus_TaiKhoans_ID_TaiKhoan]
GO
