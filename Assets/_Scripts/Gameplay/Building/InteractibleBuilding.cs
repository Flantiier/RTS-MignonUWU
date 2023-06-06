using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Gameplay.Building
{
    public class InteractibleBuilding : MonoBehaviour
    {
        #region Variables/Properties
        [Header("Building GUI")]
        [SerializeField] private GameObject buildingCanvas;
        [SerializeField] private bool placedAtStart = false;
        public bool IsPlaced { get; set; } = false;
        #endregion

        #region Builts_In
        private void Awake()
        {
            IsPlaced = placedAtStart;
        }
        #endregion

        #region Methods
        public void DisableCanvas()
        {
            buildingCanvas.SetActive(false);
        }

        public void OnMouseDown()
        {
            if (!IsPlaced)
                return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!buildingCanvas || buildingCanvas.activeInHierarchy)
                return;

            buildingCanvas.SetActive(true);
        }
        #endregion
    }
}