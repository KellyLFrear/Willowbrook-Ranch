using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 5f, -8f);
    public float smoothSpeed = 5f;
    public bool lookAtPlayer = true;

    void LateUpdate()
    {
        if (player == null) return;

        // Desired position behind the player
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);
        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Optionally, make the camera look at the player
        if (lookAtPlayer)
            transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
