using UnityEngine;
using System.Collections;

public class Placeable : MonoBehaviour {

    public bool mustBeGrounded;
    public LayerMask placementMask;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual bool CanPlace()
    {
        if (mustBeGrounded)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1.0f, placementMask))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
