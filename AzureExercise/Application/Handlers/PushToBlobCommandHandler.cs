﻿using Azure.Storage.Blobs;
using AzureExercise.Application.Requests;
using AzureExercise.Models;
using MediatR;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureExercise.Application.Handlers
{
    public class PushToBlobCommandHandler : IRequestHandler<PushToBlobCommandRequest>
    {
        private readonly BlobContainerClient _blobContainerClient;

        public PushToBlobCommandHandler(BlobContainerClient containerClient)
        {
            _blobContainerClient = containerClient ?? throw new ArgumentNullException(nameof(containerClient));
        }

        public async Task Handle(PushToBlobCommandRequest request, CancellationToken cancellationToken)
        {
            if (!await _blobContainerClient.ExistsAsync())
            {
                throw new ContainerNotFoundException("The container could not be contacted", _blobContainerClient.Name);
            }

            var dataToPush = JsonSerializer.SerializeToUtf8Bytes(request.Customer);
            var blobClient = _blobContainerClient.GetBlobClient(GenerateBlobName());
            using var memoryStream = new MemoryStream(dataToPush);
            await blobClient.UploadAsync(memoryStream);

            return;
        }

        private static string GenerateBlobName()
        {
            var now = DateTimeOffset.UtcNow;
            var year = now.Year.ToString();
            var month = now.Month.ToString("00");
            var day = now.Day.ToString("00");
            var hour = now.Hour.ToString("00");
            var minute = now.Minute.ToString("00");
            var guid = Guid.NewGuid().ToString();

            return $"{year}/{month}/{day}/{hour}/{minute}/{guid}.json";
        }
    }
}
