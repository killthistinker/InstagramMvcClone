using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InstagramMVC.Models;
using Microsoft.AspNetCore.Http;

namespace InstagramMVC.ViewModels
{
    public class PublicationViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public IFormFile File { get; set; }
        public string ImagePath { get; set; }

        public User User { get; set; }
    }
}