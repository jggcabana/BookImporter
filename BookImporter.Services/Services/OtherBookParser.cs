using BookImporter.Entities.Models;
using BookImporter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Services.Services
{
    // this is just to show that we can implement another parser
    public class OtherBookParser : IBookParser
    {
        public string Key { get; } = "other";
        public Book Parse(string line)
        {
            throw new NotImplementedException();
        }

        public Book Parse(string line, string formatType)
        {
            throw new NotImplementedException();
        }
    }
}
