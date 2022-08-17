using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramMVC.ViewModels;
using InstagramMVC.Models;

namespace InstagramMVC.Services.Abstractions
{
    public interface IProfileService
    {
        public Task<User> GetUser(int userId);
        public Task AddPublication(Publication publication, int userId);
        public  Task<List<User>> SearchUsers(string value);
    }
}