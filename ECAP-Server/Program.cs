using ECAP_Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(x => x.MapHub<MirrorHub>("/listenkeyboard"));

app.Run();