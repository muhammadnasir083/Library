using Library.Constants;
using Library.Models;

namespace Library.Services
{
    public interface ILibraryService
    {
        LibraryModel GetLibraryModel();
        bool SaveContext(int Id, Actions action);
    }
    public class LibraryService : ILibraryService
    {
        private readonly ICachingService _cachingService;
        private readonly ILogger<LibraryService> _logger;
        private readonly IUserInfoService _userInfoService;
        private readonly IXmLDataService _xmlDataService;

        public LibraryService(ICachingService cachingService
            , ILogger<LibraryService> logger
            , IUserInfoService userInfoService
            , IXmLDataService xmlDataService)
        {
            _cachingService = cachingService;
            _logger = logger;
            _userInfoService = userInfoService;
            _xmlDataService = xmlDataService;
        }

        public LibraryModel GetLibraryModel()
        {
            var libraryModel = _cachingService.Get<LibraryModel>(LibraryConstants.CacheKeys.LibraryCacheKey);
            if (libraryModel == null)
            {
                libraryModel = _xmlDataService.GetLibraryDataFromXml();   
                _cachingService.Set(LibraryConstants.CacheKeys.LibraryCacheKey, libraryModel);
            }

            return libraryModel; 
        }

        private BookModel GetBookById(int Id) 
        {
            var librarModel = GetLibraryModel(); 
            return librarModel.Books.FirstOrDefault(x => x.Id == Id);
        }

        public bool SaveContext(int Id, Actions action)
        {
            if (action != Actions.Borrow
                && action != Actions.Return)
            {
                _logger.LogWarning("Not a valid action.");
                return false;
            }

            var context = GetBookById(Id);
            if (context.Id == 0)
            {
                _logger.LogWarning("Book not found.");
                return false;
            }

            context.IsBorrowed = action == Actions.Borrow;
            context.BorrowedBy = context.IsBorrowed
                ? _userInfoService.GetInfo().UserId
                : string.Empty;

            return true;
        }
    }
}
