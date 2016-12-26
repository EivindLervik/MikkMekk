using UnityEngine;
using System.Collections;

public enum PipeType
{
    Straight, Turn, Tri, Quad, Corner, OpenCorner, FullNormal, Star
}

public class LiquidHandeler : Placeable {

    public PipeType pipeType;

    void Start () {
	    
	}

	void Update () {
	
	}

    public override bool CanPlace()
    {
        return false;
    }
}
