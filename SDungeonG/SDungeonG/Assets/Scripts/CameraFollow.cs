using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}