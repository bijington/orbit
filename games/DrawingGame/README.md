# Drawing Game

This sample game is designed to show how it could be possible to add in a drawing element in a multiplayer game.

The concept was shown in a live demo at .NET Conf 2022 focus on MAUI <https://www.youtube.com/watch?v=3jcVYUE-Tww>

## Structure

There are 3 main components to this sample application.

### DrawingGame

This is the .NET MAUI application that provides a sample drawing game experience.

### DrawingGame.Server

This is an ASP .NET Core web app that provides the SignalR communications.

### DrawingGame.Shared

This houses any shared code for us between the other projects. It currently just houses the data types that are sent to/from the server.
