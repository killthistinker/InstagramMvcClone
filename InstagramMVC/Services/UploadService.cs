using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InstagramMVC.Services
{
    public class UploadService
    {
        public async Task UploadAsync(string dirPath, string fileName, IFormFile file)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            var absolutePath = Path.Combine(dirPath, fileName);
            if (File.Exists(absolutePath))
                throw new FileNotFoundException();
            await using var stream = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}