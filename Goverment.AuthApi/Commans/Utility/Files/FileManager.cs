namespace Goverment.AuthApi.Commans.Utility.Files
{
    public class FileManager : IFileService
    {
        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string Update(IFormFile file, string filePath, string root)
        {

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                string extension = Path.GetExtension(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string fullFileName = guid + extension;

                using (FileStream fileStream = File.Create(root + fullFileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return fullFileName;
                }
            }
            return null;



        }
    }
}
