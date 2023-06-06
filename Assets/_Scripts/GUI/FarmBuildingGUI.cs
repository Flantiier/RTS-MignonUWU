using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.Building
{
    public class FarmBuildingGUI : MonoBehaviour
    {
        #region Variables
        [Header("Text properties")]
        [SerializeField] protected TextMeshProUGUI titleField;
        [SerializeField] protected TextMeshProUGUI propertiesField;
        [SerializeField] private Color textColor = Color.green;
        [Header("Resource properties")]
        [SerializeField] private Transform slotsContent;
        [SerializeField] private ResourceSlot resourceSlot;
        [SerializeField] protected Button upgradeButton;
        [SerializeField] private GameObject upgradeText;
        [Header("Production properties")]
        [SerializeField] private Slider productSlider;

        protected FarmBuilding _farm;
        #endregion

        #region Builts_In
        protected virtual void Awake()
        {
            _farm = GetComponentInParent<FarmBuilding>();
            upgradeButton.gameObject.SetActive(_farm.Upgrades.Length != 0);
        }

        protected virtual void OnEnable()
        {
            if (_farm.CurrentLevel >= _farm.Upgrades.Length)
                return;

            upgradeButton.interactable = GameManager.Instance.HasEnoughResources(_farm.Upgrades[_farm.CurrentLevel].RequiredResources);
        }

        protected virtual void LateUpdate()
        {
            productSlider.value = _farm.ProductProgress;

            if (_farm.Upgrades.Length <= 0)
            {
                upgradeButton.gameObject.SetActive(false);
                return;
            }

            //interactability of the upgrade button
            if (upgradeButton.gameObject.activeSelf)
                upgradeButton.interactable = GameManager.Instance.HasEnoughResources(_farm.Upgrades[_farm.CurrentLevel].RequiredResources);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Invoke the upgrade building method
        /// </summary>
        public void UpgradeBuilding()
        {
            if (!_farm || _farm.CurrentLevel >= _farm.Properties.Length)
                return;

            _farm.UpgradeBuilding();
        }

        /// <summary>
        /// DIsplay farm properties at current level
        /// </summary>
        public void UpdateGUI()
        {
            bool maxLevel = _farm.CurrentLevel >= _farm.Upgrades.Length;
            upgradeButton.gameObject.SetActive(!maxLevel);

            SetTextFields(maxLevel);
            SetResourceSlots(maxLevel);
            SetSlider();
        }

        /// <summary>
        /// Set GUI texts
        /// </summary>
        protected virtual void SetTextFields(bool maxLevel)
        {
            int index = _farm.CurrentLevel;
            FarmDatas current = _farm.Properties[index];

            //Set current properties
            string title = $"Niveau {index + 1}";
            string prodTime = $"Temps de production : {current.ProductTime}s";
            string amount = $"Resources produites : {_farm.GeneratedAmount}";

            //Display upgrades properties
            if (!maxLevel)
            {
                FarmDatas next = _farm.Properties[index + 1];
                title += InsertColoredText(" -> ", $"{index + 2}");
                prodTime += InsertColoredText(" -> ", $"{next.ProductTime}", "s");
            }

            //Set texts
            titleField.SetText(title);
            propertiesField.text = prodTime + "\r\n" + amount;
        }

        /// <summary>
        /// Set resources icon required to upgrade the building
        /// </summary>
        protected void SetResourceSlots(bool maxLevel)
        {
            //Disable panel when level max is reached
            if (maxLevel)
            {
                upgradeText.SetActive(true);
                slotsContent.parent.gameObject.SetActive(false);
                return;
            }

            //Destroy previous slots
            foreach (Transform child in slotsContent)
                Destroy(child.gameObject);

            //Create new slots
            UpgradeDatas[] datas = _farm.Upgrades[_farm.CurrentLevel].RequiredResources;
            foreach (UpgradeDatas data in datas)
            {
                ResourceSlot instance = Instantiate(resourceSlot, slotsContent);
                instance.resource = data.Resource;
                instance.MaxAmount = data.Amount;
                instance.InitializeSlot();
            }
        }

        /// <summary>
        /// Set slider max value
        /// </summary>
        protected void SetSlider()
        {
            productSlider.maxValue = _farm.Properties[_farm.CurrentLevel].ProductTime;
            productSlider.value = 0;
        }

        /// <summary>
        /// Create a colored string and return the result
        /// </summary>
        private string InsertColoredText(string beforeColor, string colored)
        {
            string hex = textColor.ToHexString();
            return beforeColor + $"<color=#{hex}>{colored}</color>";
        }

        private string InsertColoredText(string beforeColor, string colored, string afterColor)
        {
            string hex = textColor.ToHexString();
            return beforeColor + $"<color=#{hex}>{colored}</color>" + afterColor;
        }
        #endregion
    }
}
