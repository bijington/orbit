using BuildingGames.Models;

namespace BuildingGames.Scenes;

public class CharacterSelectionScene : SlideSceneBase
{
    const float padding = 40f;
    const int columns = 3;
    const int rows = 2;

    private readonly IDictionary<string, Microsoft.Maui.Graphics.IImage> images;
    private readonly IList<Character> characters;
    private readonly ControllerManager controllerManager;
    private int currentCharacterIndex;
    private bool handlingKeyPress = false;

    public override bool CanProgress => currentCharacterIndex == 0;

    public CharacterSelectionScene(ControllerManager controllerManager)
    {
        images = new Dictionary<string, Microsoft.Maui.Graphics.IImage>
        {
            ["shaun.png"] = LoadImage("shaun.png")
        };

        this.characters = new List<Character>
        {
            new Character
            {
                Name = "Shaun Lawrence",
                Strengths = new List<string>
                {
                    "15+ years experience",
                    "Microsoft MVP",
                    "Author of 'Introducing .NET MAUI'"
                },
                Weaknesses = new List<string>
                {
                    "Distracted easily",
                    "Rubbish at paddleboarding",
                    "Cake"
                },
                ImageName = "shaun.png"
            },
            new Character
            {
                Name = "??",
                IsLocked = true,
                UnlockCriteria = "Reach 1,000m to unlock this boarder"
            },
            new Character
            {
                Name = "??",
                IsLocked = true,
                UnlockCriteria = "Reach 10,000m to unlock this boarder"
            },
            new Character
            {
                Name = "??",
                IsLocked = true,
                UnlockCriteria = "Reach 30,000m to unlock this boarder"
            },
            new Character
            {
                Name = "??",
                IsLocked = true,
                UnlockCriteria = "Reach 100,000m to unlock this boarder"
            },
            new Character
            {
                Name = "??",
                IsLocked = true,
                UnlockCriteria = "Reach 1,000,000m to unlock this boarder"
            }
        };

        this.controllerManager = controllerManager;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var font = Styling.Font;
        canvas.Font = font;
        canvas.FontSize = 50;
        canvas.FontColor = Colors.White;
        canvas.SetShadow(new SizeF(5, 5), 5, Colors.Black);

        // Title
        // 1 2 |
        // 3 4 |

        canvas.DrawString(
            dimensions,
            "Choose your boarder",
            font,
            50,
            new PointF(0, 30),
            HorizontalAlignment.Center,
            VerticalAlignment.Center);

        float tileWidth = ((dimensions.Width / columns) - (padding * (columns + 1))) / rows;
        float yOffset = 200;

        canvas.FillColor = Colors.Black;
        canvas.StrokeColor = Colors.Orange;
        canvas.StrokeSize = 4;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                canvas.FillRectangle(
                    padding * (x + 1) + (tileWidth * x),
                    padding * (y + 1) + (tileWidth * y) + yOffset,
                    tileWidth,
                    tileWidth);

                if (x == 0 &&
                    y == 0)
                {
                    var image = images["shaun.png"];

                    canvas.DrawImage(
                        image,
                        padding * (x + 1) + (tileWidth * x),
                        padding * (y + 1) + (tileWidth * y) + yOffset,
                        tileWidth,
                        tileWidth);
                }
            }
        }

        // Based on characterIndex;
        var x1 = this.currentCharacterIndex % columns;
        var y1 = this.currentCharacterIndex / columns;

        canvas.DrawRectangle(
            padding * (x1 + 1) + (tileWidth * x1),
            padding * (y1 + 1) + (tileWidth * y1) + yOffset,
            tileWidth,
            tileWidth);

        var character = this.characters[currentCharacterIndex];

        canvas.Font = font;
        canvas.FontSize = 30;
        canvas.FontColor = Colors.White;

        var columnWidth = dimensions.Width / 2;

        canvas.DrawString(
            new RectF(dimensions.Center.X + padding, yOffset, columnWidth, 100),
            $"Name:{Environment.NewLine}{character.Name}",
            font,
            30,
            new PointF(dimensions.Center.X + padding, yOffset),
            HorizontalAlignment.Left,
            VerticalAlignment.Center);

        if (character.IsLocked is false)
        {
            canvas.DrawString(
                new RectF(dimensions.Center.X + padding, yOffset + 100, columnWidth, 100),
                $"Strengths:{Environment.NewLine}{string.Join(Environment.NewLine, character.Strengths)}",
                font,
                30,
                new PointF(dimensions.Center.X + padding, yOffset + 100),
                HorizontalAlignment.Left,
                VerticalAlignment.Center);

            canvas.DrawString(
                new RectF(dimensions.Center.X + padding, yOffset + 400, columnWidth, 100),
                $"Weaknesses:{Environment.NewLine}{string.Join(Environment.NewLine, character.Weaknesses)}",
                font,
                30,
                new PointF(dimensions.Center.X + padding, yOffset + 400),
                HorizontalAlignment.Left,
                VerticalAlignment.Center);
        }
        else
        {
            canvas.DrawString(
                new RectF(dimensions.Center.X + padding, yOffset + 100, columnWidth, 100),
                character.UnlockCriteria,
                font,
                30,
                new PointF(dimensions.Center.X + padding, yOffset + 100),
                HorizontalAlignment.Left,
                VerticalAlignment.Center);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (controllerManager.CurrentPressedButton == ControllerButton.Right &&
            !handlingKeyPress)
        {
            handlingKeyPress = true;
            this.currentCharacterIndex = Math.Clamp(this.currentCharacterIndex + 1, 0, this.characters.Count);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.Left &&
            !handlingKeyPress)
        {
            handlingKeyPress = true;
            this.currentCharacterIndex = Math.Clamp(this.currentCharacterIndex - 1, 0, this.characters.Count);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.Down &&
            !handlingKeyPress)
        {
            handlingKeyPress = true;
            this.currentCharacterIndex = Math.Clamp(this.currentCharacterIndex + columns, 0, this.characters.Count);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.Up &&
            !handlingKeyPress)
        {
            handlingKeyPress = true;
            this.currentCharacterIndex = Math.Clamp(this.currentCharacterIndex - columns, 0, this.characters.Count);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.None)
        {
            handlingKeyPress = false;
        }
    }
}
