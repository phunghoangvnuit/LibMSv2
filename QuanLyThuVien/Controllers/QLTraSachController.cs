using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class QLTraSachController : Controller
    {
        private readonly ApplicationDbContext _db;
        private string vaiTro = LoginController.layout.VaiTro;
        public QLTraSachController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: QLTraSach
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return NotFound();

            if (vaiTro.ToLower() != "librarian")
            {
                TempData["error"] = "Only librarian can use this feature !";
                return RedirectToAction("ViewSachMuon", "QuanLyMuonTraSach");
            }

            var data = from tt in _db.TaiKhoans
                       from pm in _db.PhieuMuons
                       from ctpm in _db.CTPhieuMuon
                       from dg in _db.DocGias
                       from s in _db.Saches
                       from ttv in _db.TheThuViens
                       where (pm.ID_PhieuMuon == ctpm.ID_PhieuMuon && pm.ID_The == ttv.ID_The
                             && pm.ID_TaiKhoan == tt.ID_TaiKhoan && ttv.ID_DocGia == dg.ID_DocGia
                             && ctpm.ID_Sach == s.ID_Sach && pm.ID_PhieuMuon == id)
                       select new SachTraVM
                       {
                           IDPhieuMuon = pm.ID_PhieuMuon,
                           IDSach = s.ID_Sach,
                           TenSach = s.TenSach,
                           SoLuongTra = ctpm.SoLuongMuon,
                           TinhTrang = -1,
                           NgayTra = pm.NgayHenTra,
                           GhiChuTra = "",
                           HinhThucMuon = pm.HinhThucMuon,
                           NgayMuon= pm.NgayTaoPhieu,
                           GiaThueTheoNgay = s.GiaThueTheoNgay
                       };

            var obj = data.FirstOrDefault();

            if (obj == null)
                return NotFound();


            CTPhieuMuon? ct = _db.CTPhieuMuon.First(e => e.ID_PhieuMuon == obj.IDPhieuMuon);

            int soSachDaTra = _db.LSTraSach
                .Where(x => x.ID_PhieuMuon == obj.IDPhieuMuon && x.ID_Sach == obj.IDSach)
                .Sum(x => x.SoLuongTra);

            int soLuongSachConLai = ct.SoLuongMuon - soSachDaTra;

            obj.SoLuongTra = soLuongSachConLai;
            obj.GhiChuTra = obj.NgayTra.Value.Date < DateTime.Now.Date ? "Late return" : "";
            obj.NgayTra = DateTime.Now;
            obj.SoNgayMuon = (int) (obj.NgayTra.Value - obj.NgayMuon.Value).TotalDays + 1;
            obj.TienPhaiTra = obj.GiaThueTheoNgay * obj.SoNgayMuon;
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SachTraVM obj)
        {
            if (obj.IDPhieuMuon == null)
                return NotFound();

            PhieuMuon pm = _db.PhieuMuons.Find(obj.IDPhieuMuon);
            
            if (pm == null)
                return NotFound();

            if (ModelState.IsValid)
            {

                if (obj.TinhTrang <= 0)
                {
                    TempData["error"] = "Please select status !";
                    return RedirectToAction("Index", "QLTraSach", new { id = obj.IDPhieuMuon });
                }
                // Select dữ liệu bảng chi tiết phiếu mượn qua mã phiếu mượn
                CTPhieuMuon? ctpm = _db.CTPhieuMuon
                    .Include(x => x.Sach)
                    .FirstOrDefault(e => e.ID_PhieuMuon == obj.IDPhieuMuon);
                
                int soSachDaTra = _db.LSTraSach
                    .Where(x => x.ID_PhieuMuon == obj.IDPhieuMuon && x.ID_Sach == obj.IDSach)
                    .Sum(x => x.SoLuongTra);

                if (ctpm == null)
                {
                    TempData["error"] = "Cannot find the record details !";
                    return RedirectToAction("Index", "QLTraSach", new { id = obj.IDPhieuMuon });
                }

                int soLuongSachConLai = ctpm.SoLuongMuon - soSachDaTra;

                if (obj.SoLuongTra <= 0 || obj.SoLuongTra > soLuongSachConLai)
                {
                    TempData["error"] = "Your return quantity is invalid ! You only can return " + soLuongSachConLai + " book(s) you borrowed !";
                    return RedirectToAction("Index", "QLTraSach", new { id = obj.IDPhieuMuon });
                }

                if (obj.TinhTrang > 1 && obj.TinhTrang < 4)
                {
                    if (!obj.MucDo.HasValue || obj.MucDo.Value <= 0)
                    {
                        TempData["error"] = "Please select level of damage !";
                        return RedirectToAction("Index", "QLTraSach", new { id = obj.IDPhieuMuon });
                    }
                }

                if (obj.SoLuongTra == soLuongSachConLai) // Trả hết sách
                {
                    // 1 -> đã trả
                    ctpm.TrangThai = 1;
                    ctpm.NgayTra = DateTime.Now;
                    
                    if (pm.HinhThucMuon == 2) // Mượn có trả phí
                    {
                        var soNgayMuon = (int)(obj.NgayTra.Value - pm.NgayTaoPhieu).TotalDays + 1;
                        var tienPhaiTra = ctpm.Sach.GiaThueTheoNgay * soNgayMuon;
                        ctpm.TongTienThue = (float) tienPhaiTra;
                    } 
                    _db.CTPhieuMuon.Update(ctpm);
                    await _db.SaveChangesAsync();

                }

                // Không cập nhật sách bị mất, bị rách, bị mất trang 
                // vào danh sách sách, chỉ cập nhật trạng thái sách bình thường

                if (obj.TinhTrang == 1)
                {
                    // Đợi cập nhật thông tin sách
                    await UpdateSlSachKhiTra(obj.IDSach, obj.SoLuongTra);
                }

                Sach sach = _db.Saches.Find(obj.IDSach);

                // Lưu lại lịch sử trả
                LSTraSach ls = new LSTraSach();
                ls.ID_PhieuMuon = obj.IDPhieuMuon;
                ls.ID_Sach = obj.IDSach;
                ls.TrangThai = obj.TinhTrang;
                ls.SoLuongTra = obj.SoLuongTra;
                ls.NgayTra = obj.NgayTra;
                ls.SoTienPhat = 0;
                ls.GhiChuPhat = "";
                
                if (pm.HinhThucMuon == 2) // Mượn có trả phí
                {
                    var soNgayMuon = (int)(obj.NgayTra.Value - pm.NgayTaoPhieu).TotalDays + 1;
                    var tienPhaiTra = ctpm.Sach.GiaThueTheoNgay * soNgayMuon;
                    ls.TongTienThue = (float)tienPhaiTra;
                }

                if (obj.TinhTrang == 1)
                    ls.GhiChuTra = "Normal";
                    
                if (obj.TinhTrang == 2)
                    ls.GhiChuTra = "The book is torn";

                if (obj.TinhTrang == 3)
                    ls.GhiChuTra = "The book is missing pages";

                if (obj.TinhTrang == 4)
                {
                    ls.GhiChuTra = "The book is lost";
                    ls.SoTienPhat = obj.SoLuongTra * sach.GiaTien; // Phạt 10%
                    ls.GhiChuPhat = "Fine 100% of the book's value";
                }

                if (obj.TinhTrang == 2) // Nếu sách bị rách
                {
                    ls.MucDo = obj.MucDo;

                    if (obj.MucDo == 1) // Ít
                    {
                        ls.SoTienPhat = obj.SoLuongTra * (sach.GiaTien * 0.1); // Phạt 10%
                        ls.GhiChuPhat = "Fine 10% of the book's value";
                    }

                    if (obj.MucDo == 2) // Trung bình
                    {
                        ls.SoTienPhat = obj.SoLuongTra * (sach.GiaTien * 0.2); // Phạt 10%
                        ls.GhiChuPhat = "Fine 20% of the book's value";
                    }

                    if (obj.MucDo == 3) // Nhiều
                    {
                        ls.SoTienPhat = obj.SoLuongTra * (sach.GiaTien * 0.3); // Phạt 10%
                        ls.GhiChuPhat = "Fine 30% of the book's value";
                    }
                }

                if (obj.TinhTrang == 3) // Nếu sách bị mất trang
                {
                    ls.MucDo = obj.MucDo;

                    if (obj.MucDo == 1) // Ít
                    {
                        ls.SoTienPhat = obj.SoLuongTra * (sach.GiaTien * 0.15); // Phạt 10%
                        ls.GhiChuPhat = "Fine 15% of the book's value";
                    }

                    if (obj.MucDo == 2) // Trung bình
                    {
                        ls.SoTienPhat = obj.SoLuongTra * (sach.GiaTien * 0.3); // Phạt 10%
                        ls.GhiChuPhat = "Fine 30% of the book's value";
                    }

                    if (obj.MucDo == 3) // Nhiều
                    {
                        ls.SoTienPhat = obj.SoLuongTra * (sach.GiaTien * 0.45); // Phạt 10%
                        ls.GhiChuPhat = "Fine 45% of the book's value";
                    }
                }
                // Phạt quá hạn trả
                if (obj.NgayTra > pm.NgayHenTra && obj.TinhTrang >= 1 && obj.TinhTrang < 4)
                {
                    ls.GhiChuTra = "Late return (20000) + " + ls.GhiChuPhat;
                    ls.SoTienPhat = ls.SoTienPhat + (obj.SoLuongTra * 20000);
                }
                _db.Add(ls);
                await _db.SaveChangesAsync();
                TempData["success"] = "Successfully return !";
                return RedirectToAction("ViewSachMuon", "QuanLyMuonTraSach");
            }
            return RedirectToAction("Index", "QLTraSach", new { id = obj.IDPhieuMuon });
        }

        // Phương thức thay đổi số lượng sách khi xác nhận trả sách
        public async Task UpdateSlSachKhiTra(int maSach, int slTra)
        {
            Sach? s = _db.Saches.Find(maSach);
            if (s != null)
            {
                s.SoLuong += slTra;
                _db.Saches.Update(s);
                await _db.SaveChangesAsync();
            }
        }
    }
}
