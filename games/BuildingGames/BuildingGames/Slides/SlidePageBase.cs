namespace BuildingGames.Slides;

public abstract class SlidePageBase : ContentPage
{
	public SlidePageBase()
	{
		
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        var tap = new TapGestureRecognizer
        {

        };

        tap.Tapped += Tap_Tapped;

        this.Content.GestureRecognizers.Add(tap);
    }

    private void Tap_Tapped(object sender, TappedEventArgs e)
    {
        OnCurrentSceneNext(null);
    }

    async void LoadSlide(Type sceneType)
    {
        if (sceneType.IsAssignableTo(typeof(SlideSceneBase)))
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
        else if (sceneType.IsAssignableTo(typeof(ContentPage)))
        {
            await Shell.Current.GoToAsync(sceneType.Name);
        }
    }

    private void OnCurrentSceneNext(SlideSceneBase sender)
    {
        if (SlideDeck.GetNextSlideType() is Type nextSlideType)
        {
            this.LoadSlide(nextSlideType);
        }
    }

    private void OnCurrentSceneBack(SlideSceneBase sender)
    {
        if (SlideDeck.GetPreviousSlideType() is Type previousSlideType)
        {
            this.LoadSlide(previousSlideType);
        }
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        ProgressSlides();
    }

    protected virtual void ProgressSlides()
    {
        
    }
}
