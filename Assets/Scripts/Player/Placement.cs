using UnityEngine;
using System.Collections;

public class Placement : MonoBehaviour {

    public GameObject[] placeables;
    public LayerMask placementMask;
    public Material canPlaceMAT;
    public Material canNotPlaceMAT;

    private int currentlySelected;
    private GameObject placingNow;
    private Placeable placingNowScript;
    private Material placingNowMAT;
    private int placingNowLayer;
    private bool active;
    private Camera cam;
    private bool canPlace;

    private float placeDist = 4.0f;

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 placePos = new Vector3();
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);

            placePos = cam.transform.position + (ray.direction * placeDist);

            // Fix position to grid
            placePos.x = Mathf.Floor(placePos.x) + 0.5f;
            placePos.y = Mathf.Floor(placePos.y) + 0.5f;
            placePos.z = Mathf.Floor(placePos.z) + 0.5f;

            if (placingNow == null)
            {
                placingNow = (GameObject) Instantiate(placeables[currentlySelected], transform.position, Quaternion.identity);
                placingNowScript = placingNow.GetComponent<Placeable>();
                placingNowMAT = placingNow.GetComponentInChildren<Renderer>().material;
                placingNowLayer = placingNow.layer;
                placingNow.layer = 2;
            }

            // Move
            if(placingNow.transform.position != placePos)
            {
                placingNow.transform.position = placePos;

                // Can the object be placed based on own rules?
                if (placingNowScript.CanPlace())
                {
                    placingNow.GetComponentInChildren<Renderer>().material = canPlaceMAT;
                    canPlace = true;
                }
                else
                {
                    placingNow.GetComponentInChildren<Renderer>().material = canNotPlaceMAT;
                    canPlace = false;
                }

                // Is inside something?
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Vector3.Distance(cam.transform.position, placingNow.transform.position), placementMask))
                {
                    if(Vector3.Distance(hit.transform.position, placingNow.transform.position) < 0.5f)
                    {
                        placingNow.GetComponentInChildren<Renderer>().material = canNotPlaceMAT;
                        canPlace = false;
                    }
                }
            }
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetCam(Camera cam)
    {
        this.cam = cam;
    }

    public void UpdatePlacementDistance(float delta)
    {
        placeDist += delta * 3.0f;
    }

    public void Place()
    {
        if (active)
        {
            if (canPlace)
            {
                placingNow.GetComponentInChildren<Renderer>().material = placingNowMAT;
                placingNow.layer = placingNowLayer;
                placingNow = null;
            }
        }
    }

    public void ChangeObject(int way)
    {
        currentlySelected= Mathf.Clamp(currentlySelected + way, 0, placeables.Length-1);
        Destroy(placingNow);
        print(currentlySelected);
    }
}
