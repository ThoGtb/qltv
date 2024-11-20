using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class NhaXuatBan
    {
        public NhaXuatBan()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaNxb { get; set; } = null!;
        public string TenNxb { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
