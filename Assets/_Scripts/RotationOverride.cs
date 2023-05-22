using UnityEngine;

public class RotationOverride : MonoBehaviour
{
    private void LateUpdate()
    {
        Camera cam = Camera.main;
        Quaternion rotation = Quaternion.LookRotation(cam.transform.forward);
        transform.rotation = rotation;
    }
}
