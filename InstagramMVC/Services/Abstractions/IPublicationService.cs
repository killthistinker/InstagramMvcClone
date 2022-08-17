using InstagramMVC.Models;
using InstagramMVC.ViewModels;

namespace InstagramMVC.Services.Abstractions
{
    public interface IPublicationService
    {
        public Publication GetPublication(int id);
        public void AddComment(int userId, AddCommentViewModel model);
        public User GetFollowersPublications(int userId);

        public void DeletePublication(int publicationId);
        public void EditPublication(int publicationId, string description);
    }
}