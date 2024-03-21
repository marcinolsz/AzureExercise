using Azure.Storage.Queues;
using AzureExercise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureExercise.Functions.LocalHelperFunctions
{
    // A function for local testing purposes, publishes statically defined message to the QueueStorage
    public class PublishMessageFunction
    {
        [FunctionName(nameof(PublishMessageFunction))]
        public async Task<IActionResult> PublishMessage([HttpTrigger("get", Route = "publishMessage")] HttpRequest req)
        {
            var queueClient = new QueueClient("UseDevelopmentStorage=true", "customer-queue");

            var serializedMessage = JsonSerializer.Serialize(new Customer("Marcin", "Olszewski", 13));

            byte[] messageBytes = Encoding.UTF8.GetBytes(serializedMessage);

            string base64Message = Convert.ToBase64String(messageBytes);

            await queueClient.SendMessageAsync(base64Message);

            return new OkObjectResult("Message published!");
        }
    }
}
