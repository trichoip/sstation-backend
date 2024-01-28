using Microsoft.AspNetCore.Http;

namespace ShipperStation.Application.Contracts.Services;

public interface IStorageService
{
    Task<byte[]> DownloadFileAsync(string fileName, CancellationToken cancellationToken = default);

    Task<string> GetContentTypeAsync(string fileName, CancellationToken cancellationToken = default);

    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);

    Task<bool> DeleteFileAsync(string fileName, string versionId = "", CancellationToken cancellationToken = default);

    Task<bool> IsFileExistsAsync(string fileName, string versionId = "", CancellationToken cancellationToken = default);

    Task<string> GetPresignedUrlAsync(string fileName, CancellationToken cancellationToken = default);

    string GetObjectUrl(string fileName);
}