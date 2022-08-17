using InstagramMVC.Models;

namespace InstagramMVC.Services.Abstractions
{
    public interface ILikeService
    {
        public void Like(int userId, int authenticatedId);
        public Publication GetPublication(int publicationId);
    }
}