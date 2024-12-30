using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public class Game
{
    /// <summary>
    /// This will uniquely identity a game
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the game
    /// </summary>
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// The genre of the game
    /// </summary>
    public required Genre Genre { get; set; }

    /// <summary>
    /// The price of the game
    /// </summary>
    [Range(1, 100)]
    public decimal Price { get; set; }

    /// <summary>
    /// When the game was released
    /// </summary>
    public DateOnly ReleasedDate { get; set; }

    /// <summary>
    /// THe description of the game
    /// </summary>
    public required string Description { get; set; }
}