using System.Threading.Tasks;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using InstagramMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstagramMVC.Controllers
{
    public class PublicationController : Controller
    {
        private readonly ILikeService _service;
        private readonly IPublicationService _publicationService;
        private readonly UserManager<User> _userManager;
        private readonly IProfileService _profileService;
        

        public PublicationController(ILikeService service, UserManager<User> userManager, IPublicationService publicationService, IProfileService profileService)
        {
            _service = service;
            _userManager = userManager;
            _publicationService = publicationService;
            _profileService = profileService;
        }
        
        
        [HttpGet]
        public IActionResult LikePublication(int publicationId)
        {
            int.TryParse(_userManager.GetUserId(User), out int authenticatedId);
            _service.Like(publicationId, authenticatedId);
            Publication publication = _service.GetPublication(publicationId);
            if (publication is null)
                return RedirectToAction("Error", "Errors", new { statusCode = 404 });
            ViewBag.UserId = authenticatedId;
            return PartialView("ViewsPatrial/LikesPatrialView", publication);
        }

        [HttpGet]
        public IActionResult LikeProfilePublication(int publicationId)
        {
            int.TryParse(_userManager.GetUserId(User), out int authenticatedId);
            _service.Like(publicationId, authenticatedId);
            Publication publication = _service.GetPublication(publicationId);
            if (publication is null)
                return RedirectToAction("Error", "Errors", new { statusCode = 404 });
            return PartialView("ViewsPatrial/ProfileLikePatrialView", publication);
        }

        [HttpGet]
        public IActionResult AddComment(int publicationId)
        {
            int.TryParse(_userManager.GetUserId(User),out int userId);
            Publication publication = _publicationService.GetPublication (publicationId);
            AddCommentViewModel model = new AddCommentViewModel
            {
                Publication = publication
            };
            ViewBag.UserId = userId;
            return View(model);
        }

        [HttpPost]
        [ActionName("Comment")]
        public IActionResult AddComment(AddCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(_userManager.GetUserId(User), out int userId);
                _publicationService.AddComment(userId,model);
                ViewBag.UserId = userId;
                return RedirectToAction("AddComment", "Publication", new {model.Comment.PublicationId});
            }

            return RedirectToAction("Error", "Errors", new { statusCode = 404 });
        }

        [HttpGet]
        public IActionResult Delete(int publicationId)
        {
            _publicationService.DeletePublication(publicationId);
            return PartialView("ViewsPatrial/PublicationsPartialViewModel");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int publicationId, string description)
        {
            _publicationService.EditPublication(publicationId,description);
            int userId = int.Parse(_userManager.GetUserId(User));
            User user = await _profileService.GetUser(userId);
            return PartialView("ViewsPatrial/PublicationsPartialViewModel", user.Publications);
        }
        
    }
}