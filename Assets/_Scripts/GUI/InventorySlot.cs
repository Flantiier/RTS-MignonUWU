using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region Variables
    public Resource resource;

    protected TextMeshProUGUI _textMesh;
    protected Image _image;
    #endregion

    #region Builts_In
    protected virtual void Awake()
    {
        _image = GetComponent<Image>();
        _textMesh = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        InitializeSlot();
    }

    protected virtual void Update()
    {
        if (!resource)
            return;

        _textMesh.text = resource.amount.ToString();
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

        _image.sprite = resource.icon;
    }
    #endregion
}
