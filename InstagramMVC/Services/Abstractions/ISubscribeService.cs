
namespace InstagramMVC.Services.Abstractions
{
    public interface ISubscribeService
    {
        public void Subscribe(int subscriberId, int userId);
    }
}