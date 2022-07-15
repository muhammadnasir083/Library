using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        public LoginController(IUserInfoService userInfoservice)
        {
            _userInfoService = userInfoservice;
        }

        public IActionResult Index(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return View();

            _userInfoService.SetInfo(loginModel);
            return RedirectToAction("Index", "Library");
        }

        public IActionResult Logout()
        {
            _userInfoService.SetInfo(null);
            return RedirectToAction("Index", "Login");
        }
    }
}
