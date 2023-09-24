using BookImporter.Entities.Exceptions;
using BookImporter.Entities.Models;
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
        public BookService(IEnumerable<IBookParser> parsers)
        {
            foreach(var parser in parsers)
            {
                _parsers.Add(parser.Key, parser);
            }
        }

        public List<Book> ImportBooks(StreamReader reader)
        {
            var books = new List<Book>();

            var firstLine = reader.ReadLine();
            var parser = CreateParser(firstLine);

            string line = "";
            while((line = reader.ReadLine()) != null)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                var book = parser.Parse(line, firstLine);
                books.Add(book);
            }

            return books;
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
