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
        [SerializeField] private UpgradeDatas[] resourcesToUpgrade;
        #endregion

        #region Properties
        public SelectableUnit Prefab => prefab;
        public string Name => unitName;
        public Sprite Sprite => unitSprite;
        public UnitDatas[] Properties => properties;
        public UpgradeDatas[] Upgrades => resourcesToUpgrade;
        public int CurrentLevel { get; private set; }
        public int MaxLevel => resourcesToUpgrade.Length;
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

            UpgradeDatas data = resourcesToUpgrade[CurrentLevel];
            if (data.Resource.amount < data.Amount)
                return;

            data.Resource.amount -= data.Amount;
            data.Resource.amount = (int)Mathf.Clamp(data.Resource.amount, 0, Mathf.Infinity);
            CurrentLevel++;
        }

        public void ResetDatas()
        {
            CurrentLevel = 0;
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
