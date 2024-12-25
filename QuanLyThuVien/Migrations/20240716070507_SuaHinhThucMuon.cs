using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class SuaHinhThucMuon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoTinhPhi",
                table: "PhieuMuons");

            migrationBuilder.AddColumn<int>(
                name: "HinhThucMuon",
                table: "PhieuMuons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhThucMuon",
                table: "PhieuMuons");

            migrationBuilder.AddColumn<bool>(
                name: "CoTinhPhi",
                table: "PhieuMuons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
