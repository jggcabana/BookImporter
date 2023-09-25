using BookImporter.Entities.DTOs;
using BookImporter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Services.Interfaces
{
    public interface IBookParser
    {
        public BookDTO Parse(string line);

        public BookDTO Parse(string line, string formatType);

        public string Key { get; }
    }
}
