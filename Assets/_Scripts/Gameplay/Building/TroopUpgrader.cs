using ScriptableObjects;
using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class TroopUpgrader : InteractibleBuilding
    {
        #region Variables
        [SerializeField] private UnitProperties[] units;
        [SerializeField] private UpgradeUnitButton[] buttons;
        #endregion

        #region Buitls_In
        private void Awake()
        {
            for (int i = 0; i < units.Length; i++)
                buttons[i].SetUnit(units[i]);
        }
        #endregion
    }
}