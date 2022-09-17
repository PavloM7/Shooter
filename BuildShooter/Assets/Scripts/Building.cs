using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    private float maxDistance = 5;
    private float cubeOffset = 0.5f;

    private Vector3 buildingPointForCubes;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Raycasting.ray, maxDistance))
        {
            ClickBuild();
        }
    }

    private void ClickBuild()
    {
        if (Raycasting.hit.collider.gameObject.CompareTag("Floor"))
        {
            BuildOnFloor();
        }
        else if (Raycasting.hit.collider.gameObject.CompareTag("Cube"))
        {

            BuildOnCubes();
        }

        void BuildOnFloor()
        {
            Instantiate(cube, Raycasting.pointOfContact + new Vector3(0, cubeOffset, 0), Quaternion.identity);

        }

        void BuildOnCubes()
        {
            buildingPointForCubes = Raycasting.hit.point;

            float offsetX = Raycasting.hit.collider.transform.position.x - buildingPointForCubes.x;
            float offsetY = Raycasting.hit.collider.transform.position.y - buildingPointForCubes.y;
            float offsetZ = Raycasting.hit.collider.transform.position.z - buildingPointForCubes.z;

            if (offsetX >= 0.49f && offsetX <= 0.51f || offsetX >= -0.51f && offsetX <= -0.49f)
            {
                Instantiate(cube, new Vector3(buildingPointForCubes.x - offsetX, Raycasting.hit.collider.transform.position.y, Raycasting.hit.collider.transform.position.z), Quaternion.identity);
            }
            else if (offsetY >= 0.49f && offsetY <= 0.51f || offsetY >= -0.51f && offsetY <= -0.49f)
            {
                Instantiate(cube, new Vector3(Raycasting.hit.collider.transform.position.x, buildingPointForCubes.y - offsetY, Raycasting.hit.collider.transform.position.z), Quaternion.identity);
            }
            else if (offsetZ >= 0.49f && offsetZ <= 0.51f || offsetZ >= -0.51f && offsetZ <= -0.49f)
            {
                Instantiate(cube, new Vector3(Raycasting.hit.collider.transform.position.x, Raycasting.hit.collider.transform.position.y, buildingPointForCubes.z - offsetZ), Quaternion.identity);
            }
        }
    }
}