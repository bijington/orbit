using System;
using System.Collections.Generic;
using AirHockey.Shared;

namespace AirHockey.Server;

public class GameManager
{
    public class Game
    {
        public Game(Guid id, PlayerState playerOne)
        {
            Id = id;
            PlayerOne = playerOne;
            PlayerTwo = PlayerState.Empty;
            PuckState = new();
            PuckState.X = 0.5;
            PuckState.Y = 0.5;
            PuckState.VelocityX = 0.01;
            PuckState.VelocityY = 0.001;
            ScoreState = new();
        }

        public Guid Id { get; }

        public PlayerState PlayerOne { get; }

        public PlayerState PlayerTwo { get; set; }

        public PuckState PuckState { get; }

        public ScoreState ScoreState { get; }
    }

    public class PlayerState
    {
        public PlayerState(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public double X { get; set; }

        public double Y { get; set; }

        public static PlayerState Empty { get; } = new PlayerState(Guid.Empty);
    }

    private readonly IList<Game> games = new List<Game>();

    public IReadOnlyList<Game> Games => this.games.Where(g => g.PlayerTwo != PlayerState.Empty).ToList();

    public Guid PlayGame(Guid playerId)
    {
        var openGame = this.games.FirstOrDefault(g => g.PlayerTwo == PlayerState.Empty);

        var player = new PlayerState(playerId);

        if (openGame is null)
        {
            openGame = new Game(Guid.NewGuid(), player);
            this.games.Add(openGame);
        }
        else
        {
            openGame.PlayerTwo = player;
        }

        return openGame.Id;
    }
}