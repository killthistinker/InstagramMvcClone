using InstagramMVC.Models;
using InstagramMVC.ViewModels.AccountViewModel;

namespace InstagramMVC.Helpers
{
    public static class UserRegistrationMapper
    {
        public static User MapToUser(this RegisterViewModel self)
        {
            User user = new User
            {
                Email = self.Email,
                Name = self.Name,
                UserName = self.UserName,
                ImagePath = self.ImagePath,
                Gender = self.Gender,
                UserInfo = self.UserInfo,
                PhoneNumber = self.PhoneNumber
            };
            return user;
        }
    }
}