namespace Animaite.Trainer.Common.Models;

public abstract record SpriteAnimation
{
    public required string Name { get; init; }
    public required List<string> Labels { get; init; }
    public required int Length { get; init; }
    public required float FrameRate { get; init; }

    public abstract IEnumerable<Sprite> GetFrames();
}