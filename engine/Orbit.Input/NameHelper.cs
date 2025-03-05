namespace Orbit.Input;

internal static class NameHelper
{
    internal static string GetName(string parent, string child)
    {
        if (string.IsNullOrEmpty(parent))
        {
            return child;
        }
        
        return parent + "." + child;
    }
}
