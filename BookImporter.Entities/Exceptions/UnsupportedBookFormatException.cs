using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.Exceptions
{
    public class UnsupportedBookFormatException : Exception
    {
        public UnsupportedBookFormatException()
            :base() { }

        public UnsupportedBookFormatException(string message)
            : base(message) { }
    }
}
