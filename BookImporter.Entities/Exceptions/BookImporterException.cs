using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.Exceptions
{
    public class BookImporterException : Exception
    {
        public BookImporterException()
            : base() { }

        public BookImporterException(string message)
            : base(message) { }
    }
}
