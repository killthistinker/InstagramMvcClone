using System;
using System.Collections.Generic;

namespace InstagramMVC.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Publication()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }
}