using Azure.Storage.Blobs;
using AzureExercise.Application.Requests;
using AzureExercise.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureExercise.Application.Handlers
{
    public class ListBlobsQueryHandler : IRequestHandler<ListBlobsQueryRequest, string[]>
    {
        private readonly BlobContainerClient _blobContainerClient;

        public ListBlobsQueryHandler(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient ?? throw new ArgumentNullException(nameof(blobContainerClient));
        }

        public async Task<string[]> Handle(ListBlobsQueryRequest request, CancellationToken cancellationToken)
        {
            if (!await _blobContainerClient.ExistsAsync())
            {
                throw new ContainerNotFoundException("The container could not be contacted", _blobContainerClient.Name);
            }

            var blobNames = new List<string>();

            await foreach (var blobItem in _blobContainerClient.GetBlobsAsync())
            {
                blobNames.Add(blobItem.Name);
            }

            return blobNames.ToArray();
        }
    }
}
