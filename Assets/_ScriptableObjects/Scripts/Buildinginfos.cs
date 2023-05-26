using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Building;

[CreateAssetMenu(menuName = "SO/Building Infos")]
public class Buildinginfos : ScriptableObject
{
    public string name;
    public Sprite visual;
    [TextArea(2, 2)] public string description;

    public BuildingUpgrade resourcesToBuild;

}
