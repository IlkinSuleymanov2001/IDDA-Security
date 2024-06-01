namespace Goverment.AuthApi.Business.Utlilities.Files
{
    public interface IFileService
    {
        string Upload(IFormFile file, string root);
        string Update(IFormFile file, string filePath, string root);
        void Delete(string filePath);
    }
}
