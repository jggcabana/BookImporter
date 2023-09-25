using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.Models
{
    public class Author : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
