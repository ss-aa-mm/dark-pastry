using UnityEngine;

public class NewEgg : NewEnemy
{
    private int _frames;
    private int _xAxis;
    private int _yAxis;
    protected override void AssignReferences()
    {
        DamageInflicted = 0.5f;
        MovementTime = 0.5f;
        Unit = 0.25f;
        ItemDropped = Resources.Load<GameObject>("Prefabs/DroppedEgg");
    }

    protected override void MovementPattern()
    {
        Random();
    }
}