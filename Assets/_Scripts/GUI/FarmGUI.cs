using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.Building
{
    public class FarmGUI : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TextMeshProUGUI titleField;
        [SerializeField] private TextMeshProUGUI propertiesField;
        [SerializeField] private Color textColor = Color.green;
        [SerializeField] private Transform slotsContent;
        [SerializeField] private UpgradeResourceSlot upgradeSlotPrefab;
        [SerializeField] private Button upgradeButton;

        private ResourceFarm _farm;
        #endregion

        #region Builts_In
        private void Awake()
        {
            _farm = GetComponentInParent<ResourceFarm>();
        }

        private void LateUpdate()
        {
            if (!upgradeButton.gameObject.activeSelf)
                return;

            upgradeButton.interactable = _farm.HasResourcesToUpgrade(_farm.Upgrades[_farm.CurrentLevel].RequiredResources);
        }
        #endregion

        #region Methods
        /// <summary>
        /// DIsplay farm properties at current level
        /// </summary>
        public void SetFarmGUI()
        {
            bool maxLevel = _farm.CurrentLevel >= _farm.Upgrades.Length;
            upgradeButton.gameObject.SetActive(!maxLevel);

            SetTexts(maxLevel);
            SetSlots(maxLevel);
        }

        /// <summary>
        /// Set GUI texts
        /// </summary>
        private void SetTexts(bool maxLevel)
        {
            int index = _farm.CurrentLevel;
            FarmDatas current = _farm.Properties[index];

            //Set current properties
            string title = $"Niveau {index + 1}";
            string prodTime = $"Temps de production : {current.ProductTime}s";
            string required = $"Ressources requises : {current.AmountToGenerate}";
            string generated = $"Ressources produites : {current.GeneratedAmount}";

            //Display upgrades properties
            if (!maxLevel)
            {
                FarmDatas next = _farm.Properties[index + 1];
                title += InsertColoredText(" -> ", $"{index + 2}");
                prodTime += InsertColoredText(" -> ", $"{next.ProductTime}", "s");
                required += InsertColoredText(" -> ", $"{next.AmountToGenerate}");
                generated += InsertColoredText(" -> ", $"{next.GeneratedAmount}");
            }

            //Set texts
            titleField.SetText(title);
            propertiesField.text = prodTime + "\r\n" + required + "\r\n" + generated;
        }

        /// <summary>
        /// Set resources icon required to upgrade the building
        /// </summary>
        private void SetSlots(bool maxLevel)
        {
            if (maxLevel)
            {
                slotsContent.parent.gameObject.SetActive(false);
                return;
            }

            foreach (Transform child in slotsContent)
                Destroy(child.gameObject);

            UpgradeDatas[] datas = _farm.Upgrades[_farm.CurrentLevel].RequiredResources;
            foreach (UpgradeDatas data in datas)
            {
                UpgradeResourceSlot instance = Instantiate(upgradeSlotPrefab, slotsContent);
                instance.resource = data.Resource;
                instance.MaxAmount = data.Amount;
                instance.InitializeSlot();
            }
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
