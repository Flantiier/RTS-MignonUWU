using UnityEngine;
using UnityEngine.EventSystems;
using ScriptableObjects;
using Scripts.Managers;
using TMPro;
using Scripts.Gameplay.Building;

public class BuildingDropper : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region Variables
    [Header("Dropper infos")]
    public Buildinginfos infos;

    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI descriptionField;
    [SerializeField] private Transform slotContent;
    [SerializeField] private ResourceSlot slotPrefab;

    [Header("Masking")]
    [SerializeField] private GameObject mask;

    private bool _canDrag;
    #endregion

    #region Builts_In
    private void Start()
    {
        if (!infos)
            return;

        UpdateInfos(infos);
    }

    private void OnEnable()
    {
        MaskSlot();
    }

    private void Update()
    {
        MaskSlot();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Update slot infoss
    /// </summary>
    public void UpdateInfos(Buildinginfos building)
    {
        //Set infos
        infos = building;
        nameField.text = infos.Name;
        descriptionField.text = infos.Description;

        //Set slot
        if (slotContent.childCount > 0)
            return;

        foreach (UpgradeDatas resource in infos.Resource.RequiredResources)
        {
            ResourceSlot slot = Instantiate(slotPrefab, slotContent);
            slot.resource = resource.Resource;
            slot.MaxAmount = resource.Amount;
            slot.InitializeSlot();
        }
    }

    /// <summary>
    /// Enable mask when the player doesn't have enough resources to build
    /// </summary>
    private void MaskSlot()
    {
        if (!infos)
            return;

        bool enabled = GameManager.Instance.HasEnoughResources(infos.Resource.RequiredResources);
        _canDrag = enabled;
        mask.SetActive(!enabled);
    }
    #endregion

    #region Interaces Implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_canDrag)
            return;

        BuildingManager.Instance.Dragging(true);
        BuildingManager.Instance.InstantiateBuilding(infos);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_canDrag)
            return;

        BuildingManager.Instance.Dragging(false);
        BuildingManager.Instance.PlaceBuilding();
    }
    #endregion
}
