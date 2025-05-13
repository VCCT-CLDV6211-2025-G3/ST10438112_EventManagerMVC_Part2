namespace EventManagerMVC.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileBlobAsync(Stream fileStream, string fileName, string containerName);
    }
}