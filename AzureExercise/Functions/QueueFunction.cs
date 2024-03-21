using AzureExercise.Application;
using AzureExercise.Models;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureExercise.Functions
{
    public class QueueFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QueueFunction> _logger;
        public QueueFunction(
            IMediator mediator,
            ILogger<QueueFunction> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName(nameof(QueueFunction))]
        public async Task Run([QueueTrigger("%QueueName%", Connection = "AzureWebJobsStorage")] string queueItem)
        {
            try
            {
                var person = JsonSerializer.Deserialize<Customer>(queueItem);
                await _mediator.Send(new PushToBlobCommand(person));
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Failed to deserialize JSON: {ex.Message}");
            }
        }
    }
}
