namespace Orbit.Input;

/// <summary>
/// Defines the game controller specific options used to override specific functionality.
/// </summary>
public partial class GameControllerOptions
{
    /// <summary>
    /// Gets the threshold to use when comparing floats together in order to handle the inaccuracies that come with floats.
    /// </summary>
    public float ComparisonThreshold { get; set; } = 0.001f;
}