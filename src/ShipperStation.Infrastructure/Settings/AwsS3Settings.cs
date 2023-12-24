namespace ShipperStation.Infrastructure.Settings;

public class AwsS3Settings
{
    public static readonly string Section = "Aws:S3";

    public string BucketName { get; set; } = default!;

    public string Region { get; set; } = default!;

    public string AccessKey { get; set; } = default!;

    public string SecretKey { get; set; } = default!;
}