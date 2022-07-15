using Library.Services;
using Library.Settings;
using LibraryTest.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;

namespace LibraryTest.Services
{
    public class XmlDataServiceTest : TestBase<XMLDataService>
    {
        [Fact]
        public void GetLibraryDataFromXml_returns_library_model()
        {
            MockFor<IOptionsMonitor<LibrarySettings>>().Setup(x => x.CurrentValue)
                .Returns(new LibrarySettings { 
                    XmlFilePath = "../../../Data/Library.xml",
                    XmlNodeToRead = "book"
                });

            var result = SUT?.GetLibraryDataFromXml();

            result?.Books.Count.ShouldBe(12);
        }

        [Fact]
        public void GetLibraryDataFromXml_logs_error_upon_exception()
        {
            MockFor<IOptionsMonitor<LibrarySettings>>().Setup(x => x.CurrentValue)
                .Returns(new LibrarySettings
                {
                    XmlFilePath = "../../../Data/Library.xml",
                    XmlNodeToRead = "library/book"
                });

            var result = SUT?.GetLibraryDataFromXml();

            result?.Books.Count.ShouldBe(0);
            Logger.VerifyLog(LogLevel.Error
               , Times.Exactly(1)
               , It.IsAny<string>());
        }
    }
}
