using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class UpgradableBuilding : InteractibleBuilding
    {
        #region Variables
        [Header("Building Upgrades")]
        [SerializeField] private BuildingUpgrade[] buildingLevels;
        public int CurrentLevel { get; protected set; }
        public BuildingUpgrade[] Upgrades => buildingLevels;
        #endregion

        #region Methods
        /// <summary>
        /// Upgrade building level
        /// </summary>
        public void UpgradeBuilding()
        {
            if (CurrentLevel >= buildingLevels.Length)
                return;

            //Not enough resources
            if (!GameManager.Instance.HasEnoughResources(buildingLevels[CurrentLevel].RequiredResources))
                return;

            //Can upgrade building
            CurrentLevel++;
            SetBuildingProperties();
        }

        /// <summary>
        /// Set building properties, called after an update
        /// </summary>
        protected virtual void SetBuildingProperties() { }
        #endregion
    }

    #region Upgrade Properties
    [System.Serializable]
    public struct BuildingUpgrade
    {
        [SerializeField] private UpgradeDatas[] resourcesCost;
        public UpgradeDatas[] RequiredResources => resourcesCost;

    }

    [System.Serializable]
    public struct UpgradeDatas
    {
        [SerializeField] private Resource resource;
        [SerializeField] private int amount;

        public Resource Resource => resource;
        public int Amount => amount;
    }
    #endregion

}

