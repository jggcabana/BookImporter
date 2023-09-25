using BookImporter.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Entities.Models
{
    public class ImportLog : Entity
    {
        public int Row { get; set;  }

        public ImportLogStatus Status { get; set; }

        public string Message { get; set; } = String.Empty;

        public ImportBatch ImportBatch { get; set; }
    }
}
