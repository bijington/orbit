namespace Platformer;

[Flags]
public enum CharacterState
{
    Idle = 0,
    MovingRight = 1,
    MovingLeft = 2,
    Jumping = 4
}