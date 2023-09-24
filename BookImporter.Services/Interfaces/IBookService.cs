using BookImporter.Entities.DTOs;
using BookImporter.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Services.Interfaces
{
    public interface IBookService
    {
        public Task<int> ImportBooks(StreamReader reader);
    }
}
