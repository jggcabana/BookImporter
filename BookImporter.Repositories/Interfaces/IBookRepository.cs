using BookImporter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<int> ImportBookAsync(Book book);

        public Task<ImportBatch> CreateImportBatchAsync(ImportBatch batch);

        public Task CreateImportLogAsync(ImportLog log);
    }
}
