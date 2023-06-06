using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;

namespace Scripts.Gameplay.Building
{
    public class SpawnUnitButton : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private ResourceSlot slot;

        public UnitProperties Unit { get; private set; }
        private UnitSpawner _spawner;
        private Button _button;
        #endregion

        #region Buitls_In
        private void Awake()
        {
            _spawner = GetComponentInParent<UnitSpawner>();
            _button = GetComponent<Button>();
        }

        private void LateUpdate()
        {
            if (!Unit)
                return;

            _button.interactable = slot.resource.amount >= slot.MaxAmount;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set unit property reference and update GUI
        /// </summary>
        public void SetUnit(UnitProperties unit, UpgradeDatas resource)
        {
            Unit = unit;
            icon.sprite = unit.Sprite;
            textField.text = unit.Name;
            slot.resource = resource.Resource;
            slot.MaxAmount = resource.Amount;
            slot.InitializeSlot();
        }

        /// <summary>
        /// Spawn unit
        /// </summary>
        public void SpawnUnit()
        {
            _spawner.SpawnUnit(Unit);
        }
        #endregion
    }
}