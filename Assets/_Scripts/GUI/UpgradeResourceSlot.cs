using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class UpgradeResourceSlot : ResourceSlot
    {
        #region Variables/Properties
        [SerializeField] private Color colorAbove = Color.green;
        [SerializeField] private Color colorUnder = Color.red;

        public int MaxAmount { get; set; }
        #endregion

        #region Inherited Builts_In
        protected override void Update()
        {
            if (!resource)
                return;

            _textMesh.text = ColoredAmount() + "/" + MaxAmount.ToString();
        }

        private string ColoredAmount()
        {
            string hex = resource.amount >= MaxAmount ? colorAbove.ToHexString() : colorUnder.ToHexString();
            return $"<color=#{hex}>{resource.amount}</color>";
        }
        #endregion
    }
}