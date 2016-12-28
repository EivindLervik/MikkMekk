using UnityEngine;
using System.Collections;

public enum PumpDirection
{
    Forward, Backward
}

public class PumpScript : LiquidHandeler {
    [Header("PUMP")]
    public PumpDirection direction;
    public bool on;

    public PipeDirection next;
    public PipeDirection prev;

    public float pressureDelivery;

    private bool canStart = true;

    public override void Setup()
    {
        base.Setup();
        on = true;
    }

    public override bool CanPlace()
    {
        bool placeDown = base.CanPlace();
        if (placeDown)
        {
            return CheckOutputs(true);
        }

        return false;
    }

    void Update () {
        if (on && canStart)
        {
            if (direction == PumpDirection.Forward)
            {
                next.UpdatePressure(pressureDelivery);
                prev.UpdatePressure(-pressureDelivery);
            }

            canStart = false;
        }
    }
}
