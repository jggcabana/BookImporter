using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; }

        public string ISBN { get; set; }

        public string Author { get; set; }
    }
}
