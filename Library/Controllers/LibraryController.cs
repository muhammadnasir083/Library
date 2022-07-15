using Library.Constants;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger; 
        private readonly IUserInfoService _userInfoService;
        private readonly ILibraryService _libraryService;

        public LibraryController(ILogger<LibraryController> logger
            , IUserInfoService userInfoService
            , ILibraryService libraryService)
        {
            _logger = logger;
            _userInfoService = userInfoService;
            _libraryService = libraryService;
        }

        public IActionResult Index()
        {
            if (_userInfoService.GetInfo() == null)
            {
                _logger.LogInformation("Redirecting to login.");
                return RedirectToAction("Index", "Login");
            }

            var librarModel = _libraryService.GetLibraryModel();
            return View(librarModel);
        }

        public IActionResult Borrow(int id, Actions action)
        {
            _libraryService.SaveContext(id, action);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}