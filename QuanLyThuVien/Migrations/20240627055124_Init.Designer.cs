﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyThuVien.Data;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240627055124_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.7.22376.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuanLyThuVien.Models.CTPhieuMuon", b =>
                {
                    b.Property<int>("ID_PhieuMuon")
                        .HasColumnType("int");

                    b.Property<int>("ID_Sach")
                        .HasColumnType("int");

                    b.Property<string>("GhiChuTra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayTra")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuongMuon")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID_PhieuMuon", "ID_Sach");

                    b.HasIndex("ID_Sach");

                    b.ToTable("CTPhieuMuon");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.DocGia", b =>
                {
                    b.Property<int>("ID_DocGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_DocGia"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_TaiKhoan")
                        .HasColumnType("int");

                    b.Property<string>("MaSV")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDocGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_DocGia");

                    b.HasIndex("ID_TaiKhoan");

                    b.HasIndex("Email", "MaSV");

                    b.ToTable("DocGias");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.PhieuMuon", b =>
                {
                    b.Property<int>("ID_PhieuMuon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_PhieuMuon"));

                    b.Property<string>("GhiChuMuon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_The")
                        .HasColumnType("int");

                    b.Property<int?>("ID_ThuThu")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayHenTra")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTaoPhieu")
                        .HasColumnType("datetime2");

                    b.HasKey("ID_PhieuMuon");

                    b.HasIndex("ID_The");

                    b.HasIndex("ID_ThuThu");

                    b.ToTable("PhieuMuons");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.Sach", b =>
                {
                    b.Property<int>("ID_Sach")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Sach"));

                    b.Property<double>("GiaTien")
                        .HasColumnType("float");

                    b.Property<int>("ID_TacGia")
                        .HasColumnType("int");

                    b.Property<int>("ID_TheLoai")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayNhap")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenSach")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Sach");

                    b.HasIndex("ID_TacGia");

                    b.HasIndex("ID_TheLoai");

                    b.ToTable("Saches");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TacGia", b =>
                {
                    b.Property<int>("ID_TacGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TacGia"));

                    b.Property<string>("QuocGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenTacGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_TacGia");

                    b.ToTable("TacGias");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TaiKhoan", b =>
                {
                    b.Property<int>("ID_TaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TaiKhoan"));

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VaiTro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_TaiKhoan");

                    b.HasIndex("TenDangNhap")
                        .IsUnique();

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TheLoai", b =>
                {
                    b.Property<int>("ID_TheLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TheLoai"));

                    b.Property<string>("TenTheLoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID_TheLoai");

                    b.HasIndex("TenTheLoai")
                        .IsUnique();

                    b.ToTable("TheLoais");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TheThuVien", b =>
                {
                    b.Property<int>("ID_The")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_The"));

                    b.Property<int>("ID_DocGia")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBD")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoThe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_The");

                    b.HasIndex("ID_DocGia");

                    b.ToTable("TheThuViens");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.ThuThu", b =>
                {
                    b.Property<int>("ID_ThuThu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_ThuThu"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_TaiKhoan")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenThuThu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_ThuThu");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ID_TaiKhoan");

                    b.ToTable("ThuThus");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.CTPhieuMuon", b =>
                {
                    b.HasOne("QuanLyThuVien.Models.PhieuMuon", "PhieuMuon")
                        .WithMany("CTPhieuMuon")
                        .HasForeignKey("ID_PhieuMuon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyThuVien.Models.Sach", "Sach")
                        .WithMany("CTPhieuMuon")
                        .HasForeignKey("ID_Sach")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhieuMuon");

                    b.Navigation("Sach");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.DocGia", b =>
                {
                    b.HasOne("QuanLyThuVien.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("DocGias")
                        .HasForeignKey("ID_TaiKhoan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.PhieuMuon", b =>
                {
                    b.HasOne("QuanLyThuVien.Models.TheThuVien", "TheThuVien")
                        .WithMany("PhieuMuon")
                        .HasForeignKey("ID_The")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyThuVien.Models.ThuThu", "ThuThu")
                        .WithMany("PhieuMuon")
                        .HasForeignKey("ID_ThuThu");

                    b.Navigation("TheThuVien");

                    b.Navigation("ThuThu");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.Sach", b =>
                {
                    b.HasOne("QuanLyThuVien.Models.TacGia", "TacGia")
                        .WithMany("Sachs")
                        .HasForeignKey("ID_TacGia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyThuVien.Models.TheLoai", "TheLoai")
                        .WithMany("Sachs")
                        .HasForeignKey("ID_TheLoai")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TacGia");

                    b.Navigation("TheLoai");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TheThuVien", b =>
                {
                    b.HasOne("QuanLyThuVien.Models.DocGia", "DocGia")
                        .WithMany("TheThuViens")
                        .HasForeignKey("ID_DocGia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocGia");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.ThuThu", b =>
                {
                    b.HasOne("QuanLyThuVien.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("ThuThus")
                        .HasForeignKey("ID_TaiKhoan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.DocGia", b =>
                {
                    b.Navigation("TheThuViens");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.PhieuMuon", b =>
                {
                    b.Navigation("CTPhieuMuon");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.Sach", b =>
                {
                    b.Navigation("CTPhieuMuon");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TacGia", b =>
                {
                    b.Navigation("Sachs");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TaiKhoan", b =>
                {
                    b.Navigation("DocGias");

                    b.Navigation("ThuThus");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TheLoai", b =>
                {
                    b.Navigation("Sachs");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.TheThuVien", b =>
                {
                    b.Navigation("PhieuMuon");
                });

            modelBuilder.Entity("QuanLyThuVien.Models.ThuThu", b =>
                {
                    b.Navigation("PhieuMuon");
                });
#pragma warning restore 612, 618
        }
    }
}
