﻿using AzureExercise.Application.Requests;
using AzureExercise.Models;
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
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName(nameof(HttpFunction))]
        public async Task<IActionResult> ListBlobs([HttpTrigger("get", Route = "listBlobs")] HttpRequest req)
        {
            string[] blobs = Array.Empty<string>();

            try
            {
                blobs = await _mediator.Send(new ListBlobsQueryRequest());
            }
            catch (ContainerNotFoundException ex) 
            {
                _logger.LogError(exception: ex, "The specified container: {0} does not exist", ex.ContainerName);
            }

            return new OkObjectResult(blobs);
        }
    }
}
