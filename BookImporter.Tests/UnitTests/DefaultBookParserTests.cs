using BookImporter.Services.Services;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Tests.UnitTests
{
    [TestClass]
    public class DefaultBookParserTests
    {
        private readonly IConfiguration _config;
        private readonly DefaultBookParser _parser;
        public DefaultBookParserTests()
        {
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

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var logger = Substitute.For<ILogger<DefaultBookParser>>();
            _parser = new DefaultBookParser(_config, logger);
        }

        [TestMethod]
        public void PerLine_FormatA_TestCase_SpaceDelimitedFieldsOnly()
        {
            // Arrange
            var line = @"Charlie Bone Series 1234567890           Jenny Nimmo";

            // Act
            var book = _parser.Parse(line, "a");

            // Assert
            Assert.AreEqual("Charlie Bone Series", book.Name);
            Assert.AreEqual("1234567890", book.ISBN);
            Assert.AreEqual("Jenny Nimmo", book.Author);
        }

        [TestMethod]
        public void PerLine_FormatA_TestCase_SpaceDelimitedAndTabDelimitedFields()
        {
            // Arrange
            var line = @"Chimera 			4343445454			 John Barth";

            // Act
            var book = _parser.Parse(line, "a");

            // Assert
            Assert.AreEqual("Chimera", book.Name);
            Assert.AreEqual("4343445454", book.ISBN);
            Assert.AreEqual("John Barth",book.Author);
        }

        [TestMethod]
        public void PerLine_FormatB_TestCase_SpaceDelimitedAndTabDelimitedFields()
        {
            // Arrange
            var line = @"A Fine and Private Place 	  4334445345564		   Peter S. Beagle";

            // Act
            var book = _parser.Parse(line, "b");

            // Assert
            Assert.AreEqual("A Fine and Private Place", book.Name);
            Assert.AreEqual("4334445345564", book.ISBN);
            Assert.AreEqual("Peter S. Beagle", book.Author);
        }

        [TestMethod]
        public void PerLine_FormatB_TestCase_SpaceDelimitedAndTabDelimitedFields2()
        {
            // Arrange
            var line = @"The Artefacts of Power 		  9999676565656        Maggie Furey";

            // Act
            var book = _parser.Parse(line, "b");

            // Assert
            Assert.AreEqual("The Artefacts of Power", book.Name);
            Assert.AreEqual("9999676565656", book.ISBN);
            Assert.AreEqual("Maggie Furey", book.Author);
        }

        [TestMethod]
        public void PerLine_FormatA_OutOfBounds_On_NameColumn_Returns_TruncatedString()
        {
            var line = @"Charlie Bone Series: The Unabridged Tales 1234567890           Jenny Nimmo";

            var book = _parser.Parse(line, "a");

            Assert.AreEqual("Charlie Bone Series", book.Name);
            Assert.AreNotEqual("1234567890", book.ISBN);
            Assert.AreNotEqual("Jenny Nimmo", book.Author);
        }
    }
}
