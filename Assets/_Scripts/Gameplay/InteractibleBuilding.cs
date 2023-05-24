﻿using UnityEngine;

namespace Scripts.Gameplay.Building
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

            buildingCanvas.SetActive(true);
        }

        public void OnMouseDown()
        {
            Debug.Log("On clicked");
            EnableCanvas();
        }
    }
}