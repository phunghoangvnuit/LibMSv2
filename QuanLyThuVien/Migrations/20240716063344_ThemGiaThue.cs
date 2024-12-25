using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class ThemGiaThue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GiaThueTheoNgay",
                table: "Saches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "CoTinhPhi",
                table: "PhieuMuons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TongTienThue",
                table: "LSTraSach",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "TongTienThue",
                table: "CTPhieuMuon",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaThueTheoNgay",
                table: "Saches");

            migrationBuilder.DropColumn(
                name: "CoTinhPhi",
                table: "PhieuMuons");

            migrationBuilder.DropColumn(
                name: "TongTienThue",
                table: "LSTraSach");

            migrationBuilder.DropColumn(
                name: "TongTienThue",
                table: "CTPhieuMuon");
        }
    }
}
