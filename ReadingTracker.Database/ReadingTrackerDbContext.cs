using ReadingTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ReadingTracker.Database
{

    public class ReadingTrackerDbContext : DbContext
    {
        public ReadingTrackerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<ReaderBook> ReaderBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ReaderBook>()
                .HasKey(rb => new { rb.BookId, rb.ReaderId });

            modelBuilder.Entity<ReaderBook>()
                .HasOne(rb => rb.Book)
                .WithMany(b => b.ReaderBooks)
                .HasForeignKey(rb => rb.BookId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ReaderBook>()
                .HasOne(rb => rb.Reader)
                .WithMany(r => r.ReaderBooks)
                .HasForeignKey(rb => rb.ReaderId)
                .OnDelete(DeleteBehavior.Cascade); 
        }


    }
}
