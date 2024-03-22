using Azure.Storage.Queues;
using AzureExercise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureExercise.Functions.LocalHelperFunctions
{
    // A function for local testing purposes, publishes statically defined message to the QueueStorage
    public class PublishMessageFunction
    {
        private readonly ILogger<PublishMessageFunction> _logger;

        public PublishMessageFunction(ILogger<PublishMessageFunction> logger)
        {
            _logger = logger;
        }

        [FunctionName(nameof(PublishMessageFunction))]
        public async Task<IActionResult> PublishMessage([HttpTrigger("post", Route = "publishMessage")] HttpRequest req)
        {
            var queueClient = new QueueClient("UseDevelopmentStorage=true", "customer-queue");

            if (!await queueClient.ExistsAsync())
            {
                _logger.LogError("Specified queue: {0} does not exist", queueClient.Name);
                return new NotFoundResult();
            }

            var serializedMessage = JsonSerializer.Serialize(new Customer("Marcin", "Olszewski", 13));

            byte[] messageBytes = Encoding.UTF8.GetBytes(serializedMessage);

            string base64Message = Convert.ToBase64String(messageBytes);

            await queueClient.SendMessageAsync(base64Message);

            return new OkObjectResult("Message published!");
        }
    }
}
