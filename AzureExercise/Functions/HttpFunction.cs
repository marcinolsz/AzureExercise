using AzureExercise.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AzureExercise.Functions
{
    public class HttpFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HttpFunction> _logger;
        public HttpFunction(
            IMediator mediator,
            ILogger<HttpFunction> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }

        [FunctionName(nameof(HttpFunction))]
        public async Task<IActionResult> ListBlobs([HttpTrigger("get", Route = "listBlobs")] HttpRequest req)
        {
            var blobs = await _mediator.Send(new ListBlobsQuery());

            return new OkObjectResult(blobs);
        }
    }
}
