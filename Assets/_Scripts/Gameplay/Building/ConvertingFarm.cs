using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class ConvertingFarm : FarmBuilding
    {
        #region Variables
        [Header("Variant properties")]
        [SerializeField] private Resource convertedResource;
        [SerializeField] private int amountToGenerate = 10;

        private ConvertingFarmGUI _convertingFarmGUI;
        #endregion

        #region Properties
        public Resource ConvertedResource => convertedResource;
        public int AmountToGenerate => amountToGenerate;
        public bool FarmRunning => _farmRoutine != null;
        #endregion

        #region Builts_In
        protected override void Awake()
        {
            _convertingFarmGUI = GetComponentInChildren<ConvertingFarmGUI>();
        }

        protected override void Start()
        {
            buildingCanvas.SetActive(false);
            SetBuildingProperties();
        }

        protected override void Update() { }
        #endregion

        #region Methods
        public override void ProductResource()
        {
            if (_farmRoutine != null)
                return;

            if (convertedResource.amount < amountToGenerate)
                return;

            StartCoroutine("GenerateResourceRoutine");
        }

        protected override IEnumerator GenerateResourceRoutine()
        {
            ProductProgress = 0;
            while (ProductProgress < _currentDatas.ProductTime)
            {
                ProductProgress += Time.deltaTime;
                yield return null;
            }

            //Reset routine
            AddResource();
            ProductProgress = 0;
            _farmRoutine = null;

            //Invert gui
            _convertingFarmGUI.InvertPanels();
        }

        /// <summary>
        /// Stop the converting coroutine
        /// </summary>
        public void StopConversion()
        {
            if (_farmRoutine == null)
                return;

            StopCoroutine(_farmRoutine);
            _farmRoutine = null;
        }

        /// <summary>
        /// Increase resource amount
        /// </summary>
        protected override void AddResource()
        {
            if (convertedResource)
                convertedResource.amount -= amountToGenerate;

            farmResource.amount += generatedAmount;
        }
        #endregion

        #region Inherited Methods
        /// <summary>
        /// Set building properties
        /// </summary>
        protected override void SetBuildingProperties()
        {
            _currentDatas = properties[CurrentLevel];
            _convertingFarmGUI.UpdateGUI();
        }
        #endregion

    }
}
