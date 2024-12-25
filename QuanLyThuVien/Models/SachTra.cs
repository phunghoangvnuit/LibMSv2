namespace QuanLyThuVien.Models
{
    public class SachTra
    {
        private int _Id_PhieuMuon;
        private int _Id_Sach;
        private string _TenSach;
        private int _Id_MaThe;
        private string _TenDocGia;
        private int _SlMuon;
        private DateTime? _NgayTra;
        private string _GhiChuTra;

        public int Id_PhieuMuon { get => _Id_PhieuMuon; set => _Id_PhieuMuon = value; }
        public int Id_Sach { get => _Id_Sach; set => _Id_Sach = value; }
        public string TenSach { get => _TenSach; set => _TenSach = value; }
        public int Id_MaThe { get => _Id_MaThe; set => _Id_MaThe = value; }
        public string TenDocGia { get => _TenDocGia; set => _TenDocGia = value; }
        public int SlMuon { get => _SlMuon; set => _SlMuon = value; }
        public DateTime? NgayTra { get => _NgayTra; set => _NgayTra = value; }
        public int TrangThai { get; set; }
        public string TrangThaiStr { get
            {
                if (TrangThai == 1)
                    return "Bình thường";
                if (TrangThai == 2)
                    return "Bị rách";
                if (TrangThai == 3)
                    return "Bị mất trang";
                if (TrangThai == 4)
                    return "Bị mất";
                return "";
            } 
        }
        public int? MucDo { get; set; }
        public string MucDoStr
        {
            get
            {
                if (MucDo.HasValue)
                {
                    if (MucDo.Value == 1)
                        return "Ít";
                    if (MucDo.Value == 2)
                        return "Trung bình";
                    if (MucDo.Value == 3)
                        return "Nhiều";
                }

                return "";
            }
        }
        public int SoLuongTra { get;set; }
        public double SoTienPhat { get; set; }
        public string GhiChuPhat { get; set; }
        public string GhiChuTra { get => _GhiChuTra; set => _GhiChuTra = value; }

        public SachTra() { }

        public SachTra(int id_PhieuMuon
            , int id_Sach, string tenSach, int id_MaThe, string tenDocGia, int slMuon, DateTime? ngayTra
            , string ghiChuTra, int trangThai, int? mucDo, int soLuongTra, double soTienPhat, string ghiChuPhat)
        {
            Id_PhieuMuon = id_PhieuMuon;
            Id_Sach = id_Sach;
            TenSach = tenSach;
            Id_MaThe = id_MaThe;
            TenDocGia = tenDocGia;
            SlMuon = slMuon;
            NgayTra = ngayTra;
            GhiChuTra = ghiChuTra;
            TrangThai = trangThai;
            MucDo = mucDo;
            SoLuongTra = soLuongTra;
            SoTienPhat = soTienPhat;
            GhiChuPhat = ghiChuPhat;
        }
    }
}
