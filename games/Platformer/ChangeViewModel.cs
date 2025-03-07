namespace Platformer;

public class ChangeViewModel
{
    public ChangeViewModel(string description)
    {
        Description = description;
    }
    
    public string Description { get; }
}