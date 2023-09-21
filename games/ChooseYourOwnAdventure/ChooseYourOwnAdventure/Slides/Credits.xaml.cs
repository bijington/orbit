using BuildingGames.Controls;

namespace BuildingGames.Slides;

public partial class Credits : ContentPage
{
	public Credits()
	{
		InitializeComponent();
	}

    async void OnLinkButtonClicked(object sender, EventArgs e)
    {
		if (sender is LinkButton linkButton)
		{
			await Browser.OpenAsync(linkButton.Text);
		}
    }
}
