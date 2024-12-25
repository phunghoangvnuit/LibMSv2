using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class DocGia
    {
        private int _ID_DocGia;
        private string _TenDocGia;
        private string _GioiTinh;
        private DateTime _NgaySinh;
        private string _DiaChi;
        private string _SDT;
        private string _Email;
        private string _maSV;

        private int _ID_TaiKhoan;
        private TaiKhoan _TaiKhoan;

        private ICollection<TheThuVien> _TheThuViens = new List<TheThuVien>();

        [Key]
        public int ID_DocGia { get => _ID_DocGia; set => _ID_DocGia = value; }
        public string TenDocGia { get => _TenDocGia; set => _TenDocGia = value; }
        public string GioiTinh { get => _GioiTinh; set => _GioiTinh = value; }
        public DateTime NgaySinh { get => _NgaySinh; set => _NgaySinh = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string MaSV { get => _maSV; set => _maSV = value; }

        public ICollection<TheThuVien> TheThuViens { get => _TheThuViens; set => _TheThuViens = value; }

        public int ID_TaiKhoan { get => _ID_TaiKhoan; set => _ID_TaiKhoan = value; }
        [ForeignKey("ID_TaiKhoan")]
        public TaiKhoan TaiKhoan { get => _TaiKhoan; set => _TaiKhoan = value; }
    }
}
