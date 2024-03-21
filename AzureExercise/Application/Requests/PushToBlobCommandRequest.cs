using AzureExercise.Models;
using MediatR;

namespace AzureExercise.Application.Requests
{
    public record PushToBlobCommandRequest : IRequest
    {
        public Customer Customer { get; init; }

        public PushToBlobCommandRequest(Customer customer)
        {
            Customer = customer;
        }
    }
}
