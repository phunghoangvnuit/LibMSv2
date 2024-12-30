using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class TaiKhoan
    {
        private int _ID_TaiKhoan;
        private string _TenDangNhap;
        private string _MatKhau;
        private string _VaiTro;
        private ICollection<DocGia> _DocGias;

        [Key]
        public int ID_TaiKhoan { get => _ID_TaiKhoan; set => _ID_TaiKhoan = value; }
        [BindProperty]
        [Required(ErrorMessage = "This feild cannot be null")]
        public string TenDangNhap { get => _TenDangNhap; set => _TenDangNhap = value; }
        [BindProperty]
        [Required(ErrorMessage = "This feild cannot be null")]
        public string MatKhau { get => _MatKhau; set => _MatKhau = value; }
        public string VaiTro { get => _VaiTro; set => _VaiTro = value; }
        public ICollection<DocGia> DocGias { get => _DocGias; set => _DocGias = value; }
    }
}
