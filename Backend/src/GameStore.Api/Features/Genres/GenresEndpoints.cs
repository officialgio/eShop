using GameStore.Api.Data;
using GameStore.Api.Features.Genres.GetGenres;

namespace GameStore.Api.Features.Genres;

public static class GenresEndpoints
{
    public static void MapGenres(this IEndpointRouteBuilder app, GameStoreData data)
    {
        var group = app.MapGroup("/genres");
        
        // GET /genres
        app.MapGetGenres(data);
    }
}