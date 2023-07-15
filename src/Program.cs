using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace AwsS3Example;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        var awsAccessKey = builder.GetSection("aws_access_key").Value;
        var awsSecretKey = builder.GetSection("aws_secret_key").Value;
        var awsBucketName = builder.GetSection("aws_bucket_name").Value;

        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
        var s3Client = new AmazonS3Client(awsCredentials);

        using var inputStream = new FileStream("./sp500_companies.csv", FileMode.Open, FileAccess.Read);

        var putObject = new PutObjectRequest
        {
            BucketName = awsBucketName,
            Key = "files/sp500_companies.csv",
            ContentType = "text/csv",
            InputStream = inputStream,
            Metadata = {
                ["x-amz-meta-originalname"] = inputStream.Name
            }
        };

        Console.WriteLine("Uploading object to S3...");
        await s3Client.PutObjectAsync(putObject);
        Console.WriteLine("Done!");

        var getObject = new GetObjectRequest
        {
            BucketName = awsBucketName,
            Key = "files/sp500_companies.csv",

        };

        Console.WriteLine("Downloading object from S3...");
        var getResponse = await s3Client.GetObjectAsync(getObject);
        var memoryStream = new MemoryStream();
        getResponse.ResponseStream.CopyTo(memoryStream);
        var result = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
        Console.WriteLine("Done!");
        Console.WriteLine(result);

        var delteObject = new DeleteObjectRequest
        {
            BucketName = awsBucketName,
            Key = "files/sp500_companies.csv",

        };

        Console.WriteLine("Deleting object from S3...");
        await s3Client.DeleteObjectAsync(delteObject);
        Console.WriteLine("Done!");
    }
}
