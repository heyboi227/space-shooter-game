using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = LaserConfig.speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(laserSpeed * Time.deltaTime * Vector3.up);

        if (transform.position.y >= LaserConfig.distanceLimit)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
