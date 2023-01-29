namespace Animaite.Trainer.Common.Models;

public record GridSpriteSheetGroup : SpriteSheetGroup
{
    public required int Cols;
    public required int Rows;
}