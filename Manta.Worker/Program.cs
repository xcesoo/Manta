using Manta.Application;
using Manta.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddWorkerInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();