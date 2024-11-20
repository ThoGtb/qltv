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
        private readonly ILogger<HomeController> _logger;
        private readonly QLTVContext _DB;

        public HomeController(ILogger<HomeController> logger, QLTVContext dB)
        {
            _logger = logger;
            _DB = dB;
        }

        public IActionResult Index()
        {
            // Retrieve all MuonTra records along with related data
            var muonTraList = _DB.MuonTras
                .Select(mt => new
                {
                    mt.MaMuonTra,
                    MaDocGia = mt.MaDocGiaNavigation.TenDocGia,
                    MaSach = mt.MaSachNavigation.TenSach,
                    MaNhanVien = mt.MaNhanVienNavigation.TenNhanVien,
                    mt.NgayMuon,
                    mt.HanTra,
                    mt.NgayTra,
                    mt.TrangThai
                })
                .ToList();

            return View(muonTraList);
        }
        public IActionResult TrangChu()
        {
            return View();
        }

        //Danh sách sinh viên
        public IActionResult DSSinhVien(int? page)
        {
            var danhSach = _DB.MuonTras.Include(x=>x.MaDocGia).Include(x=>x.MaSach).Include(x=>x.MaNhanVien).ToList();
            return View(danhSach);
        }

        //Trang thông báo từ chối truy cập
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ThemMuonTra()
        {
            // Populate dropdown lists
            ViewBag.MaDocGia = new SelectList(_DB.DocGias, "MaDocGia", "TenDocGia"); // Assuming "TenDocGia" is a meaningful display name
            ViewBag.MaSach = new SelectList(_DB.Sachs, "MaSach", "TenSach");
            ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien");

            return View();
        }

        // POST: ThemMuonTra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMuonTra(MuonTra muon)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate dropdown lists in case of form re-rendering
                ViewBag.MaDocGia = new SelectList(_DB.DocGias, "MaDocGia", "TenDocGia");
                ViewBag.MaSach = new SelectList(_DB.Sachs, "MaSach", "TenSach");
                ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien");
                return View(muon);
            }

            // Check for duplicate "MaMuonTra"
            if (_DB.MuonTras.Any(e => e.MaMuonTra == muon.MaMuonTra))
            {
                ViewBag.ThongBao = "Mã mượn trả đã tồn tại. Vui lòng kiểm tra lại.";

                // Handle dropdown lists again if validation fails
                ViewBag.MaDocGia = new SelectList(_DB.DocGias, "MaDocGia", "TenDocGia");
                ViewBag.MaSach = new SelectList(_DB.Sachs, "MaSach", "TenSach");
                ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien");

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
        public IActionResult CapNhatMuonTra(string maMuonTra)
        {
            var muonTra = _DB.MuonTras.FirstOrDefault(mt => mt.MaMuonTra == maMuonTra);
            if (muonTra == null)
            {
                TempData["ThongBaoSua"] = "Không tìm thấy thông tin mượn trả.";
                return RedirectToAction("DanhSachMuonTra");
            }

            // Populate dropdown lists
            ViewBag.MaDocGia = new SelectList(_DB.DocGias, "MaDocGia", "TenDocGia", muonTra.MaDocGia);
            ViewBag.MaSach = new SelectList(_DB.Sachs, "MaSach", "TenSach", muonTra.MaSach);
            ViewBag.MaNhanVien = new SelectList(_DB.NhanViens, "MaNhanVien", "TenNhanVien", muonTra.MaNhanVien);

            return View(muonTra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatMuonTra(MuonTra muonTra)
        {
            try
            {
                if (muonTra != null)
                {
                    _DB.MuonTras.Update(muonTra);
                    _DB.SaveChanges();
                    TempData["ThongBaoSua"] = "Cập nhật mượn trả thành công.";
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

            return RedirectToAction("DanhSachMuonTra");
        }


    }
}
