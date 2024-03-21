using System;

namespace AzureExercise.Models
{
    public class ContainerNotFoundException : Exception
    {
        public string ContainerName { get; }
        public ContainerNotFoundException(string message, string containerName)
            : base(message)
        {
            ContainerName = containerName;
        }
    }
}
