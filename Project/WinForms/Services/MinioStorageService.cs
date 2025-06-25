namespace WinForms.Services
{
    using System.IO.Compression;
    using System.Reactive.Linq;
    using Minio;
    using Minio.ApiEndpoints;
    using Minio.DataModel.Args;
    using MinioConfig = Configurations.MinioConfig;

    
    public class MinioStorageService
    {
        private readonly IMinioClient   _client;
        private readonly string         _bucketName;

        public MinioStorageService(MinioConfig minioConfig)
        {
            _client = new MinioClient()
                .WithEndpoint(minioConfig.Endpoint, minioConfig.Port)
                .WithCredentials(minioConfig.AccessKey, minioConfig.SecretKey)
                .WithSSL(minioConfig.UseSsl)
                .Build();

            _bucketName = minioConfig.BucketName;
            InitializeBucket().Wait();
        }

        private async Task InitializeBucket()
        {
            var exists = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
            if (!exists)
            {
                await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                Console.WriteLine($"Created bucket: {_bucketName}");
            }
        }

        public async Task UploadFileAsync(string filePath)
        {
            var objectName = Path.GetFileName(filePath);
            await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithFileName(filePath)
                .WithObjectSize(new FileInfo(filePath).Length)
                .WithContentType("application/octet-stream"));
        }

        [Obsolete]
        public async Task ClearBucketAsync()
        {
            var fileUrls = new List<string>();
            await _client.ListObjectsAsync(new ListObjectsArgs()
                    .WithBucket(_bucketName)
                    .WithRecursive(true))
                .ForEachAsync(item => fileUrls.Add(item.Key));

            foreach (var fileUrl in fileUrls)
            {
                await _client.RemoveObjectAsync(new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileUrl));
            }
        }
        
        [Obsolete]
        public async Task<List<string>> ListClinicalFilesInMinIo()
        {
            var fileNames = new List<string>();
            await _client
                .ListObjectsAsync(new ListObjectsArgs()
                    .WithBucket(_bucketName)
                    .WithRecursive(true))
                .ForEachAsync(item => fileNames.Add(item.Key));
            return fileNames;
        }

        public async Task<string> DownloadClinicalFileFromMinIo(string downloadDirectory, string fileName)
        {
            var localPath = Path.Combine(downloadDirectory, fileName);
            await using var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write);
            await _client
                .GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(fs)));
            return localPath;
        }
        
        public async Task<Stream> GetFileFromMinIo(string fileName)
        {
            var memoryStream = new MemoryStream();
            await _client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

            memoryStream.Position = 0;
            
            var decompressedStream = new MemoryStream();
            await using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                await gzipStream.CopyToAsync(decompressedStream);
            }
            decompressedStream.Position = 0;
            return decompressedStream;
        }
    }
}