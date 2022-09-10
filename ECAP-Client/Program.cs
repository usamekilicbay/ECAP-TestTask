using ECAP_Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Press \"Enter\" to kill my masterpiece...");

var services = new ServiceCollection();
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

services.AddSingleton(configuration);
services.AddSingleton<ClientManager>();

var serviceProvider = services.BuildServiceProvider();

var receiver = serviceProvider.GetService<ClientManager>();
await receiver.ContinuousTyping();
