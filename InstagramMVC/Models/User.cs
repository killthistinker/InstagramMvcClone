using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace InstagramMVC.Models
{
    public class User : IdentityUser<int>
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string UserInfo { get; set; }
        public string Gender { get; set; }
        
        public List<Publication> Publications { get; set; }

        public List<User> Subscribers { get; set; }
        public List<User> Followers { get; set; }
        
        public User()
        {
            Publications = new List<Publication>();
            Followers = new List<User>();
            Subscribers = new List<User>();
        }
    }
}