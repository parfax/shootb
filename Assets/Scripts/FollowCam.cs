using UnityEngine;
using System.Collections;
public class FollowCam : MonoBehaviour
{
    static public FollowCam S;
    public GameObject poi;
    public float camZ, easing = 0.05f;
    Vector2 minXY;
    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }
    void FixedUpdate()
    {
        Vector3 destination;
        // If there is no poi, return to P:[0,0,0]
        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Get the position of the poi
            destination = poi.transform.position;
            // If poi is a Projectile, check to see if it's at rest
            if (poi.tag == "Projectile")
            {
                // if it is sleeping (that is, not moving)
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    poi = null;
                    // in the next update
                    return;
                }
            }
        }
        if (poi == null) return;
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}
