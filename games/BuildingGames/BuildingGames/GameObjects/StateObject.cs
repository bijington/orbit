using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class StateObject : GameObject
{
    private const double flowRateMillisecondsToIncrease = 5000;
    private const float flowRateIncrease = 2f;
    private double flowRateCurrentMilliseconds = 0;

    private const double distanceMillisecondsToIncrease = 1000;
    private double distanceCurrentMilliseconds = 0;

    public float CurrentFlowRate { get; private set; } = 1f;

    public float DistanceTraveled { get; private set; }

    public StateObject()
    {

    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        //if (flowRateCurrentMilliseconds > flowRateMillisecondsToIncrease)
        //{
        //    CurrentFlowRate += flowRateIncrease;

        //    flowRateCurrentMilliseconds = 0;
        //}
        //else
        //{
        //    flowRateCurrentMilliseconds += millisecondsSinceLastUpdate;
        //}

        if (distanceCurrentMilliseconds > distanceMillisecondsToIncrease)
        {
            DistanceTraveled += CurrentFlowRate;

            distanceCurrentMilliseconds = 0;
        }
        else
        {
            distanceCurrentMilliseconds += millisecondsSinceLastUpdate;
        }
    }
}
