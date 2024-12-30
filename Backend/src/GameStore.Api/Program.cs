using System.ComponentModel.DataAnnotations;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres = new()
{
    new Genre { Id = new Guid("490520c3-1e6c-47e9-9780-62b2cbcecd78"), Name = "Fighting" },
    new Genre { Id = new Guid("6d5586c7-120f-4ef9-b19a-cae6939a0046"), Name = "Kids and Family" },
    new Genre { Id = new Guid("5b0d1f04-d405-4260-a2e8-6d03a38adfc1"), Name = "Racing" },
    new Genre { Id = new Guid("6364e3b4-6934-479c-b37d-02cfcedaf964"), Name = "Roleplaying" },
    new Genre { Id = new Guid("e5025957-7352-40c6-b36f-43e2711991a6"), Name = "Sports" }
};

List<Game> games = new()
{
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Street Fighter II",
        Genre = genres[0], 
        Price = 19.99m,
        ReleasedDate = new DateOnly(1992, 7, 15),
        Description = "A revolutionary fighting game that introduced iconic characters and intense one-on-one battles, setting the standard for arcade fighting games."
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Final Fantasy XIV",
        Genre = genres[3],
        Price = 59.99m,
        ReleasedDate = new DateOnly(2010, 9, 30),
        Description = "An expansive MMORPG set in the fantasy world of Eorzea, offering rich storytelling, deep customization, and epic multiplayer adventures."
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "FIFA 2023",
        Genre = genres[4],
        Price = 69.99m,
        ReleasedDate = new DateOnly(2022, 9, 27),
        Description = "The latest installment in the popular football simulation series, featuring enhanced gameplay, updated rosters, and realistic visuals."
    }
};


// GET /games
app.MapGet("/games", () => games.Select(game => new GameSummaryDto(
        game.Id,
        game.Name,
        game.Genre.Name,
        game.Price,
        game.ReleasedDate
)));

// GET /games/{id}
app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(
        new GameDetailsDto(
            game.Id, 
            game.Name, 
            game.Genre.Id, 
            game.Price, 
            game.ReleasedDate, 
            game.Description)
        );
}).WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto gameDto) =>
{
    var genre = genres.Find(genre => genre.Id == gameDto.GenreId);

    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre Id");
    }

    // Transfer DTO to your Data Model
    var game = new Game
    {
        Id = Guid.NewGuid(),
        Name = gameDto.Name,
        Genre = genre,
        Price = gameDto.Price,
        ReleasedDate = gameDto.ReleaseDate,
        Description = gameDto.Description
    };
    
    games.Add(game);

    return Results.CreatedAtRoute(
        GetGameEndpointName, 
        new { id = game.Id }, 
        new GameDetailsDto(
            game.Id, 
            game.Name, 
            game.Genre.Id, 
            game.Price, 
            game.ReleasedDate, 
            game.Description));
}).WithParameterValidation();

// PUT /games/{id}
app.MapPut("/games/{id}", (Guid id, UpdateGameDto gameDto) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        Results.NotFound();
    }
    
    var genre = genres.Find(genre => genre.Id == gameDto.GenreId);

    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre Id");
    }
    
    existingGame.Name = gameDto.Name;
    existingGame.Genre = genre;
    existingGame.Price = gameDto.Price;
    existingGame.ReleasedDate = gameDto.ReleaseDate;
    existingGame.Description = gameDto.Description;

    return Results.NoContent();
}).WithParameterValidation();

// DELETE /games/{id}
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.MapGet("/genres", 
    () => genres.Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record GameDetailsDto(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price, 
    DateOnly ReleaseDate, 
    string Description);

public record GameSummaryDto(
    Guid Id, 
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate);

public record CreateGameDto(
    [Required] [StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description);

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description);

public record GenreDto(Guid Id, string Name);