using UnityEngine;
using System.Collections;

public class PipeScript : LiquidHandeler {
	void Start () {
	    
	}

	void Update () {

    }

    public override bool CanPlace()
    {
        foreach (PipeDirection p in outputs)
        {
            Ray ray = new Ray(p.transform.position, -p.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1.0f, placementMask))
            {
                if (hit.transform.GetComponent<LiquidHandeler>().HasRightDirection(p))
                {
                    return true;
                }
            }
        }

        return false;
    }  
}
