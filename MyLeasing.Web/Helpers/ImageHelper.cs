namespace MyLeasing.Web.Helpers;

public class ImageHelper : IImageHelper
{
    public async Task<string> UploadImageAsync(
        IFormFile? imageFile, string folder)
    {
        var guid = Guid.NewGuid().ToString();
        var fileName = guid + ".jpg";

        // Cria o diretório se não existir
        var folderPath = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot", "images", folder);
        Directory.CreateDirectory(folderPath);
        Path.Exists(folderPath);

        var filePath =
            Directory.GetCurrentDirectory() +
            $"\\wwwroot\\images\\{folder}\\{fileName}";

        // Cria o diretório se não existir
        Directory.CreateDirectory(fileName);
        Path.Exists(folderPath);

        await using var stream =
            new FileStream(
                filePath, FileMode.Create, FileAccess.ReadWrite);

        if (imageFile != null) await imageFile.CopyToAsync(stream);

        return $"~/images/{folder}/{fileName}";
    }
}