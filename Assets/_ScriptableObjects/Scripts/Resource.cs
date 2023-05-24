using UnityEngine;

[CreateAssetMenu(menuName = "SO/Resource")]
public class Resource : ScriptableObject
{
    public Sprite icon;
    public int amount = 500;
    public int baseAmount = 0;
}
