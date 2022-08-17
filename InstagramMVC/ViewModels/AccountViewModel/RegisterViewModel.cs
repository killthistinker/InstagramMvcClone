using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace InstagramMVC.ViewModels.AccountViewModel
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Данные введены неверно")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Email { get; set; }
        
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Compare((nameof(Password)), ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        public IFormFile File { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string UserInfo { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}