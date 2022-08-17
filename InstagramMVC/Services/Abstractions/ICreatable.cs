using System.Threading.Tasks;
using InstagramMVC.ViewModels.AccountViewModel;

namespace InstagramMVC.Services.Abstractions
{
    public interface ICreatable<in T> where T : class
    {
        public Task CreateAsync(T entity, string userName);
       
    }
}