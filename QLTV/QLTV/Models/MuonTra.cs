using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class MuonTra
    {
        public string MaMuonTra { get; set; } = null!;
        public string? MaDocGia { get; set; }
        public string? MaSach { get; set; }
        public string? MaNhanVien { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public DateTime? HanTra { get; set; }
        public string? TrangThai { get; set; }

        public virtual DocGium? MaDocGiaNavigation { get; set; }
        public virtual NhanVien? MaNhanVienNavigation { get; set; }
        public virtual Sach? MaSachNavigation { get; set; }
    }
}
