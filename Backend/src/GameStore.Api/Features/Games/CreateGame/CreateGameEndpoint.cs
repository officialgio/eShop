using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapPost("/", (CreateGameDto gameDto) =>
        {
            var genre = data.GetGenre(gameDto.GenreId);

            if (genre is null)
            {
                return Results.BadRequest("Invalid Genre Id");
            }

            // Transfer DTO to your Data Model
            var game = new Game
            {
                Name = gameDto.Name,
                Genre = genre,
                Price = gameDto.Price,
                ReleasedDate = gameDto.ReleaseDate,
                Description = gameDto.Description
            };
    
            data.AddGame(game);

            return Results.CreatedAtRoute(
                EndpointNames.GetGame, 
                new { id = game.Id }, 
                new GameDetailsDto(
                    game.Id, 
                    game.Name, 
                    game.Genre.Id, 
                    game.Price, 
                    game.ReleasedDate, 
                    game.Description));
        }).WithParameterValidation();
    }
}