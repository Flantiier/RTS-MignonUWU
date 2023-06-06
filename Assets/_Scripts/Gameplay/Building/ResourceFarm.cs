using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class ResourceFarm : UpgradableBuilding
    {
        #region Variables
        [Header("Farm properties")]
        [SerializeField] private Resource farmResource;
        [SerializeField] private Resource requiredResource;
        [SerializeField] private int requiredWorkers = 1;
        [Tooltip("This array size should match the amount of upgrades.")]
        [SerializeField] private FarmDatas[] properties  = new FarmDatas[1];

        private FarmDatas _currentDatas;
        private FarmGUI _gui;
        private int _workersAmount = 0;
        private bool _farmRunning;
        #endregion

        #region Properties
        public Resource FarmResource => farmResource;
        public Resource Resource => requiredResource;
        public FarmDatas[] Properties => properties;
        #endregion

        #region Builts_In
        private void Awake()
        {
            _gui = GetComponentInChildren<FarmGUI>();
        }

        private void Start()
        {
            SetBuildingProperties();
        }
        #endregion

        #region Methods
        public virtual void ProductResource()
        {
            if (_farmRunning || _workersAmount < requiredWorkers)
                return;

            if (requiredResource)
                if (requiredResource.amount < _currentDatas.AmountToGenerate)
                    return;

            Debug.Log("Started production.");
            StartCoroutine("GenerateResourceRoutine");
        }

        /// <summary>
        /// Generate a resource after waiting a amount of time
        /// </summary>
        private IEnumerator GenerateResourceRoutine()
        {
            _farmRunning = true;
            yield return new WaitForSecondsRealtime(_currentDatas.ProductTime);

            AddResource();
            _farmRunning = false;
        }

        /// <summary>
        /// Increase resource amount
        /// </summary>
        private void AddResource()
        {
            if (requiredResource)
                requiredResource.amount -= _currentDatas.AmountToGenerate;

            farmResource.amount += _currentDatas.GeneratedAmount;
        }
        #endregion

        #region Inherited Methods
        protected override void SetBuildingProperties()
        {
            _currentDatas = properties[CurrentLevel];
            _gui.SetFarmGUI();
        }
        #endregion
    }

    #region FarmDatas class
    [System.Serializable]
    public class FarmDatas
    {
        [Tooltip("Production speed of the farm.")]
        [SerializeField] private float productTime = 3f;
        [Tooltip("How many resources required to generate a resource.")]
        [SerializeField] private int amountToGenerate = 10;
        [Tooltip("How many resources generated.")]
        [SerializeField] private int generatedAmount = 1;

        public float ProductTime => productTime;
        public int AmountToGenerate => amountToGenerate;
        public int GeneratedAmount => generatedAmount;
    }
    #endregion
}