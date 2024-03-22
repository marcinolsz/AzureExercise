# Http and Queue triggered Azure Functions

The app consists of two Azure Functions:
- Queue triggered - saves message content to a blob storage
- HTTP triggered - lists all the files within the blob storage

## Local Setup

 ### Prerequisites
 - package manager (preferably npm)
 - azure functions core tools
```bash
npm i -g azure-functions-core-tools@4 --unsafe-perm true
```
 - azurite
 ```bash
npm install -g azurite
```
### Running the app
In the the project's directory run:
```bash
start azurite --silent & func start
```

## Suggested Usage
There are two helper functions, not really a part of the app, developed just for the convenience of not having to setup containers and publish messages manually.

StartupFunction checks for the necessary container and queue and creates them if needed.
```c#
StartupFunction: [POST] http://localhost:7071/api/startup
```
PublishMessageFunction publishes a statically defined message to the queue.

```c#
PublishMessageFunction: [POST] http://localhost:7071/api/publishMessage
```

Once these two were succesfully called the HttpFunction can be called to see existing files:
```c#
HttpFunction: [GET] http://localhost:7071/api/listBlobs
```

## Requirements
- JSON validation
- MediatR
- Dependency Injection
- Blobs being saved in timestamp directory hierarchy (year/month/day/hour/minute/{guid}.json)
- Connection strings configured in local.settings.json
