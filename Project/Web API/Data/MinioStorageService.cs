using Minio;
using Minio.DataModel.Args;

namespace WebApi.Data
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(Stream data, string objectName, string contentType, CancellationToken ct = default);
        Task EnsureBucketExistsAsync(CancellationToken ct = default);
    }
    
    public class MinioStorageService : IStorageService
    {
        private readonly IMinioClient _client;
        private readonly string _endpoint;
        private readonly int _port;
        private readonly bool _useSSL;
        private readonly string _bucket;

        public MinioStorageService(IConfiguration config)
        {
            var section = config.GetSection("Minio");
            _endpoint = section["Endpoint"];
            _port     = section.GetValue<int>("Port");
            _useSSL   = section.GetValue<bool>("UseSSL");
            _bucket   = section["BucketName"];
            _client = new MinioClient()
                .WithEndpoint(_endpoint, _port)
                .WithCredentials(section["AccessKey"], section["SecretKey"])
                .WithSSL(_useSSL)
                .Build();
        }

        public async Task EnsureBucketExistsAsync(CancellationToken ct = default)
        {
            if (!await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucket), ct))
                await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucket), ct);
        }

        public async Task<string> UploadFileAsync(Stream data, string objectName, string contentType, CancellationToken ct = default)
        {
            await EnsureBucketExistsAsync(ct);
            await _client.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_bucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType), ct);
            var scheme = _useSSL ? "https" : "http";
            return $"{scheme}://{_endpoint}:{_port}/{_bucket}/{objectName}";
        }
    }
}