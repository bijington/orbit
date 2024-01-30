using System;
using System.Collections.Generic;
using AirHockey.Shared;

namespace AirHockey.Server;

public class GameManager
{
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