using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class UpgradableBuilding : InteractibleBuilding
    {
        #region Variables
        [Header("Building Upgrades")]
        [SerializeField] private UpgradeProperties[] buildingLevels;
        public int CurrentLevel { get; protected set; }
        #endregion

        #region Methods
        /// <summary>
        /// Upgrade building level
        /// </summary>
        public void UpgradeBuilding()
        {
            if (buildingLevels.Length <= 0 || CurrentLevel >= buildingLevels.Length)
                return;

            //Not enough resources
            if (HasResourcesToUpgrade(buildingLevels[CurrentLevel].RequiredResources))
                return;

            //Can upgrade building
            CurrentLevel++;
            SetBuildingProperties();
        }

        /// <summary>
        /// Set building properties, called after an update
        /// </summary>
        protected virtual void SetBuildingProperties() { }

        /// <summary>
        /// Check if the player has the enough resources to upgrade
        /// </summary>
        private bool HasResourcesToUpgrade(UpgradeDatas[] datas)
        {
            foreach (UpgradeDatas data in datas)
            {
                if (data.Resource.amount >= data.Amount)
                    continue;
                else
                    return false;
            }

            return true;
        }
        #endregion
    }

    #region Upgrade Properties
    [System.Serializable]
    public struct UpgradeProperties
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

