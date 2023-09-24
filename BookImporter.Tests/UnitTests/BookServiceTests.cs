using BookImporter.Entities.Exceptions;
using BookImporter.Entities.Models;
using BookImporter.Repositories.Interfaces;
using BookImporter.Repositories.Repositories;
using BookImporter.Services.Interfaces;
using BookImporter.Services.Services;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Tests.UnitTests
{
    [TestClass]
    public class BookServiceTests
    {
        private readonly List<IBookParser> _parsers = new(); 
        private readonly IBookRepository _bookRepository;

        public BookServiceTests()
        {

            var logger = Substitute.For<ILogger<DefaultBookParser>>();

            var inMemorySettings = new Dictionary<string, string>
            {
                {"Formats:DefaultBookFormat:0:definitionType","a" },
                {"Formats:DefaultBookFormat:0:nameColumnStart","1" },
                {"Formats:DefaultBookFormat:0:nameColumnEnd","20" },
                {"Formats:DefaultBookFormat:0:isbnColumnStart","21" },
                {"Formats:DefaultBookFormat:0:isbnColumnEnd","41" },
                {"Formats:DefaultBookFormat:0:authorColumnStart","42" },
                {"Formats:DefaultBookFormat:0:authorColumnEnd","62" },

                {"Formats:DefaultBookFormat:1:definitionType","b" },
                {"Formats:DefaultBookFormat:1:nameColumnStart","1" },
                {"Formats:DefaultBookFormat:1:nameColumnEnd","30" },
                {"Formats:DefaultBookFormat:1:isbnColumnStart","31" },
                {"Formats:DefaultBookFormat:1:isbnColumnEnd","51" },
                {"Formats:DefaultBookFormat:1:authorColumnStart","52" },
                {"Formats:DefaultBookFormat:1:authorColumnEnd","72" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _parsers.Add(new DefaultBookParser(config, logger));

            _bookRepository = Substitute.For<IBookRepository>();
        }

        [TestMethod]
        public async Task BookService_ImportBooks_FromTextFileA_Returns_15_rows()
        {
            // Arrange
            using var reader = new StreamReader("../../../TestData/A.TXT");
            var service = new BookService(_parsers, _bookRepository);

            var batch = new ImportBatch();
            _bookRepository.CreateImportBatchAsync(batch).Returns(new ImportBatch());
            _bookRepository.ImportBookAsync(Arg.Any<Book>()).Returns(3);

            // Act
            var bookCount = await service.ImportBooks(reader);

            // Assert
            Assert.AreEqual(15, bookCount);
        }

        [TestMethod]
        public async Task BookService_ImportBooks_FromTextFileB_Returns_11_rows()
        {
            // Arrange
            using var reader = new StreamReader("../../../TestData/B.TXT");
            var service = new BookService(_parsers, _bookRepository);

            var batch = new ImportBatch();
            _bookRepository.CreateImportBatchAsync(batch).Returns(new ImportBatch());
            _bookRepository.ImportBookAsync(Arg.Any<Book>()).Returns(3);

            // Act
            var bookCount = await service.ImportBooks(reader);

            // Assert
            Assert.AreEqual(11, bookCount);
        }

        [TestMethod]
        public async Task BookService_ImportBooks_Throws_UnsupportedBookFormatException_If_InvalidFormat()
        {
            // Arrange
            using var reader = new StreamReader("../../../TestData/C-Format-In-First-Line.TXT");
            var service = new BookService(_parsers, _bookRepository);
            
            var batch = new ImportBatch();
            _bookRepository.CreateImportBatchAsync(batch).Returns(new ImportBatch());

            // Act
            try
            {
                var books = await service.ImportBooks(reader);
                Assert.Fail();
            }
            // Assert
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(UnsupportedBookFormatException));
            }
        }
    }
}
