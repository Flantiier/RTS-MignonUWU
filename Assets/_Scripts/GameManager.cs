using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Inventory inventory;
    #endregion

    #region Properties
    public static GameManager Instance { get; private set; }
    public Inventory Inventory => inventory;
    #endregion

    #region Builts_In
    private void Awake()
    {
        SingletonPattern();
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

    #region Methods

    #endregion
}
