using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
    private readonly List<Genre> genres = new()
    {
        new Genre { Id = new Guid("490520c3-1e6c-47e9-9780-62b2cbcecd78"), Name = "Fighting" },
        new Genre { Id = new Guid("6d5586c7-120f-4ef9-b19a-cae6939a0046"), Name = "Kids and Family" },
        new Genre { Id = new Guid("5b0d1f04-d405-4260-a2e8-6d03a38adfc1"), Name = "Racing" },
        new Genre { Id = new Guid("6364e3b4-6934-479c-b37d-02cfcedaf964"), Name = "Roleplaying" },
        new Genre { Id = new Guid("e5025957-7352-40c6-b36f-43e2711991a6"), Name = "Sports" }
    };

    private readonly List<Game> games;

    public GameStoreData()
    {
        this.games = new()
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
    }

    public IEnumerable<Game> GetGames() => games;
    
    public Game? GetGame(Guid id) =>  games.Find(game => game.Id == id);

    public void AddGame(Game game)
    {
        game.Id = Guid.NewGuid();
        games.Add(game);
    }

    public void RemoveGame(Guid id)
    {
        games.RemoveAll(game => game.Id == id);
    }

    public IEnumerable<Genre> GetGenres() => genres;

    public Genre? GetGenre(Guid id) => genres.Find(genre => genre.Id == id);
}