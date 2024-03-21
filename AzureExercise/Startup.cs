using Azure.Storage.Blobs;
using AzureExercise.Application;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using static AzureExercise.Application.PushToBlobCommand;

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
