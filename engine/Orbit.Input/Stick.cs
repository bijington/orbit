namespace Orbit.Input;

public class Stick : ButtonElement
{
    public Stick(GameController controller, string name) : base(controller, name)
    {
    }

    private float xAxis;

    public float XAxis
    {
        get => xAxis;
        set => SetValue(ref this.xAxis, value);
    }
    
    private float yAxis;

    public float YAxis
    {
        get => yAxis;
        set => SetValue(ref this.yAxis, value);
    }
}
