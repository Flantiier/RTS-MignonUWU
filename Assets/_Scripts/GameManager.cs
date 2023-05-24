using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private ResourceDisplay slotPrefab;
    #endregion

    #region Properties
    public static GameManager Instance { get; private set; }
    public Inventory Inventory => inventory;
    #endregion

    #region Builts_In
    private void Awake()
    {
        SingletonPattern();
        InitializeInventory();
    }
    #endregion

    #region Design Patterns
    private void SingletonPattern()
    {
        if (Instance != null || Instance != this)
        {
            Debug.LogWarning("A duplicated singleton has been destroyed.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    #region Inventory Methods
    /// <summary>
    /// Initialize inventory GUI
    /// </summary>
    private void InitializeInventory()
    {
        foreach (Resource item in inventory.ressources)
        {
            item.amount = item.baseAmount;
            ResourceDisplay slot = Instantiate(slotPrefab, inventoryPanel);
            slot.resource = item;
            slot.InitializeSlot();
        }
    }
    #endregion
}
