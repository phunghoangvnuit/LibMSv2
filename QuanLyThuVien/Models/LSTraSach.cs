using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class LSTraSach
    {
        [Key]
        public int Id { get; set; }
        private int _ID_PhieuMuon;
        private int _ID_Sach;
        private int _SoLuongTra;
        private int _TrangThai;
        private DateTime? _NgayTra;
        private string _GhiChuTra;

        private PhieuMuon _PhieuMuon;
        private Sach _Sach;

        public int ID_PhieuMuon { get => _ID_PhieuMuon; set => _ID_PhieuMuon = value; }
        public int ID_Sach { get => _ID_Sach; set => _ID_Sach = value; }

        [ForeignKey("ID_PhieuMuon")]
        public PhieuMuon PhieuMuon { get => _PhieuMuon; set => _PhieuMuon = value; }
        [ForeignKey("ID_Sach")]
        public Sach Sach { get => _Sach; set => _Sach = value; }
        // bình thường , sách bị rách, sách bị mất trang, sách bị mất, quá hạn trả
        public int TrangThai { get => _TrangThai; set => _TrangThai = value; }
        public int? MucDo { get; set; }
        public int SoLuongTra { get => _SoLuongTra; set => _SoLuongTra = value; }
        public DateTime? NgayTra { get => _NgayTra; set => _NgayTra = value; }
        public string GhiChuTra { get => _GhiChuTra; set => _GhiChuTra = value; }
        public double SoTienPhat { get; set; } = 0;
        public string GhiChuPhat {  get; set; }
        public double TongTienThue { get; set; }
    }
}
