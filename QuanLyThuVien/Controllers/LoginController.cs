using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        public static int id;
        // Model lưu thông tin tên đăng nhập, vai trò dùng chuyển dữ liệu qua _Layout.cshtml
        public static LayoutModel layout;

        public IActionResult Index()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string taikhoan, string matkhau)
        {
            string username = taikhoan;
            string password = matkhau;
            
            // Lấy ra thông tin người dùng theo tên đăng nhập và tài khoản được nhập.

            var obj = from tk in _db.TaiKhoans
                      where (tk.TenDangNhap == username && tk.MatKhau == password)
                      select tk;

            // Duyệt qua danh sách nếu danh sách có phần tử => đăng nhập được vào hệ thống

            foreach (var taiKhoan in obj)
            {
                // Nếu tài khoản có vai trò là admin hoặc thủ thư
                // thì cho phép đăng nhập, điều hướng đễn trang Home

                if (taiKhoan.VaiTro == "Admin" || taiKhoan.VaiTro == "Thủ thư")
                {
                    // Lưu lại id của người đăng nhập
                    id = taiKhoan.ID_TaiKhoan;
                    // Lưu lại thông tin của người đăng nhập và vai trò
                    layout = new LayoutModel(taiKhoan.TenDangNhap, taiKhoan.VaiTro);
                    // Chuyển hướng đến trang Home
                    return RedirectToAction("Index", "Home");

                    /*return RedirectToRoute(new
                    {
                        Controller = "Home",
                        Action = "Index"
                    });*/

                    /*return View("Views/Home/Index.cshtml", id);*/
                } else // Nếu không có quyền admin hay thủ thư thì không cho truy cập
                {
                    TempData["message"] = "Bạn không có quyền truy cập vào hệ thống!!!";
                    return View("Index");
                }
            }
            // Nếu không tìm thấy tài khoản cũng không cho truy cập.
            TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác!!!";
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            return View("Index");
        }
    }
}