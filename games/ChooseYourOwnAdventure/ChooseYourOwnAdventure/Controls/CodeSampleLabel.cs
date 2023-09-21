namespace BuildingGames.Controls;

public class CodeSampleLabel : Label
{
	public CodeSampleLabel()
	{
		FontFamily = Styling.CodeFontName;
		FontSize = Styling.CodeSize;
		TextColor = Styling.CodeColor;
		Margin = new Thickness(40, 20, 0, 20);
	}
}
