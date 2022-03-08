
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public Transform slugBoy;
    public Vector3 offset;
    public float smoothness;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = slugBoy.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
