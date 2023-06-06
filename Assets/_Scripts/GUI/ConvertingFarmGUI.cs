﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.Building
{
    public class ConvertingFarmGUI : FarmBuildingGUI
    {
        #region Variables
        [SerializeField] private GameObject convertPanel;
        [SerializeField] private GameObject progressPanel;
        [SerializeField] private ResourceSlot slot;
        [SerializeField] private Image productIcon;
        [SerializeField] private TextMeshProUGUI productText;

        private ConvertingFarm _convertingFarm;
        #endregion

        #region Builts_In
        protected override void Awake()
        {
            _convertingFarm = GetComponentInParent<ConvertingFarm>();
            _farm = _convertingFarm;
        }

        private void Start()
        {
            InitializeGUI();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize ui elements
        /// </summary>
        private void InitializeGUI()
        {
            slot.resource = _convertingFarm.ConvertedResource;
            slot.MaxAmount = _convertingFarm.AmountToGenerate;
            slot.InitializeSlot();
            productIcon.sprite = _convertingFarm.FarmResource.icon;
            productText.text = $"+{_convertingFarm.GeneratedAmount}";
        }

        /// <summary>
        /// Start conversion coroutine of the farm
        /// </summary>
        public void StartConvertingResource()
        {
            _convertingFarm.ProductResource();
            InvertPanels();
        }

        /// <summary>
        /// Stop conversion coroutine of the farm 
        /// </summary>
        public void StopConversion()
        {
            _convertingFarm.StopConversion();
            InvertPanels();
        }

        /// <summary>
        /// Swicth the current enabled panel
        /// </summary>
        public void InvertPanels()
        {
            convertPanel.SetActive(!convertPanel.activeSelf);
            progressPanel.SetActive(!progressPanel.activeSelf);
        }
        #endregion
    }
}