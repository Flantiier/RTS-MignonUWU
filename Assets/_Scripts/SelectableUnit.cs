using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    public virtual void OnSelected()
    {
        Debug.Log($"Select {gameObject}");
    }

    public virtual void OnDeselected()
    {
        Debug.Log($"Deselect{gameObject}");
    }
}
