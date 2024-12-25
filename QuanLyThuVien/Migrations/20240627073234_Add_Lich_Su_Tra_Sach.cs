using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class Add_Lich_Su_Tra_Sach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LSTraSach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PhieuMuon = table.Column<int>(type: "int", nullable: false),
                    ID_Sach = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    SoLuongTra = table.Column<int>(type: "int", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChuTra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTienPhat = table.Column<double>(type: "float", nullable: false),
                    GhiChuPhat = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_LSTraSach_ID_PhieuMuon",
                table: "LSTraSach",
                column: "ID_PhieuMuon");

            migrationBuilder.CreateIndex(
                name: "IX_LSTraSach_ID_Sach",
                table: "LSTraSach",
                column: "ID_Sach");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LSTraSach");
        }
    }
}
