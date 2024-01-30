using AirHockey.Server;
using AirHockey.Server.Hubs;
using AirHockey.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<GameWorker>();

builder.Services.AddSingleton<GameManager>();
builder.Services.AddTransient<GameStateManager>();
builder.Services.AddTransient<IGameLifeCycleHandler, SignalRGameLifeCycleHandler>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapHub<GameHub>("Game");

app.Run();