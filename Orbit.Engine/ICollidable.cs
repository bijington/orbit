namespace Orbit.Engine;

public interface ICollidable
{
    int Damage { get; }

    void OnCollision(ICollidable collidable);

    // Is this a good fit?
    // Does everything need to give and accept damage?
}
