namespace QuanLyThuVien.Models
{
    public class ThongKeSachDuoi10Cuon
    {
        private int idSach;
        private string tenSach;
        private string tenTacGia;
        private string tenTheLoai;
        private DateTime ngayNhap;
        private double giaBan;
        private int soLuong;

        public string TenSach { get => tenSach; set => tenSach = value; }
        public string TenTacGia { get => tenTacGia; set => tenTacGia = value; }
        public string TenTheLoai { get => tenTheLoai; set => tenTheLoai = value; }
        public DateTime NgayNhap { get => ngayNhap; set => ngayNhap = value; }
        public double GiaBan { get => giaBan; set => giaBan = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int IdSach { get => idSach; set => idSach = value; }
    }
}
