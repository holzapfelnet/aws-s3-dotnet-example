# AWS S3 example using .Net C#

This example shows how to upload and download a file to an Amazon S3 bucket using C#.

First, we have to create the S3 bucket. Therefore we will use Terraform. You have to create
a file named _develop.tfvars_ inside the folder infrastructure with the following content:

```
aws_access_key = "your_access_key"
aws_secret_key = "your_secret_key"
aws_bucket_name = "your-bucket-name"
region = "your-prefered-amazon-region"
```

Afterwards, you can execute the following commands:

```
terraform plan -var-file=develop.tfvars

terraform apply -var-file=develop.tfvars
```

When the S3 bucket is created, you can run the .Net application.
Before you can execute the application, you have to set some user
secrets:

```
dotnet user-secrets init

dotnet user-secrets set "aws_access_key" "your_access_key"
dotnet user-secrets set "aws_secret_key" "your_secret_key"
dotnet user-secrets set "aws_bucket_name" "your-bucket-name"

dotnet build

dotnet run
```

To clean everything up, you have to run the following command

```
terraform destroy -var-file=develop.tfvars
```

# Links

- [AWS-Managementkonsole](https://aws.amazon.com/de/console/)
- [Cloud Fundamentals: AWS Services for C# Developers by Nick Chapsas](https://app.dometrain.com/courses/cloud-fundamentals-aws-services-for-c-developers)
