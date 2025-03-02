namespace Orbit.Input;

public class ButtonValue
{
    public ButtonValue(string parent, string name)
    {
        Name = NameHelper.GetName(parent, name);
    }
    
    public string Name { get; }
}

public class ButtonValue<TValue> : ButtonValue where TValue : struct
{
    private readonly GameController gameController;
    private readonly Comparer<TValue> comparer;
    private TValue buttonValue;
    
    public ButtonValue(GameController gameController, string parent, string name, Comparer<TValue>? comparer = null)
        : base(parent, name)
    {
        this.gameController = gameController;
        this.comparer = comparer ?? Comparer<TValue>.Default;
    }
    
    public ButtonValue(GameController gameController, string name, Comparer<TValue>? comparer = null)
        : base(string.Empty, name)
    {
        this.gameController = gameController;
        this.comparer = comparer ?? Comparer<TValue>.Default;
    }

    public TValue Value
    {
        get => buttonValue;
        set
        {
            if (comparer.Compare(buttonValue, value) != 0)
            {
                buttonValue = value;

                this.gameController.RaiseButtonValueChanged(this);
            }
        }
    }
}