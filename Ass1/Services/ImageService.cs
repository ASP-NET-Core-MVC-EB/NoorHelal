
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Data;
using RestaurantApplication.ViewModel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace RestaurantApplication.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService( IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void DeleteImage(string path)
        {

            if (string.IsNullOrEmpty(path)) return;
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fullPath = Path.Combine(wwwRootPath, path);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

        }

        public async Task<string> UploadAndResizeImageAsync(IFormFile file, string folderName, int width = 300, int height = 300)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(wwwRootPath, @"images",folderName);

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            string FullPath = Path.Combine(path, fileName);

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Crop
                }));
               await image.SaveAsync(FullPath);
            }
                return @"images\" + folderName + @"\" + fileName;
        }
    }
}
