using System;
using System.Collections.Generic;

namespace QLTV.Models
{
    public partial class TacGium
    {
        public TacGium()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaTacGia { get; set; } = null!;
        public string TenTacGia { get; set; } = null!;
        public string? QuocTich { get; set; }
        public DateTime? NgaySinh { get; set; }

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
