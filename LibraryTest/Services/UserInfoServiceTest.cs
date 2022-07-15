using Library.Models;
using Library.Services;
using Moq;
using Shouldly;

namespace LibraryTest.Services
{
    public class UserInfoServiceTest : TestBase<UserInfoService>
    {
        [Fact]
        public void GetInfo_returns_login_model()
        {
            MockFor<ICachingService>().Setup(x => x.Get<LoginModel>(It.IsAny<string>()))
                .Returns(LoginModel);

            var result = SUT?.GetInfo();

            result?.ShouldNotBeNull();
            result?.UserId.ShouldBe("guest");
        }

        [Fact]
        public void GetInfo_returns_null()
        {
            LoginModel? loginModel = null;
            MockFor<ICachingService>().Setup(x => x.Get<LoginModel>(It.IsAny<string>()))
                .Returns(loginModel);

            var result = SUT?.GetInfo();

            result?.ShouldBeNull();
        }

        [Fact]
        public void SetInfo_sets_login_model_to_cache()
        {
            MockFor<ICachingService>().Setup(x => x.Set(It.IsAny<string>(), It.IsAny<LoginModel>()))
                .Verifiable();

            SUT?.SetInfo(LoginModel);

            MockFor<ICachingService>().Verify(
                x => x.Set(It.IsAny<string>(), It.IsAny<LoginModel>()), Times.Once);
        }
    }
}
