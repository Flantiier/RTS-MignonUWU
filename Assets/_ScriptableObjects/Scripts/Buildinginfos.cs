using UnityEngine;
using Scripts.Gameplay.Building;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Building Infos")]
    public class Buildinginfos : ScriptableObject
    {
        [SerializeField] private InteractibleBuilding prefab;
        [SerializeField] private string buildingName;
        [SerializeField] private Sprite sprite;
        [SerializeField, TextArea(2, 2)] private string description;
        [SerializeField] private BuildingUpgrade resourcesToBuild;

        public InteractibleBuilding Prefab => prefab;
        public string Name => buildingName;
        public string Description => description;
        public Sprite Sprite => sprite;
        public BuildingUpgrade Resource => resourcesToBuild;
    }
}
