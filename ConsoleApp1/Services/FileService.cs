using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Services
{
    public class FileService(IOptions<MinioOptions> options) : IFileService
    {
        private readonly MinioOptions _options = options.Value;

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            using var client = new MinioClient()
                .WithSSL(false)
                .WithEndpoint("localhost", 9000)
                .WithCredentials(_options.AccessKey, _options.SecretKey)
                .Build();

            bool exists = await client.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_options.BucketName));

            if (!exists)
                await client.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(_options.BucketName));

            await client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType));

            return $"http://127.0.0.1:9000/{_options.BucketName}/{fileName}";
        }
    }
}
