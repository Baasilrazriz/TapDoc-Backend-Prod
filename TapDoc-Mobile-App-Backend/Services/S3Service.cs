using Minio;
using Minio.DataModel.Args;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class S3Service
    {
        private readonly string S3_HOST, USER, ACCESS_KEY, BUCKET;
        private readonly IMinioClient sClient;
        private readonly EOLogger logger;
        public S3Service(IConfiguration configuration, EOLogger logger)
        {
            S3_HOST = configuration.GetValue<string?>("S3:HOST") ?? throw new Exception("S3: Host Not Found");
            USER = configuration.GetValue<string?>("S3:USER") ?? throw new Exception("S3: User not found");
            ACCESS_KEY = configuration.GetValue<string?>("S3:ACCESS_KEY") ?? throw new Exception("S3: Access key not found");
            BUCKET = configuration.GetValue<string?>("S3:BUCKET") ?? throw new Exception("S3: Bucket name not found");


            sClient = new MinioClient()
                .WithEndpoint(S3_HOST)
                .WithSSL()
                .WithCredentials(USER, ACCESS_KEY)
                .Build();
            this.logger = logger;
        }

        public async Task<string> UploadFile(string fileName, long fileSize, Stream stream)
        {
            var extension = fileName.Split(".").LastOrDefault();
            if (extension == null)
            {
                logger.LogWarning("File without extension uploaded", new
                {
                    fileName
                });
                extension = "unknown";
            }
            var path = Path.GetRandomFileName() + "." + extension;
            var args = new PutObjectArgs()
                .WithObject(path)
                .WithObjectSize(fileSize)
                .WithBucket(BUCKET)
                .WithStreamData(stream);
            try
            {
                var response = await sClient.PutObjectAsync(args);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred", new
                {
                    ex.Message
                });
            }

            return string.Format("https://{0}/{1}/{2}", S3_HOST, BUCKET, path);

        }
        public async Task<string> UploadFile(IFormFile file)
        {
            var extension = file.FileName.Split(".").LastOrDefault();
            if (extension == null)
            {
                logger.LogWarning("File without extension uploaded", new
                {
                    file.FileName
                });
                extension = "unknown";
            }
            var path = Path.GetRandomFileName() + "." + extension;
            var args = new PutObjectArgs()
                .WithObject(path)
                .WithObjectSize(file.Length)
                .WithBucket(BUCKET)
                .WithStreamData(file.OpenReadStream());
            try
            {
                var response = await sClient.PutObjectAsync(args);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred", new
                {
                    ex.Message
                });
            }

            return string.Format("https://{0}/{1}/{2}", S3_HOST, BUCKET, path);

        }
    }
}
