using Application.Utils;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/files")]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ResponseContract<string>> Upload(IFormFile file)
    {
        try
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var stream = file.OpenReadStream();
            var url = await fileService.UploadAsync(stream, fileName, file.ContentType);
            return new ResponseContract<string> { Data = url, Ok = true, ErrorCode = null };
        }
        catch (Exception ex)
        {
            return new ResponseContract<string> { Data = null, Ok = false, ErrorCode = ex.Message };
        }
    }
}