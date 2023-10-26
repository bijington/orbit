namespace BuildingGames.Controls;

public class TitleLabel : Label
{
	public TitleLabel()
	{
		FontFamily = Styling.FontName;
		FontSize = Styling.TitleSize;
		TextColor = Styling.TitleColor;

		HorizontalTextAlignment = TextAlignment.Center;
	}
}
