using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationService.Services.ImageServices
{
    public class ImageServices : IImageServices
    {
        public async Task<string> UploadAsync(IFormFile image, string rootPath)
        {
            if (image == null || image.Length == 0)
            {
                return string.Empty;
            }

            var directoryPath = Path.Combine(rootPath, "img", "ProductImages");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileExtension = Path.GetExtension(image.FileName).ToLower();
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(directoryPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            if (File.Exists(filePath))
            {
                return Path.Combine("img", "ProductImages", uniqueFileName);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
