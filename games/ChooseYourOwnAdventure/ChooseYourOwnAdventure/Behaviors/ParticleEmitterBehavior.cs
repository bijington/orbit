using System.Windows.Input;

namespace BuildingGames.Behaviors;

public partial class ParticleEmitterBehavior : PlatformBehavior<VisualElement>
{
    public int NumberOfParticles { get; set; } = 100;
    public float LifeTime { get; set; } = 1.5f;
    public float Speed { get; set; } = 0.1f;
    public float Scale { get; set; } = 1.0f;
    public string Image { get; set; }

    public ICommand EmitCommand { get; }

    public void Emit()
    {
        if (EmitCommand is not null &&
            EmitCommand.CanExecute(null))
        {
            EmitCommand.Execute(null);
        }
    }
}