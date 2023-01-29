using Animaite.Trainer.Common.Models;
using SixLabors.ImageSharp;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Animaite.Trainer.Common.Utils;

public static class SpriteSheetDatasetUtils
{
    public static async Task<SpriteSheetDataset> LoadDatasetAsync(string path)
    {
        var text = File.ReadAllTextAsync(path);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var dataset = deserializer.Deserialize<YamlSchema.SpriteSheetDataset>(await text);

        var root = Directory.GetParent(path)!.FullName;

        var groups = new List<SpriteSheetGroup>();
        foreach (var group in dataset.Groups)
        {
            switch (group.Type)
            {
                case "grid":
                    groups.Add(await LoadGridSpriteSheetGroupAsync(root, group));
                    break;
            }
        }

        return new SpriteSheetDataset
        {
            Groups = groups
        };
    }

    private static async Task<GridSpriteSheetGroup> LoadGridSpriteSheetGroupAsync(string root,
        YamlSchema.SpriteSheetGroup yaml)
    {
        var cols = yaml.Cols ?? throw new Exception("Cols is required for grid sprite sheet groups");
        var rows = yaml.Rows ?? throw new Exception("Rows is required for grid sprite sheet groups");
        var spriteWidth = yaml.SpriteWidth;
        var spriteHeight = yaml.SpriteHeight;

        var spriteSheets = yaml.Sheets.Select(sheet => Path.Join(root, sheet)).ToList();
        var sheetImage = await Image.LoadAsync(spriteSheets[0]);
        spriteWidth ??= sheetImage.Width / cols;
        spriteHeight ??= sheetImage.Height / rows;

        var sprites = new List<Sprite>();
        var animations = new List<SpriteAnimation>();

        var group = new GridSpriteSheetGroup
        {
            Name = yaml.Name,
            SpriteSheets = spriteSheets,
            Labels = yaml.Labels,
            FrameRate = yaml.Fps,
            Sprites = sprites,
            Animations = animations,
            Cols = cols,
            Rows = rows,
            SpriteWidth = spriteWidth.Value,
            SpriteHeight = spriteHeight.Value,
        };

        for (var y = 0; y < rows; y++)
        for (var x = 0; x < cols; x++)
        {
            sprites.Add(new Sprite
            {
                X = x * spriteWidth.Value,
                Y = y * spriteHeight.Value,
                Width = spriteWidth.Value,
                Height = spriteHeight.Value,
            });
        }

        foreach (var animation in yaml.Animations)
        {
            animations.Add(new GridSpriteAnimation
            {
                Name = animation.Name,
                Labels = animation.Labels,
                Length = animation.Length,
                FrameRate = yaml.Fps,
                Group = group,
                X = animation.X ?? throw new Exception("X is required for grid sprite animations"),
                Y = animation.Y ?? throw new Exception("Y is required for grid sprite animations"),
            });
        }

        return group;
    }

    private static class YamlSchema
    {
        public record SpriteSheetDataset
        {
            public required List<SpriteSheetGroup> Groups { get; init; }
        }

        public record SpriteSheetGroup
        {
            public required string Name { get; init; }
            public required List<string> Sheets { get; init; }
            public required string Type { get; init; }
            public int? Cols { get; init; }
            public int? Rows { get; init; }
            public int? SpriteWidth { get; init; }
            public int? SpriteHeight { get; init; }
            public required float Fps { get; init; }
            public required List<string> Labels { get; init; }
            public required List<SpriteAnimation> Animations { get; init; }
        }

        public record SpriteAnimation
        {
            public required string Name { get; init; }
            public required List<string> Labels { get; init; }
            public required int Length { get; init; }
            public int? X { get; init; }
            public int? Y { get; init; }
        }
    }
}