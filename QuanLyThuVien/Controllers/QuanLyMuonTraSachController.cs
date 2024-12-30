using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class QuanLyMuonTraSachController : Controller
    {
        private readonly ApplicationDbContext _db;
        public QuanLyMuonTraSachController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Mã tài khoản đăng nhập
        private int idDN = LoginController.id;
        private string vaiTro = LoginController.layout.VaiTro;
        // Get view sách đang mượn
        public IActionResult ViewSachMuon(string filter = null)
        {
            LinkedList<SachMuon> lstPhieuMuon = new LinkedList<SachMuon>();
            var dataPhieuMuons = from tt in _db.ThuThus
                                from pm in _db.PhieuMuons
                                from ctpm in _db.CTPhieuMuon
                                from dg in _db.DocGias
                                from s in _db.Saches
                                from ttv in _db.TheThuViens
                                where (pm.ID_PhieuMuon == ctpm.ID_PhieuMuon && pm.ID_The == ttv.ID_The
                                      && pm.ID_ThuThu == tt.ID_ThuThu && ttv.ID_DocGia == dg.ID_DocGia 
                                      && ctpm.ID_Sach == s.ID_Sach && ctpm.TrangThai == 0)
                                select new
                                {
                                    MaPM = pm.ID_PhieuMuon,
                                    TenNguoiTao = tt.TenThuThu,
                                    MaTheTV = ttv.ID_The,
                                    MaSV = dg.MaSV,
                                    TenDg = dg.TenDocGia,
                                    MaSach = s.ID_Sach,
                                    TenSach = s.TenSach,
                                    UrlImg = s.UrlImg,
                                    SoLuongMuon = ctpm.SoLuongMuon,
                                    NgayTaoPM = pm.NgayTaoPhieu,
                                    NgayHenTra = pm.NgayHenTra,
                                    GhiChuMuon = pm.GhiChuMuon,
                                    HinhThucMuon = pm.HinhThucMuon
                                };
            if (!string.IsNullOrWhiteSpace(filter))
            {
                dataPhieuMuons = dataPhieuMuons
                    .Where(x => x.MaSV.ToLower().Contains(filter.ToLower()));
            } 

            var ls = _db.LSTraSach
                .GroupBy(x => new { x.ID_PhieuMuon,  x.ID_Sach })
                .Select(x => new
                {
                    ID_PhieuMuon = x.First().ID_PhieuMuon,
                    ID_Sach = x.First().ID_Sach,
                    SoLuongTra = x.Sum(c => c.SoLuongTra)
                }).ToList();

            foreach (var data in dataPhieuMuons)
            {
                SachMuon obj = new SachMuon(data.MaPM, data.TenNguoiTao, data.MaTheTV, data.TenDg, data.MaSach, 
                    data.TenSach, data.SoLuongMuon, data.NgayTaoPM, data.NgayHenTra, data.GhiChuMuon, data.UrlImg, data.HinhThucMuon);
                
                if (ls != null && ls.Any(x => x.ID_PhieuMuon == obj.MaPhieuMuon && x.ID_Sach == obj.MaSach))
                {
                    obj.SoLuongTra = ls.FirstOrDefault(x => x.ID_PhieuMuon == obj.MaPhieuMuon && x.ID_Sach == obj.MaSach)
                        .SoLuongTra;
                } 
                lstPhieuMuon.AddLast(obj);
            }

            //var slTheDangMuon = _db.CTPhieuMuon
            //   .Join(_db.PhieuMuons, x => x.ID_PhieuMuon, y => y.ID_PhieuMuon, (x, y) => new
            //   {
            //       ID_PhieuMuon = x.ID_PhieuMuon,
            //       ID_The = y.ID_The,
            //       TrangThai = x.TrangThai
            //   }).Where(x => x.TrangThai == 0)
            //   .Select(x => x.ID_PhieuMuon)
            //   .Distinct()
            //   .Count();

            ViewBag.TongTheDangMuon = lstPhieuMuon.Count;

            return View(lstPhieuMuon);
        }

        // Get view lập phiếu mượn
        public IActionResult CreatePhieuMuon()
        {
            if (vaiTro.ToLower() != "thủ thư")
            {
                TempData["error"] = "Only librarian can use this feature !";
                return RedirectToAction("ViewSachMuon", "QuanLyMuonTraSach");
            } 

            List<SelectListItem> docGias = _db.DocGias
                .Include(x => x.TheThuViens)
                .ToList()
                .Select(x => new
                {
                    MaSV = x.MaSV,
                    TenDocGia = x.TenDocGia,
                    MaTheThuVien = (x.TheThuViens.FirstOrDefault()?.ID_The ?? -1).ToString()
                })
                .Select(x => new SelectListItem(string.Format("{0} - {1}", x.MaSV, x.TenDocGia), x.MaTheThuVien))
                .ToList();

            docGias.Insert(0, new SelectListItem("-- Select patron --", ""));
            ViewBag.DocGias = docGias;

            List<SelectListItem> sachs = _db.Saches
               .Where(x => x.SoLuong > 0) // Lấy ra sách có số lượng 
               .ToList()
               .Select(x => new SelectListItem(string.Format("{0}", x.TenSach), x.ID_Sach.ToString()))
               .ToList();
            sachs.Insert(0, new SelectListItem("-- Select book --", ""));

            ViewBag.Sachs = sachs;

            List<SelectListItem> hinhThucMuons =
                new List<SelectListItem>()
                {
                    new SelectListItem("-- Select borrow type --", ""),
                    new SelectListItem("Free borrow", "1", true),
                    new SelectListItem("Paid borrow", "2")
                };

            ViewBag.HinhThucMuons = hinhThucMuons;

            return View();
        }

        // Post lập phiếu mượn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SachMuon model)
        {
            List<SelectListItem> docGias = _db.DocGias
                .Include(x => x.TheThuViens)
                .ToList()
                .Select(x => new
                {
                    MaSV = x.MaSV,
                    TenDocGia = x.TenDocGia,
                    MaTheThuVien = (x.TheThuViens.FirstOrDefault()?.ID_The ?? -1).ToString()
                })
                .Select(x => new SelectListItem(string.Format("{0} - {1}", x.MaSV, x.TenDocGia), x.MaTheThuVien))
                .ToList();

            docGias.Insert(0, new SelectListItem("-- Select patron --", ""));
            ViewBag.DocGias = docGias;

            List<SelectListItem> sachs = _db.Saches
               .Where(x => x.SoLuong > 0) // Lấy ra sách có số lượng 
               .Select(x => new SelectListItem(string.Format("{0}", x.TenSach), x.ID_Sach.ToString()))
               .ToList();
            sachs.Insert(0, new SelectListItem("-- Select book --", ""));

            ViewBag.Sachs = sachs;

            List<SelectListItem> hinhThucMuons =
               new List<SelectListItem>()
               {
                    new SelectListItem("-- Select borrow type --", ""),
                    new SelectListItem("Free borrow", "1", true),
                    new SelectListItem("Paid borrow", "2")
               };

            ViewBag.HinhThucMuons = hinhThucMuons;

            // Kiểm tra mã thẻ thư viện, mã sách có tồn tại không
            var theThuVien = _db.TheThuViens.Find(model.MaTheThuVien);
            
            if (theThuVien == null || model.MaTheThuVien <= 0)
            {
                TempData["error"] = "Please select your library card !";
                return View("CreatePhieuMuon", model);
            }

            // Kiểm tra số lượng sách mượn chưa trả
            var slPhieuMuonChuaTra = _db.CTPhieuMuon
                .Join(_db.PhieuMuons, x => x.ID_PhieuMuon, y => y.ID_PhieuMuon, (x, y) => new
                {
                    ID_PhieuMuon = x.ID_PhieuMuon,
                    ID_The = y.ID_The,
                    TrangThai = x.TrangThai
                }).Where(x => x.TrangThai == 0 && x.ID_The == theThuVien.ID_The)
                .Select(x => x.ID_PhieuMuon)
                .Distinct()
                .Count();

            if (slPhieuMuonChuaTra >= 3) // Số phiếu mượn chưa trả lớn hơn 3
            {
                TempData["error"] = "The patron has already reached the maximum limit of 3 borrowed items not yet returned! They must return them before creating a new borrowing request!";
                return RedirectToAction("ViewSachMuon", "QuanLyMuonTraSach");
            } 

            var sach = _db.Saches.Find(model.MaSach);

            if (sach == null || model.MaSach <= 0)
            {
                TempData["error"] = "Please select book !";
                return View("CreatePhieuMuon", model);
            } 

            // True nếu số lượng sách mượn lớn hơn số lượng sách tồn kho
            bool isSoLuongSach = sach.SoLuong - model.SoLuongMuon < 0 ? true : false;

            if (theThuVien == null)
                ModelState.AddModelError("MaTheThuVien", "Library card not existed!");
            if (sach == null)
                ModelState.AddModelError("MaSach", "Book not existed!");
            if (model.SoLuongMuon <= 0)
                ModelState.AddModelError("SoLuongMuon", "The borrowing quantity must be greater than 0!");
             if (isSoLuongSach)
                ModelState.AddModelError("SoLuongMuon", "The number of books in stock is insufficient!");

            if (theThuVien != null && sach != null && !isSoLuongSach && model.SoLuongMuon > 0)
            {
                try
                {
                    // Lấy mã thủ thư
                    var thuthu = from tt in _db.ThuThus
                                 where tt.ID_TaiKhoan == idDN
                                 select tt;
                    int maThuThu = 0;
                    foreach (var tt in thuthu)
                    {
                        maThuThu = tt.ID_ThuThu;
                    }

                    if (maThuThu <= 0)
                    {
                        TempData["error"] = "Only librarian can use this feature !";
                        return View("CreatePhieuMuon", model);
                    } 

                    // Thêm dữ liệu vào bảng phiếu mượn
                    PhieuMuon pm = new PhieuMuon();
                    pm.NgayTaoPhieu = model.NgayTaoPhieu;
                    pm.NgayHenTra = model.NgayHenTra;
                    pm.ID_ThuThu = maThuThu;
                    pm.ID_The = model.MaTheThuVien;
                    pm.HinhThucMuon = model.HinhThucMuon;

                    if (string.IsNullOrEmpty(model.GhiChuMuon))
                        pm.GhiChuMuon = "Empty";
                    else
                        pm.GhiChuMuon = model.GhiChuMuon;

                    await _db.PhieuMuons.AddAsync(pm);
                    await _db.SaveChangesAsync();

                    // Thêm dữ liệu vào bảng chi tiết phiếu mượn
                    CTPhieuMuon ctpm = new CTPhieuMuon();
                    // Lấy mã phiếu mượn vừa thêm vào cơ sở dữ liệu
                    int maPM = _db.PhieuMuons.Max(id => id.ID_PhieuMuon);
                    ctpm.ID_PhieuMuon = maPM;
                    ctpm.ID_Sach = model.MaSach;
                    ctpm.GhiChuTra = "Empty";
                    // Trạng thái: 0 -> đang mượn, 1 -> đã trả
                    ctpm.TrangThai = 0;
                    ctpm.SoLuongMuon = 1; // Fix cứng số lượng 1 là 1
                    await _db.CTPhieuMuon.AddAsync(ctpm);
                    await _db.SaveChangesAsync();

                    // Sửa lại số lượng sách khi lập phiếu mượn
                    await UpdateSach(model.MaSach, 1); // Trừ đi 1 cuốn sau mỗi lần mượn

                    TempData["success"] = "Borrow record created successfully!";
                    return RedirectToAction("ViewSachMuon");
                } 
                catch(Exception ex)
                {
                    TempData["error"] = ex.InnerException.Message;
                    return View("CreatePhieuMuon", model);
                }

            }
            return View("CreatePhieuMuon", model);
        }

        // Phương thức cập nhật số lượng sách khi tạo phiếu mượn
        public async Task UpdateSach(int maSach, int soLuongMuon)
        {
            var sachObj = _db.Saches.Find(maSach);
            int soLuongNew = sachObj.SoLuong - soLuongMuon;
            if(sachObj != null)
            {
                sachObj.SoLuong = soLuongNew;
                _db.Saches.Update(sachObj);
                await _db.SaveChangesAsync();
            }
        }

        // Phương thức cập nhật số lượng sách khi sửa phiếu mượn
        public async Task UpdateSach(int maSach, int soLuongOld, int soLuongNew)
        {
            var sachObj = _db.Saches.Find(maSach);
            int soLuong = sachObj.SoLuong - soLuongNew + soLuongOld;
            if (sachObj != null)
            {
                sachObj.SoLuong = soLuong;
                _db.Saches.Update(sachObj);
                await _db.SaveChangesAsync();
            }
        }

        // Phương thức cập nhật số lượng sách khi xóa phiếu mượn
        public async Task UpdateSachWhenDelete(int maSach, int soLuongMuon)
        {
            Sach? s = _db.Saches.Find(maSach);
            int soLuongNew = s.SoLuong + soLuongMuon;
            if (s != null)
            {
                s.SoLuong = soLuongNew;
                _db.Saches.Update(s);
                await _db.SaveChangesAsync();
            }
        }

        // Get view edit 
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            if (vaiTro.ToLower() != "thủ thư")
            {
                TempData["error"] = "Only librarian can use this feature !";
                return RedirectToAction("ViewSachMuon", "QuanLyMuonTraSach");
            }

            var data = from tt in _db.ThuThus
                      from pm in _db.PhieuMuons
                      from ctpm in _db.CTPhieuMuon
                      from dg in _db.DocGias
                      from s in _db.Saches
                      from ttv in _db.TheThuViens
                      where (pm.ID_PhieuMuon == ctpm.ID_PhieuMuon && pm.ID_The == ttv.ID_The
                            && pm.ID_ThuThu == tt.ID_ThuThu && ttv.ID_DocGia == dg.ID_DocGia
                            && ctpm.ID_Sach == s.ID_Sach && pm.ID_PhieuMuon == id)
                      select new
                      {
                          MaPM = pm.ID_PhieuMuon,
                          TenNguoiTao = tt.TenThuThu,
                          MaTheTV = ttv.ID_The,
                          TenDg = dg.TenDocGia,
                          MaSach = s.ID_Sach,
                          TenSach = s.TenSach,
                          ImgURL = s.UrlImg,
                          SoLuongMuon = ctpm.SoLuongMuon,
                          NgayTaoPM = pm.NgayTaoPhieu,
                          NgayHenTra = pm.NgayHenTra,
                          GhiChuMuon = pm.GhiChuMuon,
                          HinhThucMuon = pm.HinhThucMuon
                      };

            SachMuon? obj = new SachMuon();
            foreach (var i in data.ToList())
            {
                obj = new SachMuon(i.MaPM, i.TenNguoiTao, i.MaTheTV, i.TenDg, i.MaSach,
                    i.TenSach, i.SoLuongMuon, i.NgayTaoPM, i.NgayHenTra, i.GhiChuMuon, i.ImgURL, i.HinhThucMuon);
            }

            if (obj == null)
                return NotFound();

            List<SelectListItem> docGias = _db.DocGias
                 .Include(x => x.TheThuViens)
                 .ToList()
                 .Select(x => new
                 {
                     MaSV = x.MaSV,
                     TenDocGia = x.TenDocGia,
                     MaTheThuVien = (x.TheThuViens.FirstOrDefault()?.ID_The ?? -1).ToString()
                 })
                 .Select(x => new SelectListItem(string.Format("{0} - {1}", x.MaSV, x.TenDocGia), x.MaTheThuVien))
                 .ToList();

            docGias.Insert(0, new SelectListItem("-- Select patron --", "-1"));
            ViewBag.DocGias = docGias;

            List<SelectListItem> sachs = _db.Saches
               .Select(x => new SelectListItem(string.Format("{0}", x.TenSach), x.ID_Sach.ToString()))
               .ToList();
            sachs.Insert(0, new SelectListItem("-- Select book --", "-1"));

            ViewBag.Sachs = sachs;

            List<SelectListItem> hinhThucMuons =
               new List<SelectListItem>()
               {
                    new SelectListItem("-- Select borrow type --", ""),
                    new SelectListItem("Free borrow", "1"),
                    new SelectListItem("Paid borrow", "2")
               };

            ViewBag.HinhThucMuons = hinhThucMuons;

            return View(obj);
        }

        // Post edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SachMuon model)
        {
            List<SelectListItem> docGias = _db.DocGias
                .Include(x => x.TheThuViens)
                .ToList()
                .Select(x => new
                {
                    MaSV = x.MaSV,
                    TenDocGia = x.TenDocGia,
                    MaTheThuVien = (x.TheThuViens.FirstOrDefault()?.ID_The ?? -1).ToString()
                })
                .Select(x => new SelectListItem(string.Format("{0} - {1}", x.MaSV, x.TenDocGia), x.MaTheThuVien))
                .ToList();

            docGias.Insert(0, new SelectListItem("-- Select patron --", "-1"));
            ViewBag.DocGias = docGias;

            List<SelectListItem> sachs = _db.Saches
               .Select(x => new SelectListItem(string.Format("{0}", x.TenSach), x.ID_Sach.ToString()))
               .ToList();
            sachs.Insert(0, new SelectListItem("-- Select book --", "-1"));

            ViewBag.Sachs = sachs;

            List<SelectListItem> hinhThucMuons =
               new List<SelectListItem>()
               {
                    new SelectListItem("-- Select borrow type --", ""),
                    new SelectListItem("Free borrow", "1"),
                    new SelectListItem("Paid borrow", "2")
               };

            ViewBag.HinhThucMuons = hinhThucMuons;

            // Lấy số sách mượn của bảng CTPhieuMuon khi chưa sửa chi tiết phiếu mượn
            var CTPhieuMuon = from ctpm in _db.CTPhieuMuon
                             where ctpm.ID_PhieuMuon == model.MaPhieuMuon
                             select ctpm.SoLuongMuon;
            int soLuongOld = 0;
            foreach(var data in CTPhieuMuon)
            {
                soLuongOld = data;
                break;
            }

            // Kiểm tra mã thẻ thư viện, mã sách có tồn tại không
            var theThuVien = _db.TheThuViens.Find(model.MaTheThuVien);
            var sach = _db.Saches.Find(model.MaSach);
            // True nếu số lượng sách mượn lớn hơn số lượng sách tồn kho
            bool isSoLuongSach = sach.SoLuong - model.SoLuongMuon + soLuongOld < 0 ? true : false;

            if (theThuVien == null)
                ModelState.AddModelError("MaTheThuVien", "Library card not existed!");
            if (sach == null)
                ModelState.AddModelError("MaSach", "Book not existed!");
            if (model.SoLuongMuon <= 0)
                ModelState.AddModelError("SoLuongMuon", "The borrowing quantity must be greater than 0!");
            if (isSoLuongSach)
                ModelState.AddModelError("SoLuongMuon", "The number of books in stock is insufficient!");

            if (theThuVien != null && sach != null && !isSoLuongSach && model.SoLuongMuon > 0)
            {
                try
                {
                    // Lấy mã thủ thư
                    var thuthu = from tt in _db.ThuThus
                                 where tt.ID_TaiKhoan == idDN
                                 select tt;
                    int maThuThu = 0;
                    foreach (var tt in thuthu)
                    {
                        maThuThu = tt.ID_ThuThu;
                    }

                    if (maThuThu <= 0)
                    {
                        TempData["error"] = "Only librarian can use this feature ! ";
                        return View("CreatePhieuMuon", model);
                    }

                    // Sửa dữ liệu bảng phiếu mượn
                    PhieuMuon pm = _db.PhieuMuons.Where(x => x.ID_PhieuMuon == model.MaPhieuMuon)
                        .FirstOrDefault();
                    
                    if (pm == null)
                    {
                        TempData["error"] = "Cannot found borrow record";
                        return View("CreatePhieuMuon", model);
                    }
                    
                    pm.NgayTaoPhieu = model.NgayTaoPhieu;
                    pm.NgayHenTra = model.NgayHenTra;
                    pm.ID_ThuThu = maThuThu;
                    pm.ID_The = model.MaTheThuVien;
                    pm.HinhThucMuon = model.HinhThucMuon;
                    if (string.IsNullOrEmpty(model.GhiChuMuon))
                        pm.GhiChuMuon = "Empty";
                    else
                        pm.GhiChuMuon = model.GhiChuMuon;

                    _db.PhieuMuons.Update(pm);
                    await _db.SaveChangesAsync();

                    // Sửa dữ liệu bảng chi tiết phiếu mượn
                    CTPhieuMuon ctpm = new CTPhieuMuon();
                    ctpm.ID_PhieuMuon = model.MaPhieuMuon;
                    ctpm.ID_Sach = model.MaSach;
                    ctpm.GhiChuTra = "Empty";
                    // Trạng thái: 0 -> đang mượn, 1 -> đã trả
                    ctpm.TrangThai = 0;
                    ctpm.SoLuongMuon = model.SoLuongMuon;
                    _db.CTPhieuMuon.Update(ctpm);
                    await _db.SaveChangesAsync();

                    // Sửa lại số lượng sách khi sửa phiếu mượn
                    await UpdateSach(model.MaSach, soLuongOld, model.SoLuongMuon);

                    TempData["success"] = "Borrowing record updated successfully.";
                    return RedirectToAction("ViewSachMuon");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.InnerException.Message;
                    return View("CreatePhieuMuon", model);
                }

            }
            return View(model);
        }

        // Get view Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            List<SelectListItem> docGias = _db.DocGias
                .Include(x => x.TheThuViens)
                .ToList()
                .Select(x => new
                {
                    MaSV = x.MaSV,
                    TenDocGia = x.TenDocGia,
                    MaTheThuVien = (x.TheThuViens.FirstOrDefault()?.ID_The ?? -1).ToString()
                })
                .Select(x => new SelectListItem(string.Format("{0} - {1}", x.MaSV, x.TenDocGia), x.MaTheThuVien))
                .ToList();

            docGias.Insert(0, new SelectListItem("-- Select patron --", ""));
            ViewBag.DocGias = docGias;

            List<SelectListItem> sachs = _db.Saches
               .Where(x => x.SoLuong > 0) // Lấy ra sách có số lượng 
               .Select(x => new SelectListItem(string.Format("{0}", x.TenSach), x.ID_Sach.ToString()))
               .ToList();
            sachs.Insert(0, new SelectListItem("-- Select book --", ""));

            ViewBag.Sachs = sachs;

            List<SelectListItem> hinhThucMuons =
               new List<SelectListItem>()
               {
                    new SelectListItem("-- Select borrow type --", ""),
                    new SelectListItem("Free borrow", "1"),
                    new SelectListItem("Paid borrow", "2")
               };

            ViewBag.HinhThucMuons = hinhThucMuons;

            var data = from tt in _db.ThuThus
                       from pm in _db.PhieuMuons
                       from ctpm in _db.CTPhieuMuon
                       from dg in _db.DocGias
                       from s in _db.Saches
                       from ttv in _db.TheThuViens
                       where (pm.ID_PhieuMuon == ctpm.ID_PhieuMuon && pm.ID_The == ttv.ID_The
                             && pm.ID_ThuThu == tt.ID_ThuThu && ttv.ID_DocGia == dg.ID_DocGia
                             && ctpm.ID_Sach == s.ID_Sach && pm.ID_PhieuMuon == id)
                       select new
                       {
                           MaPM = pm.ID_PhieuMuon,
                           TenNguoiTao = tt.TenThuThu,
                           MaTheTV = ttv.ID_The,
                           TenDg = dg.TenDocGia,
                           MaSach = s.ID_Sach,
                           TenSach = s.TenSach,
                           UrlImg = s.UrlImg,
                           SoLuongMuon = ctpm.SoLuongMuon,
                           NgayTaoPM = pm.NgayTaoPhieu,
                           NgayHenTra = pm.NgayHenTra,
                           GhiChuMuon = pm.GhiChuMuon,
                           HinhThucMuon = pm.HinhThucMuon
                       };

            SachMuon? obj = new SachMuon();
            foreach (var i in data)
            {
                obj = new SachMuon(i.MaPM, i.TenNguoiTao, i.MaTheTV, i.TenDg, i.MaSach,
                    i.TenSach, i.SoLuongMuon, i.NgayTaoPM, i.NgayHenTra, i.GhiChuMuon, i.UrlImg, i.HinhThucMuon);
            }

            if (obj == null)
                return NotFound();
            return View(obj);
        }

        // Post delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? maPhieuMuon)
        {
            if(maPhieuMuon != null)
            {
                try
                {
                    // Select dữ liệu bảng chi tiết phiếu mượn qua mã phiếu mượn
                    CTPhieuMuon? ctpm = _db.CTPhieuMuon.First(e => e.ID_PhieuMuon == maPhieuMuon);

                    // Select dữ liệu bảng phiếu mượn qua mã phiếu mượn
                    PhieuMuon? pm = _db.PhieuMuons.Find(maPhieuMuon);

                    if (ctpm != null && pm != null)
                    {
                        _db.Remove(ctpm);
                        _db.PhieuMuons.Remove(pm);
                        await _db.SaveChangesAsync();

                        await UpdateSachWhenDelete(ctpm.ID_Sach, ctpm.SoLuongMuon);

                        TempData["success"] = "Borrowing record deleted successfully.";
                        return RedirectToAction("ViewSachMuon");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.InnerException.Message;
                    return View("Delete", maPhieuMuon);
                }
            }
            return NotFound();
        }

        // Post trả sách
        public async Task<IActionResult> TraSach(int? id)
        {
            if (id == null) return NotFound();
            // Select dữ liệu bảng chi tiết phiếu mượn qua mã phiếu mượn
            CTPhieuMuon? ctpm = _db.CTPhieuMuon.First(e => e.ID_PhieuMuon == id);
            // 1 -> đã trả
            ctpm.TrangThai = 1;
            ctpm.NgayTra = DateTime.Now;

            _db.CTPhieuMuon.Update(ctpm);
            await _db.SaveChangesAsync();

            // Đợi cập nhật thông tin sách
            int maSach = _db.CTPhieuMuon.First(e => e.ID_PhieuMuon == id).ID_Sach;
            await UpdateSlSachKhiTra(maSach, ctpm.SoLuongMuon);

            TempData["success"] = "Book return confirmed successfully!";
            return RedirectToAction("ViewSachMuon");
        }


        // Phương thức thay đổi số lượng sách khi xác nhận trả sách
        public async Task UpdateSlSachKhiTra(int maSach, int slTra)
        {
            Sach? s = _db.Saches.Find(maSach);
            if(s != null)
            {
                s.SoLuong += slTra;
                _db.Saches.Update(s);
                await _db.SaveChangesAsync();
            }
        }

        // Get view sách đã trả
        public IActionResult ViewSachTra()
        {
            var obj = from s in _db.Saches
                      from dg in _db.DocGias
                      from lsts in _db.LSTraSach
                      from ttv in _db.TheThuViens
                      from pm in _db.PhieuMuons
                      where s.ID_Sach == lsts.ID_Sach 
                        && dg.ID_DocGia == ttv.ID_DocGia 
                        && lsts.ID_PhieuMuon == pm.ID_PhieuMuon && ttv.ID_The == pm.ID_The
                        && lsts.SoTienPhat <= 0
                      select new
                      {
                          sachTra = new SachTra(
                              lsts.ID_PhieuMuon, s.ID_Sach, s.TenSach, ttv.ID_The
                              , dg.TenDocGia, lsts.SoLuongTra, lsts.NgayTra, lsts.GhiChuTra
                              , lsts.TrangThai, lsts.MucDo, lsts.SoLuongTra, lsts.SoTienPhat, lsts.GhiChuPhat)
                      };

            LinkedList<SachTra> lstSachTra = new LinkedList<SachTra>();

            foreach(var item in obj)
            {
                lstSachTra.AddLast(item.sachTra);
            }

            return View(lstSachTra);
        }

        public IActionResult ViewSachPhat()
        {
            var obj = from s in _db.Saches
                      from dg in _db.DocGias
                      from lsts in _db.LSTraSach
                      from ttv in _db.TheThuViens
                      from pm in _db.PhieuMuons
                      where s.ID_Sach == lsts.ID_Sach 
                        && dg.ID_DocGia == ttv.ID_DocGia 
                        && lsts.ID_PhieuMuon == pm.ID_PhieuMuon 
                        && ttv.ID_The == pm.ID_The
                        && lsts.SoTienPhat > 0
                      select new
                      {
                          sachTra = new SachTra(
                              lsts.ID_PhieuMuon, s.ID_Sach, s.TenSach, ttv.ID_The
                              , dg.TenDocGia, lsts.SoLuongTra, lsts.NgayTra, lsts.GhiChuTra
                              , lsts.TrangThai, lsts.MucDo, lsts.SoLuongTra, lsts.SoTienPhat, lsts.GhiChuPhat)
                      };

            LinkedList<SachTra> lstSachTra = new LinkedList<SachTra>();
            foreach (var item in obj)
            {
                lstSachTra.AddLast(item.sachTra);
            }

            return View(lstSachTra);
        }
    }
}
