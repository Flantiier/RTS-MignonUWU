using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region Variables
    public Resource resource;
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI textField;
    #endregion

    #region Builts_In
    protected virtual void Awake()
    {
        InitializeSlot();
    }

    protected virtual void Update()
    {
        if (!resource)
            return;

        textField.text = resource.amount.ToString();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Set slot icon
    /// </summary>
    public void InitializeSlot()
    {
        if (!resource || !resource.icon)
            return;

        image.sprite = resource.icon;
    }
    #endregion
}
