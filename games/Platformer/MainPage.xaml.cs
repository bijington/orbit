﻿using System.Diagnostics;

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

    private void KeyboardManagerOnKeyDown(object? sender, KeyboardKeyChangeEventArgs e)
    {
        if (e.Key == KeyboardKey.KeyD)
        {
            this.playerStateManager.State |= CharacterState.MovingRight;
        }
        else if (e.Key == KeyboardKey.KeyA)
        {
            this.playerStateManager.State |= CharacterState.MovingLeft;
        }
        else if (e.Key == KeyboardKey.Space)
        {
            this.playerStateManager.State |= CharacterState.Jumping;
        }
        else if (e.Key == KeyboardKey.ShiftLeft)
        {
            this.playerStateManager.State |= CharacterState.Running;
        }
    }

    private void KeyboardManagerOnKeyUp(object? sender, KeyboardKeyChangeEventArgs e)
    {
        if (e.Key == KeyboardKey.KeyD)
        {
            this.playerStateManager.State ^= CharacterState.MovingRight;
        }
        else if (e.Key == KeyboardKey.KeyA)
        {
            this.playerStateManager.State ^= CharacterState.MovingLeft;
        }
        else if (e.Key == KeyboardKey.Space)
        {
            this.playerStateManager.State ^= CharacterState.Jumping;
        }
        else if (e.Key == KeyboardKey.ShiftLeft)
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
        
        this.gameController.ButtonChanged += GameControllerOnButtonChanged;
        this.gameController.ValueChanged += GameControllerOnValueChanged;
    }

    private void GameControllerOnValueChanged(object? sender, GameControllerValueChangedEventArgs e)
    {
        if (gameController is null)
        {
            return;
        }
        
        if (e.ButtonName == gameController.LeftStick.XAxis.Name)
        {
            if (e.Value == 0)
            {
                this.playerStateManager.State = CharacterState.Idle;
            }
            else if (e.Value < -0.001f)
            {
                this.playerStateManager.State = CharacterState.MovingLeft;
            }
            else if (e.Value > 0.001f)
            {
                this.playerStateManager.State = CharacterState.MovingRight;
            }
            else
            {
                this.playerStateManager.State = CharacterState.Idle;
            }

            Debug.WriteLine($"e.Value = {e.Value} {this.playerStateManager.State}");
        }
    }

    private void GameControllerOnButtonChanged(object? sender, GameControllerButtonChangedEventArgs e)
    {
        if (gameController is null)
        {
            return;
        }
        
        if (e.ButtonName == gameController.South.Name)
        {
            if (e.IsPressed)
            {
                this.playerStateManager.State |= CharacterState.Jumping;
            }
            else
            {
                this.playerStateManager.State ^= CharacterState.Jumping;
            }
        }
        else if (e.ButtonName == gameController.West.Name)
        {
            if (e.IsPressed)
            {
                this.playerStateManager.State |= CharacterState.Running;
            }
            else
            {
                this.playerStateManager.State ^= CharacterState.Running;
            }
        }
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

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameControllerPage));
    }
}
