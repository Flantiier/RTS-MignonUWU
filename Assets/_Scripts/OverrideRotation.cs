using UnityEngine;

public class OverrideRotation : MonoBehaviour
{
    [SerializeField] private Vector3 eulers;

    private void Update()
    {
        RectTransform rect = transform as RectTransform;
        rect.rotation = Quaternion.Euler(eulers);
    }
}
