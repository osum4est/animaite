namespace Animaite.Trainer.Common.Models;

public record Sprite
{
    public required int X { get; init; }
    public required int Y { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
}