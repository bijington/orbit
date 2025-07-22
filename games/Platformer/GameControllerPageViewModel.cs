using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Orbit.Input;

namespace Platformer;

public class GameControllerPageViewModel : INotifyPropertyChanged
{
    private Orbit.Input.GameController? selectedGameController;
    private readonly Orbit.Input.GameControllerManager gameControllerManager;

    public ObservableCollection<Orbit.Input.GameController> GameControllers { get; } = [];
    public ObservableCollection<ChangeViewModel> Changes { get; } = [];
    
    public Orbit.Input.GameController? SelectedGameController
    {
        get => selectedGameController;
        set
        {
            if (selectedGameController is not null)
            {
                selectedGameController.ButtonChanged -= SelectedGameControllerOnButtonChanged;
                selectedGameController.ValueChanged -= SelectedGameControllerOnValueChanged;
            }
            
            if (SetField(ref selectedGameController, value))
            {
                if (selectedGameController is not null)
                {
                    AddChange($"Connected to controller '{selectedGameController.Name}'");
                    selectedGameController.ButtonChanged += SelectedGameControllerOnButtonChanged;
                    selectedGameController.ValueChanged += SelectedGameControllerOnValueChanged;
                }
            }
        }
    }

    private void SelectedGameControllerOnValueChanged(object? sender, GameControllerValueChangedEventArgs e)
    {
        AddChange($"'{e.ButtonName}' button changed to {e.Value}");
    }

    private void SelectedGameControllerOnButtonChanged(object? sender, GameControllerButtonChangedEventArgs e)
    {
        AddChange($"'{e.ButtonName}' was {(e.IsPressed ? "Pressed" : "Released")}");
    }

    private void AddChange(string description)
    {
        Changes.Add(new ChangeViewModel($"{DateTime.Now:O} - {description}"));
    }

    public ICommand RefreshCommand { get; }
    
    public GameControllerPageViewModel(Orbit.Input.GameControllerManager gameControllerManager)
    {
        this.gameControllerManager = gameControllerManager;
        
        this.gameControllerManager.GameControllerConnected += GameControllerManagerOnGameControllerConnected;
        
        RefreshCommand = new Command(OnRefresh);
    }

    private void GameControllerManagerOnGameControllerConnected(object? sender, GameControllerConnectedEventArgs e)
    {
        GameControllers.Add(e.GameController);
    }

    private void OnRefresh()
    {
        try
        {
            GameControllers.Clear();
        
            _ = this.gameControllerManager.StartDiscovery();

            foreach (var gameController in gameControllerManager.GameControllers)
            {
                GameControllers.Add(gameController);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}