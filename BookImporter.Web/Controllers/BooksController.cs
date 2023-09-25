using BookImporter.Entities.DTOs;
using BookImporter.Entities.Models;
using BookImporter.Services.Interfaces;
using BookImporter.Web.ViewModels.Request;
using BookImporter.Web.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookImporter.Web.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportBooks([FromForm]ImportBooksRequest request)
        {
            using var reader = new StreamReader(request.File.OpenReadStream());
            var importCount = await _bookService.ImportBooksAsync(reader);
            return Ok(new BaseResponse
            {
                Success = true,
                Message = $"Imported {importCount} new book{(importCount == 1 ? "" : "s")}."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = (await _bookService.GetBooksAsync()) ?? new List<BookDTO>();
            return Ok(new BaseResponse
            {
                Success = true,
                Data = result
            });
        }
    }
}
