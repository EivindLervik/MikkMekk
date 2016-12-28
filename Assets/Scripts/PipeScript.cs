using UnityEngine;
using System.Collections;

public class PipeScript : LiquidHandeler {

    public override bool CanPlace()
    {
        bool placeDown = base.CanPlace();
        bool utPuts = CheckOutputs(false);

        return placeDown && utPuts;
    }
}
