using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.Models
{
    public class ImportBatch : Entity
    {
        public string BookImportFormat { get; set; }
        public virtual ICollection<ImportLog> ImportLogs { get; set; } = new List<ImportLog>();
    }
}
