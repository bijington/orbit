using AirHockey.Server;
using AirHockey.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<GameWorker>();

builder.Services.AddSingleton<GameManager>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

//app.MapGet("/", () => "Hello World!");

app.MapHub<GameHub>("Game");

app.Run();


// using DrawingGame.Server.Hubs;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddSignalR();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// }

// app.UseHttpsRedirection();

// app.MapHub<GameHub>("Game");

// app.Run();