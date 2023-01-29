using Animaite.Trainer.Common.Utils;

namespace Animaite.Trainer.Common.Models;

public record SpriteSheetDataset
{
    public required List<SpriteSheetGroup> Groups { get; init; }

    public static async Task<SpriteSheetDataset> LoadAsync(string path)
    {
        return await SpriteSheetDatasetUtils.LoadDatasetAsync(path);
    }
}