namespace QuanLyThuVien.Models
{
    public class DoanhThuLoiNhuanThang
    {
        public int TuThang { get; set; } = 1;
        public int DenThang { get; set; } = DateTime.Now.Month;

        public int MonthCount
        {
            get
            {
                return (DenThang - TuThang) + 1;
            }
        }

        public string XValues
        {
            get
            {
                return String.Join(",", Enumerable.Range(TuThang, MonthCount)
                    .Select(x => "\"Tháng " + x + "\""));
            }
        }

        public string YValues { get; set; }

        public string Title { get; internal set; }
    }
}
