using Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingDropper : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public string building;

    public void OnBeginDrag(PointerEventData eventData)
    {
        BuildingManager.Instance.Dragging(true);
        BuildingManager.Instance.InstantiateBuilding(building);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BuildingManager.Instance.Dragging(false);
    }
}
