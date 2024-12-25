using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace QuanLyThuVien.Models
{
    public class SachTraVM
    {
        [Required]
        public int IDPhieuMuon { get; set; }
        [Required]
        public int IDSach {  get; set; }
        public string TenSach { get; set; }
        public int SoLuongTra {  get; set; }
        public int TinhTrang {  get; set; }
        public int? MucDo { get; set; }
        public DateTime? NgayTra { get; set; }
        public string? GhiChuTra { get; set; }
        public int HinhThucMuon { get; set; }
        public double GiaThueTheoNgay { get; set; }
        public DateTime? NgayMuon { get; set; }
        public double GiaSach { get; set; }
        public int SoNgayMuon { get; set; }
        public double TienPhaiTra { get; set; }
    }
}
