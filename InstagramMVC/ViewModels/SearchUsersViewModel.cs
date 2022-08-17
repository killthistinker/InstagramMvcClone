using System.Collections.Generic;
using InstagramMVC.Models;

namespace InstagramMVC.ViewModels
{
    public class SearchUsersViewModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; }

        public SearchUsersViewModel()
        {
            Users = new List<User>();
        }
    }
}