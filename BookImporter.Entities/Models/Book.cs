﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookImporter.Entities.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }

        public string ISBN { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
