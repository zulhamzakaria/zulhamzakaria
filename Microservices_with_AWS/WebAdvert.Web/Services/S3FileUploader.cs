﻿using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebAdvert.Web.Services
{
    public class S3FileUploader : IFileUploader
    {
        private readonly IConfiguration configuration;

        public S3FileUploader(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<bool> UploadFileAsync(string fileName, Stream storageStream)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name must be specified");

            var bucketName = configuration.GetValue<string>("ImageBucket");

            using (var client = new AmazonS3Client())
            {
                if (storageStream.Length > 0)
                    if (storageStream.CanSeek)
                        storageStream.Seek(0, SeekOrigin.Begin);

                var request = new PutObjectRequest
                {
                    AutoCloseStream = true,
                    BucketName = bucketName,
                    InputStream = storageStream,
                    Key = fileName
                };

                // PutObjectAsync only works if the file is not too big
                var response = await client.PutObjectAsync(request).ConfigureAwait(false);
                return response.HttpStatusCode == HttpStatusCode.OK;
            }
        }
    }
}
