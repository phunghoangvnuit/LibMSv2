using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TacGias",
                columns: table => new
                {
                    ID_TacGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTacGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuocGia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGias", x => x.ID_TacGia);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    ID_TaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.ID_TaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "TheLoais",
                columns: table => new
                {
                    ID_TheLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTheLoai = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoais", x => x.ID_TheLoai);
                });

            migrationBuilder.CreateTable(
                name: "DocGias",
                columns: table => new
                {
                    ID_DocGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDocGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID_TaiKhoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocGias", x => x.ID_DocGia);
                    table.ForeignKey(
                        name: "FK_DocGias_TaiKhoans_ID_TaiKhoan",
                        column: x => x.ID_TaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "ID_TaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Saches",
                columns: table => new
                {
                    ID_Sach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTien = table.Column<double>(type: "float", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaThueTheoNgay = table.Column<double>(type: "float", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_TheLoai = table.Column<int>(type: "int", nullable: false),
                    ID_TacGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saches", x => x.ID_Sach);
                    table.ForeignKey(
                        name: "FK_Saches_TacGias_ID_TacGia",
                        column: x => x.ID_TacGia,
                        principalTable: "TacGias",
                        principalColumn: "ID_TacGia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Saches_TheLoais_ID_TheLoai",
                        column: x => x.ID_TheLoai,
                        principalTable: "TheLoais",
                        principalColumn: "ID_TheLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TheThuViens",
                columns: table => new
                {
                    ID_The = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayBD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_DocGia = table.Column<int>(type: "int", nullable: false),
                    SoThe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheThuViens", x => x.ID_The);
                    table.ForeignKey(
                        name: "FK_TheThuViens_DocGias_ID_DocGia",
                        column: x => x.ID_DocGia,
                        principalTable: "DocGias",
                        principalColumn: "ID_DocGia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuMuons",
                columns: table => new
                {
                    ID_PhieuMuon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhThucMuon = table.Column<int>(type: "int", nullable: false),
                    NgayTaoPhieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHenTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_TaiKhoan = table.Column<int>(type: "int", nullable: true),
                    ID_The = table.Column<int>(type: "int", nullable: false),
                    GhiChuMuon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuMuons", x => x.ID_PhieuMuon);
                    table.ForeignKey(
                        name: "FK_PhieuMuons_TaiKhoans_ID_TaiKhoan",
                        column: x => x.ID_TaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "ID_TaiKhoan");
                    table.ForeignKey(
                        name: "FK_PhieuMuons_TheThuViens_ID_The",
                        column: x => x.ID_The,
                        principalTable: "TheThuViens",
                        principalColumn: "ID_The",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTPhieuMuon",
                columns: table => new
                {
                    ID_PhieuMuon = table.Column<int>(type: "int", nullable: false),
                    ID_Sach = table.Column<int>(type: "int", nullable: false),
                    TongTienThue = table.Column<float>(type: "real", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    SoLuongMuon = table.Column<int>(type: "int", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChuTra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTPhieuMuon", x => new { x.ID_PhieuMuon, x.ID_Sach });
                    table.ForeignKey(
                        name: "FK_CTPhieuMuon_PhieuMuons_ID_PhieuMuon",
                        column: x => x.ID_PhieuMuon,
                        principalTable: "PhieuMuons",
                        principalColumn: "ID_PhieuMuon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTPhieuMuon_Saches_ID_Sach",
                        column: x => x.ID_Sach,
                        principalTable: "Saches",
                        principalColumn: "ID_Sach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LSTraSach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PhieuMuon = table.Column<int>(type: "int", nullable: false),
                    ID_Sach = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    MucDo = table.Column<int>(type: "int", nullable: true),
                    SoLuongTra = table.Column<int>(type: "int", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChuTra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTienPhat = table.Column<double>(type: "float", nullable: false),
                    GhiChuPhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTienThue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LSTraSach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LSTraSach_PhieuMuons_ID_PhieuMuon",
                        column: x => x.ID_PhieuMuon,
                        principalTable: "PhieuMuons",
                        principalColumn: "ID_PhieuMuon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LSTraSach_Saches_ID_Sach",
                        column: x => x.ID_Sach,
                        principalTable: "Saches",
                        principalColumn: "ID_Sach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTPhieuMuon_ID_Sach",
                table: "CTPhieuMuon",
                column: "ID_Sach");

            migrationBuilder.CreateIndex(
                name: "IX_DocGias_Email_MaSV",
                table: "DocGias",
                columns: new[] { "Email", "MaSV" });

            migrationBuilder.CreateIndex(
                name: "IX_DocGias_ID_TaiKhoan",
                table: "DocGias",
                column: "ID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_LSTraSach_ID_PhieuMuon",
                table: "LSTraSach",
                column: "ID_PhieuMuon");

            migrationBuilder.CreateIndex(
                name: "IX_LSTraSach_ID_Sach",
                table: "LSTraSach",
                column: "ID_Sach");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuons_ID_TaiKhoan",
                table: "PhieuMuons",
                column: "ID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuons_ID_The",
                table: "PhieuMuons",
                column: "ID_The");

            migrationBuilder.CreateIndex(
                name: "IX_Saches_ID_TacGia",
                table: "Saches",
                column: "ID_TacGia");

            migrationBuilder.CreateIndex(
                name: "IX_Saches_ID_TheLoai",
                table: "Saches",
                column: "ID_TheLoai");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_TenDangNhap",
                table: "TaiKhoans",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TheLoais_TenTheLoai",
                table: "TheLoais",
                column: "TenTheLoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TheThuViens_ID_DocGia",
                table: "TheThuViens",
                column: "ID_DocGia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTPhieuMuon");

            migrationBuilder.DropTable(
                name: "LSTraSach");

            migrationBuilder.DropTable(
                name: "PhieuMuons");

            migrationBuilder.DropTable(
                name: "Saches");

            migrationBuilder.DropTable(
                name: "TheThuViens");

            migrationBuilder.DropTable(
                name: "TacGias");

            migrationBuilder.DropTable(
                name: "TheLoais");

            migrationBuilder.DropTable(
                name: "DocGias");

            migrationBuilder.DropTable(
                name: "TaiKhoans");
        }
    }
}
