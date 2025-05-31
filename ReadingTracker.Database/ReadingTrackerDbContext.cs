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
            modelBuilder.Entity<ReaderBook>()
                .HasKey(rb => new { rb.BookId, rb.ReaderId });

            modelBuilder.Entity<ReaderBook>()
                .HasOne(rb => rb.Book)
                .WithMany(b => b.ReaderBooks)
                .HasForeignKey(rb => rb.BookId);

            modelBuilder.Entity<ReaderBook>()
                .HasOne(rb => rb.Reader)
                .WithMany(r => r.ReaderBooks)
                .HasForeignKey(rb => rb.ReaderId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
