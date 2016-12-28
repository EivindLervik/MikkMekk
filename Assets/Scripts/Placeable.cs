using UnityEngine;
using System.Collections;

public class Placeable : MonoBehaviour {
    [Header("Placement")]
    public bool mustBeGrounded;
    public LayerMask placementMask;

    [Header("Rotation")]
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;

    public virtual void Setup()
    {
        // OVERRIDE
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

    public void Rotate(Vector3 rotation)
    {
        if (rotateX)
        {
            transform.Rotate(transform.right, Mathf.Round(rotation.x) * 90.0f);
        }
        if (rotateY)
        {
            transform.Rotate(transform.up, Mathf.Round(rotation.y) * 90.0f);
        }
        if (rotateZ)
        {
            transform.Rotate(transform.forward, Mathf.Round(rotation.z) * 90.0f);
        }

        // Repair
        //transform.eulerAngles = new Vector3(Mathf.Round(transform.eulerAngles.x), Mathf.Round(transform.eulerAngles.y), Mathf.Round(transform.eulerAngles.z));
    }
}
