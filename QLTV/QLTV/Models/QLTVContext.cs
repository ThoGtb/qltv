using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLTV.Models
{
    public partial class QLTVContext : DbContext
    {
        private readonly IEnumerable DocGias;
        private readonly IEnumerable Sachs;

        public QLTVContext()
        {
        }

        public QLTVContext(DbContextOptions<QLTVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DangKi> DangKis { get; set; } = null!;
        public virtual DbSet<DocGium> DocGia { get; set; } = null!;
        public virtual DbSet<MuonTra> MuonTras { get; set; } = null!;
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<Sach> Saches { get; set; } = null!;
        public virtual DbSet<TacGium> TacGia { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<TheLoai> TheLoais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-928743O; Database=QLTV; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DangKi>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DangKi");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.NgayDangKi).HasColumnType("date");
            });

            modelBuilder.Entity<DocGium>(entity =>
            {
                entity.HasKey(e => e.MaDocGia)
                    .HasName("PK__DocGia__F165F94590BECD68");

                entity.Property(e => e.MaDocGia).HasMaxLength(10);

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.NgayDangKy).HasColumnType("date");

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai).HasMaxLength(15);

                entity.Property(e => e.TenDocGia).HasMaxLength(255);
            });

            modelBuilder.Entity<MuonTra>(entity =>
            {
                entity.HasKey(e => e.MaMuonTra)
                    .HasName("PK__MuonTra__2E4C717E693A67B5");

                entity.ToTable("MuonTra");

                entity.Property(e => e.MaMuonTra).HasMaxLength(10);

                entity.Property(e => e.HanTra).HasColumnType("date");

                entity.Property(e => e.MaDocGia).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.MaSach).HasMaxLength(10);

                entity.Property(e => e.NgayMuon).HasColumnType("date");

                entity.Property(e => e.NgayTra).HasColumnType("date");

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.MaDocGiaNavigation)
                    .WithMany(p => p.MuonTras)
                    .HasForeignKey(d => d.MaDocGia)
                    .HasConstraintName("FK__MuonTra__MaDocGi__45F365D3");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.MuonTras)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK__MuonTra__MaNhanV__47DBAE45");

                entity.HasOne(d => d.MaSachNavigation)
                    .WithMany(p => p.MuonTras)
                    .HasForeignKey(d => d.MaSach)
                    .HasConstraintName("FK__MuonTra__MaSach__46E78A0C");
            });

            modelBuilder.Entity<NhaXuatBan>(entity =>
            {
                entity.HasKey(e => e.MaNxb)
                    .HasName("PK__NhaXuatB__3A19482CFBD7A2A0");

                entity.ToTable("NhaXuatBan");

                entity.Property(e => e.MaNxb)
                    .HasMaxLength(10)
                    .HasColumnName("MaNXB");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.SoDienThoai).HasMaxLength(15);

                entity.Property(e => e.TenNxb)
                    .HasMaxLength(255)
                    .HasColumnName("TenNXB");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__NhanVien__77B2CA47136EB393");

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.ChucVu).HasMaxLength(100);

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.SoDienThoai).HasMaxLength(15);

                entity.Property(e => e.TenNhanVien).HasMaxLength(255);
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.HasKey(e => e.MaSach)
                    .HasName("PK__Sach__B235742D72401BAF");

                entity.ToTable("Sach");

                entity.Property(e => e.MaSach).HasMaxLength(10);

                entity.Property(e => e.MaNxb)
                    .HasMaxLength(10)
                    .HasColumnName("MaNXB");

                entity.Property(e => e.MaTacGia).HasMaxLength(10);

                entity.Property(e => e.MaTheLoai).HasMaxLength(10);

                entity.Property(e => e.NgayXuatBan).HasColumnType("date");

                entity.Property(e => e.TenSach).HasMaxLength(255);

                entity.HasOne(d => d.MaNxbNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaNxb)
                    .HasConstraintName("FK__Sach__MaNXB__3F466844");

                entity.HasOne(d => d.MaTacGiaNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaTacGia)
                    .HasConstraintName("FK__Sach__MaTacGia__3E52440B");

                entity.HasOne(d => d.MaTheLoaiNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaTheLoai)
                    .HasConstraintName("FK__Sach__MaTheLoai__3D5E1FD2");
            });

            modelBuilder.Entity<TacGium>(entity =>
            {
                entity.HasKey(e => e.MaTacGia)
                    .HasName("PK__TacGia__F24E67566A0FBE98");

                entity.Property(e => e.MaTacGia).HasMaxLength(10);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.QuocTich).HasMaxLength(100);

                entity.Property(e => e.TenTacGia).HasMaxLength(255);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.MatKhau).HasMaxLength(10);

                entity.Property(e => e.Quyen).HasMaxLength(10);

                entity.Property(e => e.TaiKhoan1).HasMaxLength(10);
            });

            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.HasKey(e => e.MaTheLoai)
                    .HasName("PK__TheLoai__D73FF34A6EBD191C");

                entity.ToTable("TheLoai");

                entity.Property(e => e.MaTheLoai).HasMaxLength(10);

                entity.Property(e => e.TenTheLoai).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
