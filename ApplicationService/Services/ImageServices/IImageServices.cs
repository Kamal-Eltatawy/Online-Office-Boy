using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationService.Services.ImageServices
{
    public interface IImageServices
    {
         Task<string> UploadAsync(IFormFile image, string rootPath);

    }
}
