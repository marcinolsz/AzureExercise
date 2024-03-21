using Azure.Storage.Blobs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureExercise.Application
{
    public class ListBlobsQuery : IRequest<string[]>
    {
        public class ListBlobsQueryHandler : IRequestHandler<ListBlobsQuery, string[]>
        {
            private readonly BlobContainerClient _blobContainerClient;

            public ListBlobsQueryHandler(BlobContainerClient blobContainerClient)
            {
                _blobContainerClient = blobContainerClient;
            }

            public async Task<string[]> Handle(ListBlobsQuery request, CancellationToken cancellationToken)
            {
                var blobNames = new List<string>();

                await foreach (var blobItem in _blobContainerClient.GetBlobsAsync())
                {
                    blobNames.Add(blobItem.Name);
                }

                return blobNames.ToArray();
            }
        }
    }
}
