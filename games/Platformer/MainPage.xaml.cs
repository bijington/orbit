using Orbit.Engine;
using Orbit.Input;

using Platformer.GameScenes;

namespace Platformer;

public partial class MainPage : ContentPage
{
    private Orbit.Input.GameController? gameController;
    private readonly Orbit.Input.GameControllerManager gameControllerManager;
    private readonly KeyboardManager keyboardManager;
    private readonly IGameSceneManager gameSceneManager;
    private readonly PlayerStateManager playerStateManager;
    private readonly SettingsService settingsService;

    public MainPage(
        IGameSceneManager gameSceneManager,
        PlayerStateManager playerStateManager,
        SettingsService settingsService,
        Orbit.Input.GameControllerManager gameControllerManager,
        KeyboardManager keyboardManager)
    {
        InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.playerStateManager = playerStateManager;
        this.settingsService = settingsService;
        this.gameControllerManager = gameControllerManager;
        this.keyboardManager = keyboardManager;

        // TODO: disconnected.
        this.gameControllerManager.GameControllerConnected += OnGameControllerConnected;
        _ = this.gameControllerManager.StartDiscovery();

        gameSceneManager.LoadScene<FirstScene>(GameView);
        gameSceneManager.Start();
        
        this.keyboardManager.KeyDown += KeyboardManagerOnKeyDown;
        this.keyboardManager.KeyUp += KeyboardManagerOnKeyUp;
    }

    private void KeyboardManagerOnKeyDown(object? sender, KeyboardKey e)
    {
        if (e == KeyboardKey.KeyD)
        {
            this.playerStateManager.State |= CharacterState.MovingRight;
        }
        else if (e == KeyboardKey.KeyA)
        {
            this.playerStateManager.State |= CharacterState.MovingLeft;
        }
        else if (e == KeyboardKey.Space)
        {
            this.playerStateManager.State |= CharacterState.Jumping;
        }
        else if (e == KeyboardKey.ShiftLeft)
        {
            this.playerStateManager.State |= CharacterState.Running;
        }
    }

    private void KeyboardManagerOnKeyUp(object? sender, KeyboardKey e)
    {
        if (e == KeyboardKey.KeyD)
        {
            this.playerStateManager.State ^= CharacterState.MovingRight;
        }
        else if (e == KeyboardKey.KeyA)
        {
            this.playerStateManager.State ^= CharacterState.MovingLeft;
        }
        else if (e == KeyboardKey.Space)
        {
            this.playerStateManager.State ^= CharacterState.Jumping;
        }
        else if (e == KeyboardKey.ShiftLeft)
        {
            this.playerStateManager.State ^= CharacterState.Running;
        }
    }

    private void OnGameControllerConnected(object? sender, GameControllerConnectedEventArgs e)
    {
        if (this.gameController is not null)
        {
            return;
        }

        this.gameController = e.GameController;

        this.gameController.When(
            button: "LeftStickXAxis", // TODO: need something more concrete
            changesValue: value =>
            {
                if (Math.Abs(value) < 0.0001f)
                {
                    return;
                }

                if (value < 0.0000001f)
                {
                    this.playerStateManager.State = CharacterState.MovingLeft;
                }
                else if (value > 0.0000001f)
                {
                    this.playerStateManager.State = CharacterState.MovingRight;
                }
            });

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
        
        this.gameController.When(
            button: "ButtonWest",
            isPressed: isPressed =>
            {
                if (isPressed)
                {
                    this.playerStateManager.State |= CharacterState.Running;
                }
                else
                {
                    this.playerStateManager.State ^= CharacterState.Running;
                }
            });
        
        this.keyboardManager.When(
            key: KeyboardKey.ShiftLeft,
            isPressed: isPressed =>
            {
                if (isPressed)
                {
                    this.playerStateManager.State |= CharacterState.Running;
                }
                else
                {
                    this.playerStateManager.State ^= CharacterState.Running;
                }
            });
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
