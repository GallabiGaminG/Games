using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
            transform.LookAt(target);
    }
}