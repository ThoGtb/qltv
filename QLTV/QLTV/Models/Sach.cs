using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class Sach
    {
        public Sach()
        {
            MuonTras = new HashSet<MuonTra>();
        }

        public string MaSach { get; set; } = null!;
        public string TenSach { get; set; } = null!;
        public string? MaTheLoai { get; set; }
        public string? MaTacGia { get; set; }
        public string? MaNxb { get; set; }
        public DateTime? NgayXuatBan { get; set; }
        public int? SoLuong { get; set; }

        public virtual NhaXuatBan? MaNxbNavigation { get; set; }
        public virtual TacGium? MaTacGiaNavigation { get; set; }
        public virtual TheLoai? MaTheLoaiNavigation { get; set; }
        public virtual ICollection<MuonTra> MuonTras { get; set; }
    }
}
