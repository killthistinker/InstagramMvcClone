using InstagramMVC.Services.Abstractions;

namespace InstagramMVC.Services
{
    public class DefalutUserImageAvatar : IDefaultUserImageAvatar
    {
        private readonly string _path;

        public DefalutUserImageAvatar(string path)
        {
            _path = path;
        }

        public string GetPathToDefaultImage() => _path;

    }
}