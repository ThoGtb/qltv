using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaTheLoai { get; set; } = null!;
        public string TenTheLoai { get; set; } = null!;

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
