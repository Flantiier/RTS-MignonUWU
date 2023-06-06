using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class FarmBuilding : UpgradableBuilding
    {
        #region Variables
        [Header("Farm properties")]
        [SerializeField] protected Resource farmResource;
        [SerializeField] protected int generatedAmount = 1;

        protected FarmDatas _currentDatas;
        protected FarmBuildingGUI _gui;
        protected Coroutine _farmRoutine;
        #endregion

        #region Properties
        public Resource FarmResource => farmResource;
        public FarmDatas[] Properties => properties;
        public int GeneratedAmount => generatedAmount;
        public float ProductProgress { get; protected set; }
        #endregion

        #region Builts_In
        protected virtual void Awake()
        {
            _gui = GetComponentInChildren<FarmBuildingGUI>();
        }

        protected virtual void Start()
        {
            buildingCanvas.SetActive(false);
            SetBuildingProperties();
        }

        protected virtual void Update()
        {
            ProductResource();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Start the Coroutine to generate some resources
        /// </summary>
        public virtual void ProductResource()
        {
            if (_farmRoutine != null)
                return;

            _farmRoutine = StartCoroutine("GenerateResourceRoutine");
        }

        /// <summary>
        /// Generate a resource after waiting a amount of time
        /// </summary>
        protected virtual IEnumerator GenerateResourceRoutine()
        {
            ProductProgress = 0;
            while (ProductProgress < _currentDatas.ProductTime)
            {
                ProductProgress += Time.deltaTime;
                yield return null;
            }

            AddResource();
            ProductProgress = 0;
            _farmRoutine = null;
        }

        /// <summary>
        /// Increase resource amount
        /// </summary>
        protected virtual void AddResource()
        {
            farmResource.amount += generatedAmount;
        }
        #endregion

        #region Inherited Methods
        public override void UpgradeBuilding()
        {
            if (_farmRoutine != null)
            {
                StopCoroutine(_farmRoutine);
                _farmRoutine = null;
            }

            ProductProgress = 0f;
            base.UpgradeBuilding();
        }

        /// <summary>
        /// Set building properties
        /// </summary>
        protected override void SetBuildingProperties()
        {
            _currentDatas = properties[CurrentLevel];
            _gui.UpdateGUI();
        }
        #endregion
    }
}