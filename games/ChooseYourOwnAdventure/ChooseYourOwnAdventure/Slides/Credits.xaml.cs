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
		"""
		Thank you so much for joining me today!

		As a speaker I really appreciate any feedback, so if you feel particularly strongly about giving any of the colour scores outside then I would please ask you to let me know your reason. Even if it is red I would love to here why.

		I will be hanging around after this talk and will be around for the rest of the conference, so please feel free to come and say hi.
		""";

    async void OnLinkButtonClicked(object sender, EventArgs e)
    {
		if (sender is LinkButton linkButton)
		{
			await Browser.OpenAsync(linkButton.Text);
		}
    }
}
