using UnityEngine;
using System.Collections;

public class LiquidHandeler : Placeable {

    public PipeDirection[] outputs;

    void Start () {
	    
	}

	void Update () {
	
	}

    public override bool CanPlace()
    {
        return base.CanPlace();
    }

    public bool HasRightDirection(PipeDirection pIn)
    {
        foreach (PipeDirection p in outputs)
        {
            if(p.transform.forward == -pIn.transform.forward)
            {
                return true;
            }
        }

        return false;
    }
}
