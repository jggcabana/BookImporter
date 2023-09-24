using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.Models
{
    public class DefaultBookFormatDefinition
    {
        public string DefinitionType { get; set; }
        public int NameColumnStart { get; set; }

        public int NameColumnEnd { get; set; }

        public int ISBNColumnStart { get; set; }

        public int ISBNColumnEnd { get; set; }

        public int AuthorColumnStart { get; set; }

        public int AuthorColumnEnd { get; set; }
    }
}           
                  