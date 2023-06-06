using UnityEngine;
using Scripts.Gameplay.Building;
using Scripts.Gameplay.Units;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Unit Properties")]
    public class UnitProperties : ScriptableObject
    {
        #region Variables
        [SerializeField] private SelectableUnit prefab;
        [SerializeField] private string unitName;
        [SerializeField] private Sprite unitSprite;
        [SerializeField] private UnitDatas[] properties;
        [SerializeField] private ResourceUpgrade[] resourcesToUpgrade;
        #endregion

        #region Properties
        public SelectableUnit Prefab => prefab;
        public string Name => unitName;
        public Sprite Sprite => unitSprite;
        public UnitDatas[] Properties => properties;
        public int CurrentLevel { get; private set; }
        #endregion

        #region Builts_In
        private void Awake()
        {
            CurrentLevel = 0;
        }
        #endregion

        #region Methods
        public float GetDamagesValue()
        {
            return properties[CurrentLevel].damage;
        }

        public void IncreaseLevel()
        {
            if (CurrentLevel >= resourcesToUpgrade.Length)
                return;

            if (!GameManager.Instance.HasEnoughResources(resourcesToUpgrade[CurrentLevel].RequiredResources))
                return;

            CurrentLevel++;
            foreach (UpgradeDatas resource in resourcesToUpgrade[CurrentLevel].RequiredResources)
                resource.Resource.amount -= resource.Amount;
        }
        #endregion
    }

    #region UnitDatas class
    [System.Serializable]
    public class UnitDatas
    {
        public float damage = 25;
    }
    #endregion
}
