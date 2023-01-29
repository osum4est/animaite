namespace Animaite.Trainer.Common.Models;

public record GridSpriteAnimation : SpriteAnimation
{
    public required GridSpriteSheetGroup Group;
    public required int X;
    public required int Y;

    public override IEnumerable<Sprite> GetFrames()
    {
        return Group.Sprites.Skip(Y * Group.Cols + X).Take(Length);
    }
}