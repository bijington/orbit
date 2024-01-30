This game aims to provide an example on how to build a real-time multiplayer game through the use of Orbit (.NET MAUI Game Engine), ASP.NET Core Server tech; SignalR, BackgroundService.

The projects are broken down into the following:

## AirHockey

This contains the .NET MAUI based application.

## AirHockey.Server

This contains a .NET ASP Core Web app that utilises the following pieces of technology:

- SignalR
- BackgroundService

## AirHockey.Shared

This contains the shared code between the server and client applications. Hopefully this helps to highlight how much code can be shared between the two.

# Running the application

It is currently configured to host the web app locally which can be done via:

## Server

```dotnetcli
dotnet run --project AirHockey.Server/AirHockey.Server.csproj 
```

## Client

Run from within Visual Studio or Visual Studio Code.
