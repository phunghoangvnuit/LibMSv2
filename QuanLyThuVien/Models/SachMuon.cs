namespace QuanLyThuVien.Models
{
    public class SachMuon
    {
        private int _MaPhieuMuon;
        private string _TenNguoiTao;
        private int _MaTheThuVien;
        private string _TenDocGia;
        private int _MaSach;
        private string _TenSach;
        private int _SoLuongMuon;
        private DateTime _NgayTaoPhieu;
        private DateTime _NgayHenTra;
        private string _GhiChuMuon;

        public int MaPhieuMuon { get => _MaPhieuMuon; set => _MaPhieuMuon = value; }
        public string TenNguoiTao { get => _TenNguoiTao; set => _TenNguoiTao = value; }
        public int MaTheThuVien { get => _MaTheThuVien; set => _MaTheThuVien = value; }
        public string TenDocGia { get => _TenDocGia; set => _TenDocGia = value; }
        public int MaSach { get => _MaSach; set => _MaSach = value; }
        public string TenSach { get => _TenSach; set => _TenSach = value; }
        public int SoLuongMuon { get => _SoLuongMuon; set => _SoLuongMuon = value; }
        public int SoLuongTra { get; set; } = 0;
        public int ConLai { get
            {
                return SoLuongMuon - SoLuongTra;
            } 
        }
        public DateTime NgayTaoPhieu { get => _NgayTaoPhieu; set => _NgayTaoPhieu = value; }
        public DateTime NgayHenTra { get => _NgayHenTra; set => _NgayHenTra = value; }
        public string GhiChuMuon { get => _GhiChuMuon; set => _GhiChuMuon = value; }

        public string UrlImg { get; set; }
        public string TrangThai
        {
            get
            {
                if (NgayHenTra.Date < DateTime.Now.Date)
                    return "Late return";
                else
                    return "Normal";
            }
        }

        public int HinhThucMuon { get; set; }

        public SachMuon() { }

        public SachMuon(int maPhieuMuon, string tenNguoiTao, int maTheThuVien, string tenDocGia, int maSach, string tenSach, int soLuongMuon, DateTime ngayTaoPhieu, DateTime ngayHenTra, string ghiChuMuon, string urlImg, int hinhThucMuon)
        {
            MaPhieuMuon = maPhieuMuon;
            TenNguoiTao = tenNguoiTao;
            MaTheThuVien = maTheThuVien;
            TenDocGia = tenDocGia;
            MaSach = maSach;
            TenSach = tenSach;
            SoLuongMuon = soLuongMuon;
            NgayTaoPhieu = ngayTaoPhieu;
            NgayHenTra = ngayHenTra;
            GhiChuMuon = ghiChuMuon;
            UrlImg = urlImg;
            HinhThucMuon = hinhThucMuon;
        }
    }
}
