using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTV.Models;
using System.Diagnostics;

namespace QLTV.Controllers
{
    public class HomeController : Controller
    {
        MuonTra muon = new MuonTra();
        QLTVContext tv = new QLTVContext();
        private readonly ILogger<HomeController> _logger;
        private readonly QLTVContext _DB;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, QLTVContext dB, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _DB = dB;   
            _webHostEnvironment = webHostEnvironment;
        }
   
        public IActionResult Index()
        {
            var danhSachMuonTra = _DB.MuonTras.Include(mt => mt.MaDocGiaNavigation)
                                               .Include(mt => mt.MaSachNavigation)
                                               .Include(mt => mt.MaNhanVienNavigation)
                                               .ToList();
            return View(danhSachMuonTra);
        }

        public IActionResult DanhSachMuonTra()
        {
            var danhSachMuonTra = _DB.MuonTras.Include(mt => mt.MaDocGiaNavigation)
                                               .Include(mt => mt.MaSachNavigation)
                                               .Include(mt => mt.MaNhanVienNavigation)
                                               .ToList();
            return View(danhSachMuonTra);
        }

        //Trang thông báo từ chối truy cập
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("ThemMuonTra")]
        [HttpGet]
        public IActionResult ThemMuonTra()
        {
            // Populate dropdown lists
            // Populate dropdown lists for DocGia, Sach, NhanVien
            ViewBag.MaDocGia = new SelectList(_DB.DocGia, "MaDocGia", "TenDocGia"); // Assuming "TenDocGia" is a meaningful display name
            ViewBag.MaSach = new SelectList(_DB.Saches, "MaSach" , "TenSach");
            ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien");


            return View();
        }

        [Route("ThemMuonTra")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMuonTra(MuonTra muon)
        {
            if (!ModelState.IsValid)
            {
                // Populate dropdown lists
                // Populate dropdown lists for DocGia, Sach, NhanVien
                ViewBag.MaDocGia = new SelectList(_DB.DocGia, "MaDocGia", "TenDocGia"); // Assuming "TenDocGia" is a meaningful display name
                ViewBag.MaSach = new SelectList(_DB.Saches, "MaSach", "TenSach");
                ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien");

                return View(muon);
            }

            // Check for duplicate "MaMuonTra"
            if (_DB.MuonTras.Any(e => e.MaMuonTra == muon.MaMuonTra))
            {
                ViewBag.ThongBao = "Mã mượn trả đã tồn tại. Vui lòng kiểm tra lại.";

                // Handle dropdown lists again if validation fails
                // Populate dropdown lists for DocGia, Sach, NhanVien
                ViewBag.MaDocGia = new SelectList(_DB.DocGias, "MaDocGia", "MaDocGia"); // Assuming "TenDocGia" is a meaningful display name
                ViewBag.MaSach = new SelectList(_DB.Sachs, "MaSach", "MaSach");
                ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "MaNhanVien");

                return View(muon);
            }

            // Add the new record
            _DB.MuonTras.Add(muon);
            _DB.SaveChanges();

            TempData["ThongBaoThem"] = "Thêm mượn trả thành công.";
            return RedirectToAction("DanhSachMuonTra"); // Redirect to a list view or another relevant action
        }

        //Xóa sinh viên
        public IActionResult XoaMuonTra(string maMuonTra)
        {
            try
            {
                var muonTra = _DB.MuonTras.FirstOrDefault(mt => mt.MaMuonTra == maMuonTra);
                if (muonTra != null)
                {
                    _DB.MuonTras.Remove(muonTra);
                    _DB.SaveChanges();
                    TempData["ThongBaoXoa"] = "Xóa mượn trả thành công.";
                }
                else
                {
                    TempData["ThongBaoXoa"] = "Không tìm thấy thông tin mượn trả.";
                }
            }
            catch (Exception ex)
            {
                TempData["ThongBaoXoa"] = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return RedirectToAction("DanhSachMuonTra");
        }


        //Cập nhật thông tin sinh viên
        // GET: Cập nhật Mượn Trả
        [Route("CapNhatMuonTra")]
        [HttpGet]
        public IActionResult CapNhatMuonTra(string maMuonTra)
        {
            // Lấy thông tin mượn trả theo mã
            var muonTra = _DB.MuonTras.Include(mt => mt.MaDocGiaNavigation)
                                               .Include(mt => mt.MaSachNavigation)
                                               .Include(mt => mt.MaNhanVienNavigation)
                .FirstOrDefault(mt => mt.MaMuonTra == maMuonTra);

            if (muonTra == null)
            {
                TempData["ThongBaoSua"] = "Không tìm thấy thông tin mượn trả.";
                return RedirectToAction("DanhSachMuonTra");
            }

            // Populate dropdown lists
            ViewBag.MaDocGia = new SelectList(_DB.DocGia, "MaDocGia", "TenDocGia"); // Assuming "TenDocGia" is a meaningful display name
            ViewBag.MaSach = new SelectList(_DB.Saches, "MaSach", "TenSach");
            ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien");


            return View(muonTra); // Trả về View với thông tin mượn trả cần cập nhật

        }

        // POST: Cập nhật Mượn Trả
        [Route("CapNhatMuonTra")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatMuonTra(MuonTra muonTra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingMuonTra = _DB.MuonTras.FirstOrDefault(mt => mt.MaMuonTra == muonTra.MaMuonTra);
                    if (existingMuonTra != null)
                    {
                        // Cập nhật thông tin mượn trả
                        existingMuonTra.MaDocGia = muonTra.MaDocGia;
                        existingMuonTra.MaSach = muonTra.MaSach;
                        existingMuonTra.MaNhanVien = muonTra.MaNhanVien;
                        existingMuonTra.NgayMuon = muonTra.NgayMuon;
                        existingMuonTra.NgayTra = muonTra.NgayTra;

                        _DB.MuonTras.Update(existingMuonTra);
                        _DB.SaveChanges();

                        TempData["ThongBaoSua"] = "Cập nhật mượn trả thành công.";
                    }
                    else
                    {
                        TempData["ThongBaoSua"] = "Không tìm thấy thông tin mượn trả.";
                    }
                }
                else
                {
                    TempData["ThongBaoSua"] = "Thông tin không hợp lệ.";
                }
            }
            catch (Exception ex)
            {
                TempData["ThongBaoSua"] = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return RedirectToAction("DanhSachMuonTra"); // Quay về trang danh sách mượn trả
        }


    }
}
