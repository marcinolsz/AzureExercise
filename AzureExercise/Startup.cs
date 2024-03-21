using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

[assembly: FunctionsStartup(typeof(AzureExercise.Startup))]

namespace AzureExercise
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddSingleton(new BlobContainerClient(
                builder.GetContext().Configuration["AzureWebJobsStorage"],
                builder.GetContext().Configuration["ContainerName"]));
        }
    }
}
