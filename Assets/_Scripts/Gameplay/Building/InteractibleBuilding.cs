using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scripts.Gameplay.Building
{
    public class InteractibleBuilding : MonoBehaviour
    {
        [Header("Building GUI")]
        [SerializeField] private GameObject buildingCanvas;
        public bool IsPlaced { get; set; } = false;

        public void DisableCanvas()
        {
            buildingCanvas.SetActive(false);
        }

        public void OnMouseDown()
        {
            if (!IsPlaced)
                return;

            if (!buildingCanvas || buildingCanvas.activeInHierarchy)
                return;

            buildingCanvas.SetActive(true);
        }
    }
}