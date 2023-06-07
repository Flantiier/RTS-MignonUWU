using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;

namespace Scripts.Gameplay.Building
{
    public class UpgradeUnitButton : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private ResourceSlot slot;
        [SerializeField] private Button button;

        public UnitProperties Unit { get; private set; }
        #endregion

        #region Buitls_In
        private void LateUpdate()
        {
            if (!Unit || Unit.CurrentLevel >= Unit.MaxLevel || !slot.resource)
                return;

            bool hasResources = slot.resource.amount >= slot.MaxAmount;
            button.interactable = hasResources;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set unit property reference and update GUI
        /// </summary>
        public void SetUnit(UnitProperties unit)
        {
            Unit = unit;
            icon.sprite = unit.Sprite;
            textField.text = unit.Name;
            UpdateGUI();
        }

        /// <summary>
        /// Update GUI
        /// </summary>
        private void UpdateGUI()
        {
            if (Unit.CurrentLevel >= Unit.MaxLevel)
            {
                slot.gameObject.SetActive(false);
                return;
            }

            UpgradeDatas data = Unit.Upgrades[Unit.CurrentLevel];
            slot.resource = data.Resource;
            slot.MaxAmount = data.Amount;
            slot.InitializeSlot();
        }

        /// <summary>
        /// Method to increase the level of a troop
        /// </summary>
        public void UpgradeUnit()
        {
            Unit.IncreaseLevel();
            UpdateGUI();

            if(Unit.CurrentLevel >= Unit.MaxLevel)
                button.gameObject.SetActive(false);
        }
        #endregion
    }
}
