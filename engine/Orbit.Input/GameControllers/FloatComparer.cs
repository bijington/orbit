namespace Orbit.Input;

/// <summary>
/// A <see cref="float"/> based implementation of <see cref="IComparer{T}"/>.
/// </summary>
public class FloatComparer : IComparer<float>
{
    private readonly float threshold;
    
    internal static FloatComparer Default { get; set; } = new FloatComparer(0.001f);

    /// <summary>
    /// Creates a new instance of <see cref="FloatComparer"/>.
    /// </summary>
    /// <param name="threshold">The threshold to use when determining whether values are equal.</param>
    public FloatComparer(float threshold)
    {
        this.threshold = threshold;
    }
    
    /// <inheritdoc cref="IComparer{T}.Compare"/>
    public int Compare(float x, float y)
    {
        if (Math.Abs(x - y) < this.threshold)
        {
            return 0;
        }

        return x < y ? 1 : -1;
    }
}