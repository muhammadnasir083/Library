using Library.Controllers;
using Library.Models;
using Library.Services;
using LibraryTest.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace LibraryTest.Controllers
{
    public class LibraryControllerTest : TestBase<LibraryController>
    {
        [Fact]
        public void Index_redirects_to_login()
        {
            LoginModel? loginModel = null;
            MockFor<IUserInfoService>().Setup(x => x.GetInfo())
                .Returns(loginModel);

            var result = SUT?.Index() as RedirectToActionResult;

            result?.ActionName.ShouldBe("Index");
            result?.ControllerName.ShouldBe("Login");
            Logger.VerifyLog(LogLevel.Information, Times.Exactly(1), It.IsAny<string>());
        }

        [Fact]
        public void Index_returns_library_model()
        {
            MockFor<IUserInfoService>().Setup(x => x.GetInfo())
                .Returns(LoginModel);
            MockFor<ILibraryService>().Setup(x => x.GetLibraryModel())
                .Returns(LibraryModel);

            var result = SUT?.Index() as ViewResult;

            var resultModel = result?.Model as LibraryModel;
            resultModel?.Books.ShouldNotBeNull();
            resultModel?.Books.Count.ShouldBe(2);
        }
    }
}
