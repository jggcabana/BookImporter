using BookImporter.Entities.DTOs;
using BookImporter.Entities.Enums;
using BookImporter.Entities.Exceptions;
using BookImporter.Entities.Models;
using BookImporter.Repositories.Interfaces;
using BookImporter.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Services.Services
{
    public class BookService : IBookService
    {
        private readonly Dictionary<string, IBookParser> _parsers = new Dictionary<string, IBookParser>();

        private readonly IBookRepository _bookRepository;
        public BookService(IEnumerable<IBookParser> parsers, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));

            foreach(var parser in parsers)
            {
                _parsers.Add(parser.Key, parser);
            }
        }

        public async Task<int> ImportBooks(StreamReader reader)
        {
            // create import batch
            var firstLine = reader.ReadLine();
            var batch = await _bookRepository.CreateImportBatchAsync(new ImportBatch { BookImportFormat = firstLine });
            var parser = CreateParser(firstLine);

            string line;
            int row = 1;
            while((line = reader.ReadLine()) != null)
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(line))
                        continue;

                    var book = parser.Parse(line, firstLine);

                    var result = await _bookRepository.ImportBookAsync(new Book
                    {
                        Name = book.Name,
                        ISBN = book.ISBN,
                        Authors = new List<Author> { new Author { Name = book.Author } }
                    });

                    // non-zero means something was added
                    row = result > 1 ? row+1 : row;
                }
                catch (Exception e)
                {
                    // log the error, then continue
                    await _bookRepository.CreateImportLogAsync(new ImportLog
                    {
                        Row = row,
                        Status = ImportLogStatus.ERROR,
                        Message = e.Message,
                        ImportBatch = batch
                    });
                }
            }

            // omit the first row.
            return row-1;
        }

        private IBookParser CreateParser(string firstLine)
        {
            switch (firstLine.ToLower())
            {
                case "a":
                case "b":
                    return _parsers["default"];
                default:
                    throw new UnsupportedBookFormatException("Unsupported Book Format.");
            }
        }
    }
}
