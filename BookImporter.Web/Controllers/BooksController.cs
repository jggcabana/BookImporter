using BookImporter.Services.Interfaces;
using BookImporter.Web.ViewModels.Request;
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
            return Ok(_bookService.ImportBooks(reader));
        }
    }
}
