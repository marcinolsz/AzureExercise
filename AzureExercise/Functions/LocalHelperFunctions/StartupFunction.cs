﻿using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues;

namespace AzureExercise.Functions.LocalHelperFunctions
{
    // A function for local testing purposes, checks for needed blob container and queue storage, creates them if necessary
    public class StartupFunction
    {
        [FunctionName(nameof(StartupFunction))]
        public async Task<IActionResult> Startup([HttpTrigger("get", Route = "startup")] HttpRequest req)
        {
            var containerClient = new BlobContainerClient("UseDevelopmentStorage=true", "private-container");
            await containerClient.CreateIfNotExistsAsync();
            var queueClient = new QueueClient("UseDevelopmentStorage=true", "myqueue-items");
            await queueClient.CreateIfNotExistsAsync();

            return new OkObjectResult("Startup finished, everything in place!");
        }
    }
}
