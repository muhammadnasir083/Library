using Library.Models;
using Library.Services;
using Moq;
using Shouldly;

namespace LibraryTest.Services
{
    public class LibraryServiceTest : TestBase<LibraryService>
    {
        [Fact]
        public void GetLibraryModel_returns_library_model_from_cache()
        {
            MockFor<ICachingService>().Setup(x => x.Get<LibraryModel>(It.IsAny<string>()))
                .Returns(LibraryModel)
                .Verifiable();

            var result = SUT?.GetLibraryModel();

            result.ShouldNotBeNull();
            result.Books.Count.ShouldBe(2);
            MockFor<ICachingService>().Verify(x => x.Get<LibraryModel>(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetLibraryModel_returns_library_model_from_xml_file()
        {
            LibraryModel? libraryModel = null;
            MockFor<ICachingService>().Setup(x => x.Get<LibraryModel>(It.IsAny<string>()))
                .Returns(libraryModel);
            MockFor<ICachingService>().Setup(x => x.Set(It.IsAny<string>(), It.IsAny<LibraryModel>))
                .Verifiable();
            MockFor<IXmLDataService>().Setup(x => x.GetLibraryDataFromXml())
                .Returns(LibraryModel);

            var result = SUT?.GetLibraryModel();

            result.ShouldNotBeNull(); 
            result.Books.Count.ShouldBe(2);
            MockFor<ICachingService>().Verify(x => x.Get<LibraryModel>(It.IsAny<string>()), Times.Once);
            MockFor<ICachingService>().Verify(x => x.Set(It.IsAny<string>(), It.IsAny<LibraryModel>()), Times.Once);
        }
    }
}
