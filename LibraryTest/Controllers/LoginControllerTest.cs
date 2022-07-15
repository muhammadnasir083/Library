using Library.Controllers;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace LibraryTest.Controllers
{
    public class LoginControllerTest : TestBase<LoginController>
    {
        [Fact]
        public void Index_redirects_to_library()
        {
            MockFor<IUserInfoService>().Setup(x => x.GetInfo())
                .Returns(LoginModel);

            var result = SUT?.Index(new LoginModel()) as RedirectToActionResult;

            result?.ActionName.ShouldBe("Index");
            result?.ControllerName.ShouldBe("Library");
        }

        [Fact]
        public void Index_returns_invalid_model_error_when_userid_is_empty()
        {
            SUT?.ModelState.AddModelError("UserId", "UserId error message.");
            SUT?.ModelState.AddModelError("Password", "Password error message.");

            var result = SUT?.Index(new LoginModel()) as ViewResult;

            var resultModel = result?.Model as LoginModel;
            resultModel?.UserId.ShouldBeNull();
            resultModel?.Password.ShouldBeNull();
        }
    }
}
