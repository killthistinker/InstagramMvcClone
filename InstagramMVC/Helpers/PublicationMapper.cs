using System;
using InstagramMVC.Models;
using InstagramMVC.ViewModels;

namespace InstagramMVC.Helpers
{
    public static class PublicationMapper
    {
        public static Publication MapToUserPublication(this PublicationViewModel self)
        {
            Publication publication = new Publication
            {
                Description = self.Description,
                ImagePath = self.ImagePath,
                DateCreated = DateTime.Now
            };
            return publication;
        }
    }
}