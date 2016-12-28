using UnityEngine;
using System.Collections;

public class LiquidHandeler : Placeable {

    public float pressure;

    public PipeDirection[] outputs;

    public override void Setup()
    {
        ConnectAll();
    }

    public override bool CanPlace()
    {
        return base.CanPlace();
    }

    public bool CheckOutputs(bool initial)
    {
        bool ok = initial;

        foreach (PipeDirection p in outputs)
        {
            if (p.inUse)
            {
                Ray ray = new Ray(p.transform.position, p.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1.0f, placementMask))
                {
                    if (hit.transform.GetComponent<LiquidHandeler>() != null)
                    {
                        if (hit.transform.GetComponent<LiquidHandeler>().HasRightDirection(p))
                        {
                            print("HAR");
                            if (p.inUse)
                            {
                                ok = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (p.inUse)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return ok;
    }

    public bool HasRightDirection(PipeDirection pIn)
    {
        foreach (PipeDirection p in outputs)
        {
            if (Vector3.Dot(p.transform.forward, pIn.transform.forward) < -0.5f && p.inUse)
            {
                return true;
            }
        }

        return false;
    }

    public PipeDirection UpdateRightDirection(PipeDirection pIn)
    {
        foreach (PipeDirection p in outputs)
        {
            if (Vector3.Dot(p.transform.forward, pIn.transform.forward) < -0.5f && p.inUse)
            {
                p.SetNext(pIn);
                return p;
            }
        }

        return null;
    }

    public void ConnectAll()
    {
        foreach (PipeDirection pd in outputs)
        {
            if (pd.inUse)
            {
                Ray ray = new Ray(pd.transform.position, pd.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1.0f, placementMask))
                {
                    PipeDirection p = hit.transform.GetComponent<LiquidHandeler>().UpdateRightDirection(pd);
                    pd.SetNext(p);

                    print(p + " " + pd);
                    //pd.UpdatePressure(pressure);
                }
            }
        }
    }

    public void UpdatePressure(float pressure)
    {
        this.pressure = pressure;
    }
}
