using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class TheThuVien
    {
        private int _ID_The;
        private DateTime _NgayBD;
        private DateTime _NgayHetHan;
        private int _ID_DocGia;
        private string _SoThe;

        private DocGia _DocGia;
        private ICollection<PhieuMuon> _PhieuMuon;

        [Key]
        public int ID_The { get => _ID_The; set => _ID_The = value; }
        [Required]
        public DateTime NgayBD { get => _NgayBD; set => _NgayBD = DateTime.Now; }
        [Required]
        public DateTime NgayHetHan { get => _NgayHetHan; set => _NgayHetHan = value; }
        
        public int ID_DocGia { get => _ID_DocGia; set => _ID_DocGia = value; }
        public string SoThe { get => _SoThe; set => _SoThe = value; }

        [ForeignKey("ID_DocGia")]
        public DocGia DocGia { get => _DocGia; set => _DocGia = value; }
        public ICollection<PhieuMuon> PhieuMuon { get => _PhieuMuon; set => _PhieuMuon = value; }
    }
}
