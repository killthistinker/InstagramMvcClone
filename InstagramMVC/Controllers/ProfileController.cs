using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramMVC.Helpers;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using InstagramMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstagramMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<User> _userManager;
        private readonly IPublicationRegisterService _publicationRegisterService;
        private readonly ISubscribeService _subscribeService;

        public ProfileController(IProfileService profileService, 
            UserManager<User> userManager,
            IPublicationRegisterService publicationRegisterService, 
            ISubscribeService subscribeService)
        {
            _profileService = profileService;
            _userManager = userManager;
            _publicationRegisterService = publicationRegisterService;
            _subscribeService = subscribeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int.TryParse(_userManager.GetUserId(User), out var userId);
            if (userId == 0)
            {
                return RedirectToAction("Register", "Account");
            }
            var user = await _profileService.GetUser(userId);
            if (user is null)
                return RedirectToAction("Error", "Errors", new { statusCodde = 404 });
            PublicationViewModel model = new PublicationViewModel
            {
                User = user
            };
            return View(model);
        }

        [HttpGet]
        [ActionName("UserIndex")]
        public async Task<IActionResult> Index(int userId)
        {
            int.TryParse(_userManager.GetUserId(User), out int currentUser);
            if (currentUser == userId) return RedirectToAction("Index", "Profile");
            
            if (userId == 0) return RedirectToAction("Error", "Errors", new { statusCodde = 404 });
            
            var user = await _profileService.GetUser(userId);
            if (user is null)
                return RedirectToAction("Error", "Errors", new { statusCodde = 404 });
            return View(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> Publication(PublicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity != null)
                {
                    string userName = User.Identity.Name;
                    await _publicationRegisterService.CreateAsync(model, userName);
                    Publication publication = model.MapToUserPublication();
                    int.TryParse(_userManager.GetUserId(User), out int userId);
                    await _profileService.AddPublication(publication, userId);
                    return RedirectToAction("Index");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchUsers(string value)
        {
            if (value is null)
                return RedirectToAction("Error", "Errors", new { statusCode = 404 });
            
            List<User> users = await _profileService.SearchUsers(value);
            int.TryParse(_userManager.GetUserId(User), out int userId);
            User user = await _profileService.GetUser(userId);
            if(user is null)
                return RedirectToAction("Error", "Errors", new { statusCode = 404 });

            SearchUsersViewModel model = new SearchUsersViewModel
            {
                User = user,
                Users = users
            };
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Subscribe(int userId)
        {
            int.TryParse(_userManager.GetUserId(User), out int subscriberId);
             _subscribeService.Subscribe(subscriberId,userId);
             User user = await _profileService.GetUser(userId);
             if (user == null) return RedirectToAction("Error", "Errors", new { statusCode = 404 });

             return PartialView("ViewsPatrial/SubscribePatrialView", user);
        }
    }
}