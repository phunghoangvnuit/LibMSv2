using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class QuanLySachCuController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QuanLySachCuController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult SachPhieuPhat()
        {
            string sql = @"SELECT s.ID_Sach AS IdSach, s.TenSach, tg.TenTacGia, 
                     tl.TenTheLoai, s.NgayNhap, s.GiaTien AS GiaBan, SUM(ls.SoLuongTra) AS SoLuong
                    FROM Saches s
                    LEFT JOIN TacGias tg ON tg.ID_TacGia = s.ID_TacGia
                    LEFT JOIN TheLoais tl ON tl.ID_TheLoai = s.ID_TheLoai
                    INNER JOIN LSTraSach ls ON ls.ID_Sach = s.ID_Sach
                    WHERE ls.TrangThai > 1
					GROUP BY s.ID_Sach,
                             s.TenSach,
                             tg.TenTacGia,
                             tl.TenTheLoai,
                             s.NgayNhap,
                             s.GiaTien
                ";

            List<ThongKeSachBiPhat> lstSach;

            using (var connection = new SqlConnection(_db.Database.GetConnectionString()))
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                lstSach = connection.Query<ThongKeSachBiPhat>(sql).ToList();
            }

            return View(lstSach);
        }

        public IActionResult SachNhapKhoQua2Nam()
        {
            string sql = @"SELECT s.ID_Sach AS IdSach, s.TenSach, tg.TenTacGia, 
                 tl.TenTheLoai, s.NgayNhap, s.GiaTien AS GiaBan, s.SoLuong
                FROM Saches s
                LEFT JOIN TacGias tg ON tg.ID_TacGia = s.ID_TacGia
                LEFT JOIN TheLoais tl ON tl.ID_TheLoai = s.ID_TheLoai
                WHERE YEAR(GETDATE()) - YEAR(s.NgayNhap) >= 2
                ";

            List<ThongKeSachTonKhoQua2Nam> lstSach;

            using (var connection = new SqlConnection(_db.Database.GetConnectionString()))
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                lstSach = connection.Query<ThongKeSachTonKhoQua2Nam>(sql).ToList();
            }

            return View(lstSach);
        }
    }
}
