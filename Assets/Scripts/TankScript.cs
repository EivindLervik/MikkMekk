using UnityEngine;
using System.Collections;

public class TankScript : LiquidHandeler {

    public float capacity;
    public float current;

    void Start()
    {
        current = Mathf.Clamp(current, 0.0f, capacity);
    }

    void Update()
    {

    }
}
