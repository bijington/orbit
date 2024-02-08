using BuildingGames.Slides;

namespace BuildingGames;

public partial class PresenterPage : ContentPage
{
        private DateTime scheduledFinish;

    public PresenterPage()
    {
        InitializeComponent();

        SlideDeck.SlideNotesChanged += SlideDeck_SlideNotesChanged;
        this.NotesLabel.Text = SlideDeck.Notes;

        this.scheduledFinish = new DateTime(2024, 02, 06, 16, 15, 00);

        this.UpdateTime();
    }

    private void UpdateTime()
    {
        this.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(1), () =>
        {
            this.CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
            this.Countdown.Text = (this.scheduledFinish - DateTime.Now).ToString("hh\\:mm\\:ss");

            this.UpdateTime();
        });
    }

    private void SlideDeck_SlideNotesChanged(string notes)
    {
        this.NotesLabel.Text = SlideDeck.Notes;
    }

    private readonly IDictionary<Type, string> slideNotes = new Dictionary<Type, string>
    {

        [typeof(VotingSystemOrDrawingGameScene)] = @"This is our first vote so let's hope it works! As I mentioned before you can navigate to a web page, so if you scan this QR code on screen it will open the page for you.",

        [typeof(Stage1SummaryScene)] = @"OK so we have covered in some detail what SignalR is and how we can use it within both a server-side and client-side application. And in fact nothing in the client-side really breaks outside of the mould of a typical business application.

I would now like to take you on a bit of a journey to understand how I got to this point, the big bang idea (it's a space game so the pun was definitely intended).

But first I believe we have the option for a minor distraction coming up.",

        [typeof(MagicianScene)] = @"This might feel like a bit of a slightly disjointed piece of content here.

I have gained so much benefit from these concepts and wanted to give you the opportunity to as well. Of course it is up to you",

        [typeof(SlideLottie)] = @"For those of you who are not aware of Lottie it really is a fantastic resource.

Originally built and open sourced by AirBnB

It provides the ability to render animations created from Adobe After Effects

into impressively small JSON files - I say impressively small because when you compare them to an animated GIF or movie file they are a fraction of that size

They are also rendered natively on the platforms so there is no need to worry about scaling

You need to make use of the SkiaSharp.Extended.UI.Maui nuget package to use this",

        [typeof(SlideAnimations)] = @"Animations are a 2 parter here

.NET MAUI provides a powerful and yet simple API for creating and rendering animations.

The framework does ship with a set of pre-built animations to make common tasks simpler.

You can see here that we are rotating the frame around the X axis. To 90 degrees which means there is practially nothing visible on screen

Toggling the visibility of the contents of the frame and then rotating another 90 degrees.

This gives the ability to make it look like the user is actually flipping something over like a card.",

        [typeof(SlideAnimationsPartTwo)] = @"This is for when the pre-built animations are not enough and in fact the pre-built animations

make use of this underlying API and wraps them into simple extension methods.

Hopefully the code here won't look too complex however we can break it down to say

We add child animations to a single parent animation

The child animations define what percentage of the overall length a certain property is changed

So for this first line we can see that between 0 and 20% of the animation length of 500 milliseconds we will change the Scale property from 100% to 90%",

        [typeof(SlideParticleEffects)] = @"This is one my most fun things to play with.

I think this is a great example of how you can achieve something that is entirely possible on the platform despite .NET MAUI making it easy for you.",

        [typeof(SlideCombined)] = @"All of the previous little tricks are great but here you can see how when combined they can really make a big difference.",

        [typeof(TheGameEngineApproachScene)] = @"I mentioned my big bang idea which is a space related game. Originally I did knock the most basic proof of concept which was literally

just rotating images in a Xamarin.Forms application. This was good to make it look like it worked but the next step was to turn that PoC into something

that actually did work. I don't know about anyone else here but this is has been my typical job for the past few years. For that I believed I needed to get involved with using a game engine based approach in order to manage the complexity that was

going to be gravitating towards me.

Lets take a quick look at the looping approach and then dive into a bit more detail. This should feel fairly familiar to anyone that has worked on a real-time based system

First we process user input

Next we convert that user input into state

Then we render that state into something visual

Finally we wait until we need to repeat the same steps again",

        [typeof(ProcessUserInputScene)] = @"The fantastic and also potentially damning thing with .NET MAUI is the number of platforms it supports and also the idioms that entails

We learned during the tutorial that it supports mobile and desktop. This along brings us this list of possible ways to interact with the application.

I do have on my list to try and get something running on Apple TV which may also increase this possible list, although I expect Apple to wrap up their

remote support pretty well.",

        [typeof(ProcessUserInputPartTwoScene)] = @"And speaking of Apple, I wanted to give a brief indication of the effort involved in supporting a game controller.

This is a cutdown version of the game controller support for this PS4 controller to act as my clicker today.",

        [typeof(UpdateScene)] = @"The next block in our game loop is to update the game state. For this we convert any user input, apply factors based on the time taken since update was last called and then update the underlying game state

I always try to highlight that the state of your game wants to remain display independent if possible",

        [typeof(RenderScene)] = @"The next block in our game loop is to convert the game state into something visual.

Thankfully .NET MAUI provides us with .NET MAUI Graphics which can be thought of much like .NET MAUI itself... it is a unified API for interecting with each of the platform specific graphics APIs.

Graphics offers us with the GraphicsView control that provides us with a canvas that can be used to draw/paint onto the scree.

We can interact with the canvas through a stack based approach, so to make it easier to determine how and where to draw the asteroid moving towards our ship we can

- translate to it's coordinates

- rotate the canvas

- draw the asteroid",

        [typeof(WaitScene)] = @"The final block is to kick back and relax until we are called again. To be honest this slide is mostly just here to remind me to take a sip of water.",

        [typeof(OrbitGameOrOrbitEngineScene)] = @"This leads us onto our next and final decision point. As I mentioned earlier there should be the opportunity to win a prize assuming my code behaves",

        [typeof(TipsAndTricksSimpleScene)] = @"How are we doing for time? I added these slides in case we had time given the nature of this talk it was a little challenging to determine exactly how long things would take.

I can breeze through this quickly if we need",

        [typeof(SummaryScene)] = @"OK, so how did you all score today. No I am kidding, no scores just a record of the decisions that you made together.",

        [typeof(Credits)] = @"Thank you so much! I really hope this has been as enjoyable to be a part of as it was to build!

I will be around for the rest of the conference so please come and grab me and chat about anything."
    };
}
