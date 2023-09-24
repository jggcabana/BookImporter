using BookImporter.Entities.Exceptions;
using BookImporter.Entities.Models;
using BookImporter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Services.Services
{
    public class DefaultBookParser : IBookParser
    {
        private readonly List<DefaultBookFormatDefinition> formats;
        private readonly ILogger<DefaultBookParser> _logger;

        public DefaultBookParser(IConfiguration config, ILogger<DefaultBookParser> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            formats = config.GetSection("Formats:DefaultBookFormat")?.Get<List<DefaultBookFormatDefinition>>() ??
                throw new ArgumentNullException("App setting section for formats is null.");
        }

        public string Key { get; } = "default";
        public Book Parse(string line)
        {
            string defaultFormatType = "a"; // TODO: get rid of magic string 
            return Parse(line, defaultFormatType);
        }

        public Book Parse(string line, string formatType)
        {
            try
            {
                var format = formats.FirstOrDefault(x => x.DefinitionType == formatType.ToLower());
                if (format == null)
                    throw new UnsupportedBookFormatException();

                // we know that more than 4 spaces is some funny person trying to delimit the fields
                // convert 4 spaces into tabs so we can find our offset
                line = line.Replace("    ", "\t");

                int offset = 0;
                int startIndex = 0;
                int endIndex = format.NameColumnEnd - 1; // all columnStart/columnEnd are base-1, so we need to subtract.
                int firstTabIndex = line.IndexOf('\t', startIndex);

                if (firstTabIndex > -1)
                    endIndex = endIndex < firstTabIndex ? endIndex : firstTabIndex;
                
                string name = line[startIndex..endIndex].Trim();

                // from the endIndex, get the isbn, until the boundary, or if we encounter a tab. trim it.
                // account for the leading tabs by offsetting the index
                offset = line.Substring(endIndex).TakeWhile(c => char.IsWhiteSpace(c)).Count();
                startIndex = endIndex + offset;
                endIndex = format.ISBNColumnEnd - 1;
                firstTabIndex = line.IndexOf('\t', startIndex);

                if (firstTabIndex > -1)
                    endIndex = endIndex < firstTabIndex ? endIndex : firstTabIndex;

                string isbn = line[startIndex..endIndex].Trim();

                offset = line.Substring(endIndex).TakeWhile(c => char.IsWhiteSpace(c)).Count();
                startIndex = endIndex + offset;
                endIndex = format.AuthorColumnEnd - 1;
                firstTabIndex = line.IndexOf('\t', startIndex);

                if (firstTabIndex > -1)
                    endIndex = endIndex < firstTabIndex ? endIndex : firstTabIndex;

                // check for end of line
                endIndex = endIndex > line.Length ? line.Length : endIndex;

                string author = line[startIndex..endIndex].Trim();

                return new Book
                {
                    Name = name,
                    ISBN = isbn,
                    Author = author
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}




