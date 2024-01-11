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
            PuckState.Size = 0.02;
            PuckState.VelocityX = 0.01;
            PuckState.VelocityY = 0.001;
            ScoreState = new();
        }

        public Guid Id { get; }

        public PlayerState PlayerOne { get; set; }

        public PlayerState PlayerTwo { get; set; }

        public PuckState PuckState { get; }

        public ScoreState ScoreState { get; }
    }

    private readonly IList<Game> games = new List<Game>();

    public IReadOnlyList<Game> Games => this.games.Where(g => g.PlayerTwo != PlayerState.Empty).ToList();

    public Game PlayGame(Guid playerId)
    {
        var openGame = this.games.FirstOrDefault(g => g.PlayerTwo == PlayerState.Empty);

        var player = new PlayerState(playerId);

        if (openGame is null)
        {
            openGame = new Game(Guid.NewGuid(), player);
            player.IsBottom = true;
            this.games.Add(openGame);
        }
        else
        {
            openGame.PlayerTwo = player;
        }

        return openGame;
    }
}