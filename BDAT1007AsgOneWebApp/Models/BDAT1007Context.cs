using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BDAT1007AsgOneWebApp.Models
{
    public partial class BDAT1007Context : DbContext
    {
        public BDAT1007Context()
        {
        }

        public BDAT1007Context(DbContextOptions<BDAT1007Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TicketOptions> TicketOptions { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-TVDSVH6;Database=BDAT1007;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketOptions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Esource)
                    .HasColumnName("ESource")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.League)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Seatgeek)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Stubhub)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TeamName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ticketmaster)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
