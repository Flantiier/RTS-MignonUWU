using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public Resource resource;
    private TextMeshProUGUI _textMesh;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _textMesh = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        InitializeSlot();
    }

    private void Update()
    {
        if (!resource)
            return;

        _textMesh.text = resource.amount.ToString();
    }

    public void InitializeSlot()
    {
        if (!resource || !resource.icon)
            return;

        _image.sprite = resource.icon;
    }
}
