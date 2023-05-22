using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Gameplay
{
    public class InteractibleBuilding : MonoBehaviour
    {
        [Header("Building GUI")]
        [SerializeField] private GameObject buildingCanvas;

        /// <summary>
        /// Enable or disable the world space canvas attached to this building
        /// </summary>
        public void EnableCanvas()
        {
            if (!buildingCanvas || buildingCanvas.activeInHierarchy)
                return;

            buildingCanvas.SetActive(enabled);
        }

        public void OnMouseDown()
        {
            EnableCanvas();
        }
    }
}