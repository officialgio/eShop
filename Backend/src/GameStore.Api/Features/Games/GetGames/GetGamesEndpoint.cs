using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/", () => data.GetGames().Select(game => new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre.Name,
            game.Price,
            game.ReleasedDate
        )));
    }
}