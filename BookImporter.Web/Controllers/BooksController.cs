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
            var importCount = await _bookService.ImportBooks(reader);
            return Ok(new BaseResponse
            {
                Success = true,
                Message = $"Imported {importCount} new book{(importCount == 1 ? "" : "s")}."
            });
        }
    }
}
