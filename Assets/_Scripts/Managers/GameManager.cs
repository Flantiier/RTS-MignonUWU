using ScriptableObjects;
using Scripts.Gameplay.Building;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;

    [Header("Game datas")]
    [SerializeField] private UnitProperties[] units;
    #endregion

    #region Properties
    public static GameManager Instance { get; private set; }
    public Inventory Inventory => inventory;
    #endregion

    #region Builts_In
    private void Awake()
    {
        SingletonPattern();
        ResetUnitsProperties();
        InitializeInventory();
    }
    #endregion

    #region Design Patterns
    private void SingletonPattern()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("A duplicated singleton has been destroyed.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    #region GameData Methods
    private void ResetUnitsProperties()
    {
        if (units.Length <= 0)
            return;

        foreach (UnitProperties unit in units)
        {
            if (!unit)
                continue;

            unit.ResetDatas();
        }
    }
    #endregion

    #region Inventory Methods
    /// <summary>
    /// Initialize inventory GUI
    /// </summary>
    private void InitializeInventory()
    {
        foreach (Resource item in inventory.resources)
        {
            item.amount = item.baseAmount;
            InventorySlot slot = Instantiate(slotPrefab, inventoryPanel);
            slot.resource = item;
            slot.InitializeSlot();
        }
    }

    /// <summary>
    /// Check if the player has the enough resources to upgrade
    /// </summary>
    public bool HasEnoughResources(UpgradeDatas[] datas)
    {
        foreach (UpgradeDatas data in datas)
        {
            if (data.Resource.amount >= data.Amount)
                continue;

            return false;
        }

        return true;
    }

    /// <summary>
    /// Remove resources from the inventory
    /// </summary>
    public void UseResource(Resource resource, int amount)
    {
        resource.amount -= amount;
        resource.amount = (int)Mathf.Clamp(resource.amount, 0, Mathf.Infinity);
    }
    #endregion
}
