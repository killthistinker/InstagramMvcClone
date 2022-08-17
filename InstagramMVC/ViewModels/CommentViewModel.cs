using System.ComponentModel.DataAnnotations;
using InstagramMVC.Models;

namespace InstagramMVC.ViewModels
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string UserComment { get; set; }

        public int PublicationId { get; set; }
    }
}