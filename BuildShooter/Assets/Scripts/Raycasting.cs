using UnityEngine;

public class Raycasting : MonoBehaviour
{
    private Camera cam;

    [HideInInspector] public static Ray ray;
    [HideInInspector] public static RaycastHit hit;

    [HideInInspector] static public Vector3 pointOfContact;


    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray();
        DrawRaycast();  
    }

    private void DrawRaycast()
    {
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
    }

    private void Ray()
    {
        ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        pointOfContact = hit.point;
    }
}