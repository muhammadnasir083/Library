using Library.Models;
using Library.Settings;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace Library.Services
{
    public interface IXmLDataService
    {
        LibraryModel GetLibraryDataFromXml();
    }
    public class XMLDataService : IXmLDataService
    {
        private readonly IOptionsMonitor<LibrarySettings> _librarySettings;
        private readonly ILogger<XMLDataService> _logger;
        public XMLDataService(IOptionsMonitor<LibrarySettings> librarySettings
            , ILogger<XMLDataService> logger)
        {
            _librarySettings = librarySettings;
            _logger = logger;
        }

        public LibraryModel GetLibraryDataFromXml()
        {
            int Id = 1;
            var libraryModel = new LibraryModel();
            try
            {
                var xmlData = XElement.Load(_librarySettings.CurrentValue.XmlFilePath);
                libraryModel.Books = (from node in xmlData.Elements(_librarySettings.CurrentValue.XmlNodeToRead)
                                      select new BookModel
                                      {
                                          Id = Id++,
                                          BookId = node.Attribute("id").Value,
                                          Author = node.Element("author").Value,
                                          Title = node.Element("title").Value,
                                          Genre = node.Element("genre").Value,
                                          Price = double.Parse(node.Element("price").Value),
                                          PublishDate = DateTime.Parse(node.Element("publish_date").Value),
                                          Description = node.Element("description").Value
                                      }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message: {ex.Message} StackTrace: {ex.StackTrace}");
            }

            return libraryModel;
        }
    }
}
