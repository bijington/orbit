using BuildingGames.Controls;
using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class Credits : SlidePageBase
{
	public Credits(IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
	{
		InitializeComponent();
	}

    protected override string Notes => 
		@"Thank you so much for joining me today!";

    async void OnLinkButtonClicked(object sender, EventArgs e)
    {
		if (sender is LinkButton linkButton)
		{
			await Browser.OpenAsync(linkButton.Text);
		}
    }
}
