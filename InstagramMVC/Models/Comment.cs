
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InstagramMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserComment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}