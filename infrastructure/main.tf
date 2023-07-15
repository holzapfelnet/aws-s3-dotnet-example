module "s3" {
  source = "terraform-aws-modules/s3-bucket/aws"
  bucket = var.aws_bucket_name
}
