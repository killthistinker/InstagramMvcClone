using Microsoft.AspNetCore.Mvc;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace InstagramMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPublicationService _publicationService;
        public HomeController(UserManager<User> userManager,IPublicationService publicationService)
        {
            _userManager = userManager;
            _publicationService = publicationService;
        }

        public IActionResult Index()
        {
            int.TryParse(_userManager.GetUserId(User), out int userId);
            var user = _publicationService.GetFollowersPublications(userId);
            ViewBag.UserId = userId;
            return View(user);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {});
        }
    }
}