using System.Runtime.CompilerServices;

namespace Orbit.Input;

public abstract class ButtonElement
{
    private readonly GameController controller;
    private readonly string elementName;

    protected ButtonElement(GameController controller, string elementName)
    {
        this.controller = controller;
        this.elementName = elementName;
    }
    
    protected void SetState(ref bool field, bool newValue, [CallerMemberName] string? buttonName = null)
    {
        ArgumentNullException.ThrowIfNull(buttonName);

        field = newValue;
        
        this.controller.RaiseButtonPressed(elementName + buttonName, field);
    }
    
    protected void SetValue(ref float field, float newValue, [CallerMemberName] string? buttonName = null)
    {
        ArgumentNullException.ThrowIfNull(buttonName);
        
        field = newValue;
        
        this.controller.RaiseButtonValueChanged(elementName + buttonName, field);
    }
}
