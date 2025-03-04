namespace Orbit.Input;

/// <summary>
/// Represents a game controller button and its associated value.
/// </summary>
public abstract class ButtonValue
{
    /// <summary>
    /// Creates a new instance of <see cref="ButtonValue"/>.
    /// </summary>
    /// <param name="parent">The name of the component of a game controller that this button belongs to.</param>
    /// <param name="name">The name of the button.</param>
    protected ButtonValue(string parent, string name)
    {
        Name = NameHelper.GetName(parent, name);
    }
    
    /// <summary>
    /// Gets the name of the button.
    /// </summary>
    public string Name { get; }
}

/// <inheritdoc />
public class ButtonValue<TValue> : ButtonValue where TValue : struct
{
    private readonly GameController gameController;
    private readonly IComparer<TValue> comparer;
    private TValue buttonValue;

    /// <summary>
    /// Creates a new instance of <see cref="ButtonValue"/>.
    /// </summary>
    /// <param name="gameController">The <see cref="GameController"/> that this button belongs to.</param>
    /// <param name="parent">The name of the component of a game controller that this button belongs to.</param>
    /// <param name="name">The name of the button.</param>
    /// <param name="comparer">The <see cref="IComparer{T}"/> to use in comparisons for value changed events. Particularly useful when dealing with values like <see cref="float"/> where accuracy can be messy.</param>
    public ButtonValue(GameController gameController, string parent, string name, IComparer<TValue>? comparer = null)
        : base(parent, name)
    {
        this.gameController = gameController;
        this.comparer = comparer ?? Comparer<TValue>.Default;
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="ButtonValue"/>.
    /// </summary>
    /// <param name="gameController">The <see cref="GameController"/> that this button belongs to.</param>
    /// <param name="name">The name of the button.</param>
    /// <param name="comparer">The <see cref="IComparer{T}"/> to use in comparisons for value changed events. Particularly useful when dealing with values like <see cref="float"/> where accuracy can be messy.</param>
    public ButtonValue(GameController gameController, string name, IComparer<TValue>? comparer = null)
        : base(string.Empty, name)
    {
        this.gameController = gameController;
        this.comparer = comparer ?? Comparer<TValue>.Default;
    }

    /// <summary>
    /// Gets the current value for the button. 
    /// </summary>
    public TValue Value
    {
        get => buttonValue;
        internal set
        {
            if (comparer.Compare(buttonValue, value) == 0)
            {
                return;
            }

            buttonValue = value;

            this.gameController.RaiseButtonValueChanged(this);
        }
    }
}