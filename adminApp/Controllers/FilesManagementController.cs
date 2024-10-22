using adminApp.Models;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Mvc;

namespace adminApp.Controllers;
public class FilesManagementController : Controller
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName = "moveicarus-asia-bucket";

    public FilesManagementController(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<IActionResult> Index()
    {
        // Get a list of files from S3 to display on the page
        var files = await ListFiles();
        return View(new S3ViewModel { Files = files });
    }

    // Upload action
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["ErrorMessage"] = $"No file chosen";
            return RedirectToAction("Index");
        }

        var fileTransferUtility = new TransferUtility(_s3Client);

        using (var stream = file.OpenReadStream())
        {
            // Upload the file
            await fileTransferUtility.UploadAsync(stream, _bucketName, file.FileName);
        }

        TempData["SuccessMessage"] = $"File {file.FileName} Uploaded."; // Set success message

        return RedirectToAction("Index");
    }

    // Download action
    [HttpPost]
    public async Task<IActionResult> Download(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return Content("File name is required");
        }

        try
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, fileName);

            using (var responseStream = response.ResponseStream)
            {
                var memoryStream = new MemoryStream();
                responseStream.CopyTo(memoryStream);

                TempData["SuccessMessage"] = $"File {fileName} Downloaded."; // Set success message
                return File(memoryStream.ToArray(), response.Headers["Content-Type"], fileName);
            }
        }
        catch (AmazonS3Exception ex)
        {
            return Content($"Error encountered: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ViewFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            TempData["ErrorMessage"] = "File name cannot be empty.";
            return RedirectToAction("Index");
        }

        // Generate a pre-signed URL for the file
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            Expires = DateTime.UtcNow.AddHours(1) // URL expires in 1 hour
        };

        string url = await _s3Client.GetPreSignedURLAsync(request);

        // Redirect to the file URL
        return Redirect(url);
    }

    // List all files in the S3 bucket
    private async Task<List<string>> ListFiles()
    {
        var files = new List<string>();
        var listResponse = await _s3Client.ListObjectsV2Async(new Amazon.S3.Model.ListObjectsV2Request
        {
            BucketName = _bucketName
        });

        foreach (var entry in listResponse.S3Objects)
        {
            files.Add(entry.Key);
        }

        return files;
    }
}