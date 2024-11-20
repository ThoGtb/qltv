using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            MuonTras = new HashSet<MuonTra>();
        }

        public string MaNhanVien { get; set; } = null!;
        public string TenNhanVien { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? ChucVu { get; set; }

        public virtual ICollection<MuonTra> MuonTras { get; set; }
    }
}
