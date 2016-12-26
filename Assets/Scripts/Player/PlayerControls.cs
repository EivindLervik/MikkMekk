using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    // Inputs
    public string sidewaysAxis;
    public string frontbackAxis;
    public string jump;
    public string place;

    // Mouse
    public string mouseX;
    public float mouseXSensitivity;
    public string mouseY;
    public float mouseYSensitivity;
    public string scroll;

    private PlayerMovement pm;
    private Transform cam;
    private Placement p;

	void Start () {
        pm = GetComponent<PlayerMovement>();
        cam = GetComponentInChildren<Camera>().transform;

        p = GetComponent<Placement>();
        p.SetCam(cam.GetComponent<Camera>());
	}

	void Update () {
        // Movement
        Vector3 inputVector = new Vector3(Input.GetAxis(sidewaysAxis), Input.GetAxis(jump), Input.GetAxis(frontbackAxis));
        pm.UpdateInputBuffer(inputVector);

        // Camera movement
        pm.Rotate(Input.GetAxis(mouseX) * mouseXSensitivity);
        cam.Rotate(Vector3.right, -Input.GetAxis(mouseY) * mouseYSensitivity);

        // Placement
        if (Input.GetKeyDown(KeyCode.P))
        {
            p.Activate();
        }
        if (Input.GetAxis(scroll) != 0.0f)
        {
            p.UpdatePlacementDistance(Input.GetAxis(scroll));
        }
        if (Input.GetButtonDown(place))
        {
            p.Place();
        }

        // Change object
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            p.ChangeObject(-1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            p.ChangeObject(1);
        }
    }
}
