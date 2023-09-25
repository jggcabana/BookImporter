using BookImporter.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Repositories.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<ImportBatch> ImportBatches { get; set; } 

        public DbSet<ImportLog> ImportLogs { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(b =>
                {
                    b.Property("BooksId").HasColumnName("BookId");
                    b.Property("AuthorsId").HasColumnName("AuthorId");
                });

            builder.Entity<Book>()
                .ToTable("Books", builder => builder.Property("Id").HasColumnName("BookId"))
                .HasIndex(b => b.ISBN)
                .IsUnique(true);

            builder.Entity<Author>()
                .ToTable("Authors", builder => builder.Property("Id").HasColumnName("AuthorId"))
                .HasIndex(a => a.Name)
                .IsUnique(true);

            builder.Entity<ImportBatch>()
                .ToTable("ImportBatches", builder => builder.Property("Id").HasColumnName("ImportBatchId"));

            builder.Entity<ImportLog>()
                .ToTable("ImportLogs", builder => builder.Property("Id").HasColumnName("ImportLogId"));

            base.OnModelCreating(builder);
        }
    }
}
