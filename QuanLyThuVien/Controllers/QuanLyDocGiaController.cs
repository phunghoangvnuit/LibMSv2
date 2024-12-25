using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class QuanLyDocGiaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public QuanLyDocGiaController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Tìm kiếm độc giả
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string? searchInput)
        {
            List<DocGia> lstDocGia = new List<DocGia>();

            lstDocGia = _db.DocGias.Where(x => x.TenDocGia.ToUpper().Contains(searchInput.ToUpper())
                || x.DiaChi.ToUpper().Contains(searchInput.ToUpper())
                || x.SDT.ToUpper().Contains(searchInput.ToUpper())
                || x.Email.ToUpper().Contains(searchInput.ToUpper())
                || x.MaSV.ToUpper().Contains(searchInput.ToUpper()))
                .ToList();

            if (lstDocGia.Count <= 0)
            {
                TempData["message"] = "Không tìm thấy thông độc giả";
                return View("Index", lstDocGia);
            }

            TempData["message"] = $"Tìm thấy {lstDocGia.Count()} kết quả";
            return View("Index", lstDocGia);
        }


        // GET View Index
        public IActionResult Index()
        {
            IQueryable<DocGia> lstDG = getDataDocGia();
            if(lstDG.Count() <= 0)
            {
                TempData["message"] = "Dữ liệu trống";
            }
            return View(lstDG);
        }

        // Phương thức get data DocGia
        public IQueryable<DocGia> getDataDocGia()
        {
            var obj = from dg in _db.DocGias
                      select dg;
            return obj;
        }

        // GET view Create
        public IActionResult Create()
        {
            return View();
        }

        // POST create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(DocGia docGia)
        {
            string errMsg = "";

            try
            {
                if (docGia == null) { return BadRequest(); }
                // Kiểm tra tồn tại độc giả
                if (_db.DocGias.Any(x => x.Email == docGia.Email
                 || x.MaSV == docGia.MaSV))
                {
                    TempData["error"] = "Đã tồn tại độc giả với thông tin email và mã sinh viên được nhập !";
                    return View("Create", docGia);
                }
                
                bool isAddTaiKhoan = addTaiKhoanDocGia(docGia.Email);

                if (isAddTaiKhoan)
                {
                    try
                    {
                        int maTK = getMaTK(docGia.Email);

                        if (maTK != 0)
                        {
                            DocGia dg = docGia;
                            dg.ID_TaiKhoan = maTK;
                            await _db.DocGias.AddAsync(dg);
                            int ret = await _db.SaveChangesAsync();

                            if (ret > 0) // Thêm thẻ thư viện
                            {
                                bool isAddTheThuVien = addTheThuVien(dg);

                                if (isAddTheThuVien)
                                {
                                    TempData["success"] = "Thêm mới độc giả thành công";
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    errMsg = "Thêm thẻ thư viện không thành công !";
                                } 
                            }
                            else
                            {
                                errMsg = "Thêm độc giả không thành công !";
                            } 

                        }
                    } 
                    catch(Exception ex)
                    {
                        removeTaiKhoan(docGia.Email);
                        removeTheThuVien(docGia.MaSV);

                        TempData["error"] = ex.InnerException.Message;
                        return View("Create", docGia);
                    }
                }
                else
                {
                    errMsg = "Thêm tài khoản không thành công !";
                } 
            } 
            catch(Exception ex)
            {
                TempData["error"] = ex.InnerException.Message;
                return View("Create", docGia);
            }
            TempData["error"] = errMsg;
            return View("Create", docGia);
        }

        // GET view Edit
        public IActionResult Edit(int? id)
        {
            var obj = from dg in _db.DocGias
                      where dg.ID_DocGia == id
                      select dg;

            foreach (var docgia in obj)
            {
                return View(docgia);
            }

            return NotFound();
        }

        // Post Edit
        public async Task<IActionResult> EditPost(DocGia obj)
        {
            try
            {
                _db.DocGias.Update(obj);
                await _db.SaveChangesAsync();
                TempData["success"] = "Sửa thông tin độc giả thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }
        }

        // Get view delete
        public IActionResult Delete(int? id)
        {
            var obj = _db.DocGias.Find(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // Post Delete
        public async Task<IActionResult> DeletePost(int? idDocGia)
        {
            var obj = _db.DocGias.Find(idDocGia);
            if (obj == null) return NotFound();
            try
            {
                _db.DocGias.Remove(obj);
                await _db.SaveChangesAsync();

                /*
                    Khi xóa độc giả thì sẽ xóa luôn cả thông tin tài khoản 
                    và thẻ thư viện của độc giả đó.
                 */
                removeTaiKhoan(obj.Email);
                removeTheThuVien(obj.MaSV);

                TempData["success"] = "Xóa độc giả thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }
        }

        /*
            Phương thức tạo tài khoản khi thêm mới độc giả
            Tên tài khoản = email
            Mật khẩu = 123456;
         */
        public bool addTaiKhoanDocGia(string email)
        {
            if(email == null) { return false; }
            TaiKhoan tk = new TaiKhoan();
            tk.TenDangNhap = email;
            tk.MatKhau = "123456";
            tk.VaiTro = "Độc giả";

            _db.TaiKhoans.AddAsync(tk);
            if(_db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        // Phương thức lấy mã tài khoản độc giả
        public int getMaTK(string tentk)
        {
            var maTK = from tk in _db.TaiKhoans
                       where tk.TenDangNhap == tentk
                       select tk.ID_TaiKhoan;
            foreach(var item in maTK)
            {
                return item;
            }

            return 0;
        }

        /*
            Phương thức tạo thẻ thư viện khi thêm mới độc giả
            NgayDB = DateTime.Now;
            Thời hạn của 1 thẻ là 3 năm kể từ ngày tạo
            Lưu thông tin ghi chú = số MaSV
         */
        public bool addTheThuVien(DocGia dg)
        {
            TheThuVien the = new TheThuVien();
            the.NgayBD = DateTime.Now;
            the.NgayHetHan = new DateTime(the.NgayBD.Year + 3, the.NgayBD.Month, the.NgayBD.Day);
            the.SoThe = dg.MaSV;
            the.ID_DocGia = dg.ID_DocGia;
            _db.TheThuViens.AddAsync(the);
            if (_db.SaveChanges() > 0) return true;
            return false;
        }

        // Phương thức lấy mã thẻ thư viện
        public int getMaThe(string soThe)
        {
            var maThes = from th in _db.TheThuViens
                         where th.SoThe == soThe
                         select th.ID_The;
            foreach(var item in maThes)
            {
                return item;
            }

            return 0;
        }

        // Phương thức xóa thông tin tài khoản nếu thêm độc giả xảy ra lỗi
        public void removeTaiKhoan(string email)
        {
            var obj = from tk in _db.TaiKhoans
                      where tk.TenDangNhap == email
                      select tk;
            TaiKhoan taiKhoan = new TaiKhoan();
            foreach(var i in obj)
            {
                taiKhoan = i;
            }
            _db.Remove(taiKhoan);
            _db.SaveChanges();
        }

        // Phương thức xóa thông tin thẻ thư viện nếu thêm độc giả xảy ra lỗi
        public void removeTheThuVien(string soCCCD)
        {
            var obj = from t in _db.TheThuViens
                      where t.SoThe == soCCCD
                      select t;

            TheThuVien the = new TheThuVien();
            foreach (var i in obj)
            {
                the = i;
            }
            _db.Remove(the);
            _db.SaveChanges();
        }


    }
}
