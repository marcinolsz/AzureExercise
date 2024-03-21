using MediatR;
namespace AzureExercise.Application.Requests
{
    public record ListBlobsQueryRequest : IRequest<string[]>
    {
    }
}
