using Animaite.Trainer.Common.Models;

namespace Animaite.Trainer.WebApp.DatasetManager.Services;

public class DatasetManager
{
    public SpriteSheetDataset? Dataset { get; private set; }

    public bool Loaded => Dataset != null;

    public async Task LoadDataset(string path)
    {
        Dataset = await SpriteSheetDataset.LoadAsync(path);
    }
}