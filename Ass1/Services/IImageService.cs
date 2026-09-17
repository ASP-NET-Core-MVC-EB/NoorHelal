namespace RestaurantApplication.Services
{
    public interface IImageService
    {
        public Task<string> UploadAndResizeImageAsync(IFormFile file, string folderName, int width = 300, int height = 300);
        public void DeleteImage(string path);
    }
}
