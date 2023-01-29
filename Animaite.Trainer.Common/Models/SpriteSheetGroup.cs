namespace Animaite.Trainer.Common.Models;

public abstract record SpriteSheetGroup
{
    public required string Name { get; init; }
    public required List<string> SpriteSheets { get; init; }
    public required List<string> Labels { get; init; }
    public required float FrameRate { get; init; }
    public required List<Sprite> Sprites { get; init; }
    public required List<SpriteAnimation> Animations { get; init; }
    public required int SpriteWidth;
    public required int SpriteHeight;
}