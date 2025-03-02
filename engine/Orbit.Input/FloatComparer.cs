namespace Orbit.Input;

public class FloatComparer : IComparer<float>
{
    private readonly float threshold;
    
    internal static FloatComparer Default { get; } = new FloatComparer(0.001f);

    public FloatComparer(float threshold)
    {
        this.threshold = threshold;
    }
    
    public int Compare(float x, float y)
    {
        if (Math.Abs(x - y) < this.threshold)
        {
            return 0;
        }

        return x < y ? 1 : -1;
    }
}