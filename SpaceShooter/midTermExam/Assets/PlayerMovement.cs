using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform GunRight;
    public Transform GunLeft;
    public Transform GunMiddle;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, GunLeft.position, Quaternion.identity);
            Instantiate(bulletPrefab, GunRight.position, Quaternion.identity);
            Instantiate(bulletPrefab, GunMiddle.position, Quaternion.identity);
        }

        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        transform.position = worldPoint;

    }
}