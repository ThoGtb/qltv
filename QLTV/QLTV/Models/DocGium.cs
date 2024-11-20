using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class DocGium
    {
        public DocGium()
        {
            MuonTras = new HashSet<MuonTra>();
        }

        public string MaDocGia { get; set; } = null!;
        public string TenDocGia { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public DateTime? NgayDangKy { get; set; }

        public virtual ICollection<MuonTra> MuonTras { get; set; }
    }
}
