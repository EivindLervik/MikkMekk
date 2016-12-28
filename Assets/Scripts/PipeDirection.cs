using UnityEngine;
using System.Collections;

public class PipeDirection : MonoBehaviour {

    public bool inUse;

    private LiquidHandeler myPipe;
    private PipeDirection myNext;

    void Start()
    {
        myPipe = GetComponentInParent<LiquidHandeler>();
    }

    public void SetNext(PipeDirection pd)
    {
        myNext = pd;
    }

    public void UpdatePressure(float pressure)
    {
        myPipe.UpdatePressure(pressure * 0.9f);
        if(myNext != null)
        {
            print(transform.name);
            myNext.UpdatePressure(pressure * 0.9f);
        }
    }
}
