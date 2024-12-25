using Dapper;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using System.Threading.Tasks.Sources;

namespace QuanLyThuVien.Controllers
{
    public class BaoCaoThongKeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BaoCaoThongKeController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult SachDuocMuonNhieu()
        {
            string sql = @"SELECT s.ID_Sach AS IdSach, s.TenSach, tg.TenTacGia, 
                 tl.TenTheLoai, s.NgayNhap, s.GiaTien as GiaBan, SUM(ct.SoLuongMuon) AS SoLuongMuon, count(*) as SoLanMuon
                FROM Saches s
                LEFT JOIN TacGias tg ON tg.ID_TacGia = s.ID_TacGia
                LEFT JOIN TheLoais tl ON tl.ID_TheLoai = s.ID_TheLoai
                LEFT JOIN CTPhieuMuon ct ON ct.ID_Sach = s.ID_Sach
                LEFT JOIN PhieuMuons pm ON pm.ID_PhieuMuon = ct.ID_PhieuMuon
                GROUP BY s.ID_Sach,
                         s.TenSach,
                         tg.TenTacGia,
                         tl.TenTheLoai,
                         s.NgayNhap,
                         s.GiaTien
                ORDER BY SoLuongMuon desc";

            List<ThongKeSachMuonNhieu> lstSach;

            using (var connection = new SqlConnection(_db.Database.GetConnectionString()))
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                lstSach = connection.Query<ThongKeSachMuonNhieu>(sql).ToList();
            }

            return View(lstSach);
        }

        public IActionResult SachDuoi10Cuon()
        {
            string sql = @"SELECT s.ID_Sach AS IdSach, s.TenSach, tg.TenTacGia, 
                 tl.TenTheLoai, s.NgayNhap, s.GiaTien AS GiaBan, s.SoLuong
                FROM Saches s
                LEFT JOIN TacGias tg ON tg.ID_TacGia = s.ID_TacGia
                LEFT JOIN TheLoais tl ON tl.ID_TheLoai = s.ID_TheLoai
                WHERE s.SoLuong < 10
                ORDER BY s.SoLuong desc
                ";

            List<ThongKeSachDuoi10Cuon> lstSach;

            using (var connection = new SqlConnection(_db.Database.GetConnectionString()))
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                lstSach = connection.Query<ThongKeSachDuoi10Cuon>(sql).ToList();
            }

            return View(lstSach);
        }

        public IActionResult ThongKeBieuDo(int? tuThang, int? denThang)
        {
            if (!tuThang.HasValue)
                tuThang = 1;

            if (!denThang.HasValue)
                denThang = DateTime.Now.Month;

            if (tuThang.Value > denThang.Value)
            {
                tuThang = 1;
            }

            List<SelectListItem> thangs =
                Enumerable.Range(1, 12)
               .Select(x => new SelectListItem("Tháng " + x, x.ToString()))
               .ToList();

            thangs.Insert(0, new SelectListItem("-- Chọn tháng --", ""));

            ViewBag.Thangs = thangs;

            var data = _db.PhieuMuons
                .Include(x => x.CTPhieuMuon)
                .Where(x => x.CTPhieuMuon.Count > 0 && x.NgayTaoPhieu.Year == DateTime.Now.Year && x.NgayTaoPhieu.Month >= tuThang.Value && x.NgayTaoPhieu.Month <= denThang.Value)
                .Select(x => new
                {
                    Thang = x.NgayTaoPhieu.Month,
                    SLMuon = x.CTPhieuMuon.FirstOrDefault().SoLuongMuon
                })
                .ToList()
                .GroupBy(x => x.Thang)
                .Select(x => new
                {
                    Thang = x.Key,
                    SLMuon = x.Sum(c => c.SLMuon)
                })
               .ToDictionary(x => x.Thang, y => y.SLMuon);

            var obj = new ThongKeBieuDo()
            {
                TuThang = tuThang.Value,
                DenThang = denThang.Value,
                Title = string.Format("Thống kê số lượng sách mượn từ tháng {0} đến tháng {1} năm {2}",
                    tuThang.Value, denThang.Value, DateTime.Now.Year.ToString()
                )
            };

            Dictionary<int, int> values = Enumerable.Range(obj.TuThang, obj.MonthCount)
                .ToDictionary(x => x, x => 0);

            foreach (var key in values.Keys)
            {
                if (data.ContainsKey(key))
                {
                    values[key] = data[key];
                }
                else
                    values[key] = 0;
            }

            obj.YValues = string.Join(", ", values.Values);

            return View(obj);
        }

        public IActionResult BaoCaoDoanhThuLoiNhuanThang(int? tuThang, int? denThang)
        {
            if (!tuThang.HasValue)
                tuThang = 1;

            if (!denThang.HasValue)
                denThang = DateTime.Now.Month;

            if (tuThang.Value > denThang.Value)
            {
                tuThang = 1;
            }

            List<SelectListItem> thangs =
                Enumerable.Range(1, 12)
               .Select(x => new SelectListItem("Tháng " + x, x.ToString()))
               .ToList();

            thangs.Insert(0, new SelectListItem("-- Chọn tháng --", ""));

            ViewBag.Thangs = thangs;

            var data = _db.LSTraSach
                .Where(x => x.NgayTra.HasValue && x.NgayTra.Value.Year == DateTime.Now.Year && x.NgayTra.Value.Month >= tuThang.Value && x.NgayTra.Value.Month <= denThang.Value)
                .Select(x => new
                {
                    Thang = x.NgayTra.Value.Month,
                    SoTienTra = x.TongTienThue
                })
                .ToList()
                .GroupBy(x => x.Thang)
                .Select(x => new
                {
                    Thang = x.Key,
                    SoTien = x.Sum(c => c.SoTienTra)
                })
               .ToDictionary(x => x.Thang, y => y.SoTien);

            var obj = new DoanhThuLoiNhuanThang()
            {
                TuThang = tuThang.Value,
                DenThang = denThang.Value,
                Title = string.Format("Thống kê doanh thu tháng {0} đến tháng {1} năm {2}",
                    tuThang.Value, denThang.Value, DateTime.Now.Year.ToString()
                )
            };

            Dictionary<int, double> values = Enumerable.Range(obj.TuThang, obj.MonthCount)
                .ToDictionary(x => x, x => (double)0);

            foreach (var key in values.Keys)
            {
                if (data.ContainsKey(key))
                {
                    values[key] = data[key];
                }
                else
                    values[key] = 0;
            }

            obj.YValues = string.Join(", ", values.Values);

            return View("BaoCaoDoanhThuLoiNhuan", obj);
        }
    }
}
