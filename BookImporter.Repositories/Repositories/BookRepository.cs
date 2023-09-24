using BookImporter.Entities.Models;
using BookImporter.Repositories.Interfaces;
using BookImporter.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Repositories.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            // TODO: lazy loading? 
            return await _context.Books.Include(b => b.Authors).ToListAsync();
        }

        public async Task<ImportBatch> CreateImportBatchAsync(ImportBatch batch)
        {
            var entity = (await _context.ImportBatches.AddAsync(batch)).Entity;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task CreateImportLogAsync(ImportLog log)
        {
            var entity = (await _context.ImportLogs.AddAsync(log)).Entity;
            await _context.SaveChangesAsync();
        }

        public async Task<int> ImportBookAsync(Book book)
        {
            var bookEntity = _context.Books.Include(x => x.Authors).FirstOrDefault(x => x.ISBN == book.ISBN);

            if (bookEntity == null)
            {
                bookEntity = new Book { Authors = new List<Author>() };
                bookEntity.Name = book.Name;
                bookEntity.ISBN = book.ISBN;
                await _context.Books.AddAsync(bookEntity);
            }

            foreach (var author in book.Authors.ToList())
            {
                var authorEntity = _context.Authors.FirstOrDefault(x => x.Name == author.Name);
                if (authorEntity != null)
                    bookEntity.Authors.Add(authorEntity);
                else
                    bookEntity.Authors.Add(author);
            }

            return await _context.SaveChangesAsync();
        }
    }
}
