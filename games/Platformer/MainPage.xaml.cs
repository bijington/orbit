using Orbit.Engine;
using Orbit.Input;

using Platformer.GameScenes;

namespace Platformer;

public partial class MainPage : ContentPage
{
    private readonly Orbit.Input.GameController gameController;
	private readonly IGameSceneManager gameSceneManager;
    private readonly PlayerStateManager playerStateManager;
    private readonly SettingsService settingsService;

    public MainPage(
        IGameSceneManager gameSceneManager,
        PlayerStateManager playerStateManager,
        SettingsService settingsService,
        Orbit.Input.GameController gameController)
    {
        InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.playerStateManager = playerStateManager;
        this.settingsService = settingsService;
        this.gameController = gameController;
        
        this.gameController.Initialise();
        this.gameController.When(
            button: "ButtonSouth",
            isPressed: isPressed =>
            {
                if (isPressed)
                {
                    this.playerStateManager.State |= CharacterState.Jumping;
                }
                else
                {
                    this.playerStateManager.State ^= CharacterState.Jumping;
                }
            });

        gameSceneManager.StateChanged += OnGameSceneManagerStateChanged;
        gameSceneManager.LoadScene<FirstScene>(GameView);
        gameSceneManager.Start();
    }

	private void OnGameSceneManagerStateChanged(object? sender, GameStateChangedEventArgs e)
	{

	}

	void OnGameViewEndInteraction(object sender, TouchEventArgs e)
    {
    }

    void OnGameViewStartInteraction(object sender, TouchEventArgs e)
    {
    }
    
    private void OnJumpButtonPressed(object? sender, EventArgs e)
    {
        this.playerStateManager.State = CharacterState.Jumping;
    }

    private void OnLeftButtonPressed(object? sender, EventArgs e)
    {
        this.playerStateManager.State = CharacterState.MovingLeft;
    }
    
    private void OnRightButtonPressed(object? sender, EventArgs e)
    {
        this.playerStateManager.State = CharacterState.MovingRight;
    }
    
    private void OnJumpButtonReleased(object? sender, EventArgs e)
    {
        this.playerStateManager.State = CharacterState.Idle;
    }

    private void OnLeftButtonReleased(object? sender, EventArgs e)
    {
        this.playerStateManager.State = CharacterState.Idle;
    }
    
    private void OnRightButtonReleased(object? sender, EventArgs e)
    {
        this.playerStateManager.State = CharacterState.Idle;
    }

    private void OnShowDebugCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        this.settingsService.ShowDebug = e.Value;
    }
}
