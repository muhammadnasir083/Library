using Library.Constants;
using Library.Models;

namespace Library.Services
{
    public interface IUserInfoService
    {
        LoginModel GetInfo();
        void SetInfo(LoginModel loginModel);
    }
    public class UserInfoService : IUserInfoService
    {
        private readonly ICachingService _cachingService;
        public UserInfoService(ICachingService cachingService)
        {
            _cachingService = cachingService; 
        }

        public LoginModel GetInfo()
            => _cachingService.Get<LoginModel>(LibraryConstants.CacheKeys.LoginDataCacheKey);

        public void SetInfo(LoginModel loginModel)
            => _cachingService.Set(LibraryConstants.CacheKeys.LoginDataCacheKey, loginModel);
    }
}
