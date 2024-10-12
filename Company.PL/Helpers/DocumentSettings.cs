namespace Company.PL.Helpers
{
    public class DocumentSettings
    {
        //Upload
        public static string UploadFile(IFormFile file,string folderName)
        {
            //Get Location Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files",folderName);
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            string filePath = Path.Combine(folderPath,fileName);
            using var fileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;    
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName,fileName);
            if (File.Exists(filePath)) 
                File.Delete(filePath);

        }


    }
}
