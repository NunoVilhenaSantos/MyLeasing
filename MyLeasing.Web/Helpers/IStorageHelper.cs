﻿using Google.Cloud.Storage.V1;

namespace MyLeasing.Web.Helpers;

public interface IStorageHelper
{
    Task<Guid> UploadStorageAsync(IFormFile file, string bucketName);

    Task<Guid> UploadStorageAsync(byte[] file, string bucketName);

    Task<Guid> UploadStorageAsync(string file, string bucketName);


    Task<bool> CopyFile(
        string sourceBucketName = "source-bucket-name",
        string sourceObjectName = "source-file",
        string destBucketName = "destination-bucket-name",
        string destObjectName = "destination-file-name")
    {
        var storage = StorageClient.Create();
        storage.CopyObject(
            sourceBucketName,
            sourceObjectName,
            destBucketName,
            destObjectName);

        Console.WriteLine(
            "Copied " +
            $"{sourceBucketName}/{sourceObjectName}" +
            " to " +
            $"{destBucketName}/{destObjectName}.");

        return Task.FromResult(
            storage.GetNotification(
                sourceBucketName, sourceObjectName) != null);
    }


    Task<Guid> UploadFileAsyncToGcp(IFormFile fileToUpload,
        string fileNameToSave);

    // [BEGIN storage_stream_file_upload]
    // [BEGIN storage_upload_file]


    // Copyright 2020 Google Inc.
    //
    // Licensed under the Apache License, Version 2.0 (the "License");
    // you may not use this file except in compliance with the License.
    // You may obtain a copy of the License at
    //
    //     http://www.apache.org/licenses/LICENSE-2.0
    //
    // Unless required by applicable law or agreed to in writing, software
    // distributed under the License is distributed on an "AS IS" BASIS,
    // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    // See the License for the specific language governing permissions and
    // limitations under the License.

    // [START storage_upload_file]
    // [START storage_stream_file_upload]

    public class UploadFileSample
    {
        public void UploadFileToGcp(
            string bucketName = "your-unique-bucket-name",
            string localPath = "my-local-path/my-file-name",
            string objectName = "my-file-name")
        {
            var storage = StorageClient.Create();
            using var fileStream = File.OpenRead(localPath);
            storage.UploadObject(bucketName, objectName, null, fileStream);
            Console.WriteLine($"Uploaded {objectName}.");
        }
    }


    // [END storage_stream_file_upload]
    // [END storage_upload_file]
}