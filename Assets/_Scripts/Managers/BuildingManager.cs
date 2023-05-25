using Unity.VisualScripting;
using UnityEngine;
using Scripts.Gameplay.Building;

namespace Scripts.Managers
{
    public class BuildingManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private string testBuilding;
        [Header("Building references")]
        [SerializeField] private BuildingTag[] buildings;
        [Header("Building positionning")]
        [SerializeField] private Material invalidMaterial;
        public LayerMask collisionMask;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float checkRadius = 0.5f;

        private BuildingPreview _building;
        private Material _currentMat;

        public static BuildingManager Instance { get; private set; }
        #endregion

        #region Builts_In
        private void Awake()
        {
            SingletonPattern();
        }

        private void Update()
        {
            if (!_building)
                return;

            Debug.Log(CanPlaceBuilding());
            UpdateBuildingPosition();
            SetBuildingMaterial();
        }
        #endregion

        #region Methods
        [ContextMenu("Test")]
        public void TestBuilding()
        {
            InstantiateBuilding(testBuilding);
        }

        /// <summary>
        /// Instantiate a building by giving a name
        /// </summary>
        public void InstantiateBuilding(string name)
        {
            foreach (BuildingTag building in buildings)
            {
                if (building.Name != name)
                    continue;

                InteractibleBuilding instance = Instantiate(building.Prefab);
                instance.DisableCanvas();
                instance.enabled = false;

                _building = instance.AddComponent<BuildingPreview>();
                _currentMat = _building.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            }
        }

        /// <summary>
        /// Shoot a raycast from the center of the camera
        /// </summary>
        private void UpdateBuildingPosition()
        {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask, QueryTriggerInteraction.Collide))
            {
                _building.transform.position = new Vector3(hit.point.x, 1f, hit.point.z);
            }
        }

        /// <summary>
        /// Check if the building can be placed
        /// </summary>
        private void PlaceBuilding()
        {
            if (_building.NbCollisions > 0 || !CheckBuildingBounds())
                return;

            //Place building$
            _currentMat = null;
            InteractibleBuilding instance = _building.GetComponent<InteractibleBuilding>();
            instance.enabled = true;
            instance.IsPlaced = true;

            Destroy(_building);
            _building = null;
        }

        /// <summary>
        /// Indicates if the building can be placed
        /// </summary>
        private bool CanPlaceBuilding()
        {
            return _building.NbCollisions <= 0 && CheckBuildingBounds();
        }

        /// <summary>
        /// Check if there is ground around the building
        /// </summary>
        private bool CheckBuildingBounds()
        {
            SphereCollider collider = _building.Collider;
            Vector3 position = _building.transform.position;
            Vector3[] corners = new Vector3[]
            {
            new Vector3(-collider.radius, 0f, 0),
            new Vector3(0, 0f, collider.radius),
            new Vector3(collider.radius, 0f, 0),
            new Vector3(0, 0f, -collider.radius)
            };

            foreach (Vector3 corner in corners)
            {
                if (Physics.CheckSphere(position + corner, checkRadius, groundMask))
                    continue;
                else
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Set building material based on placement boolean
        /// </summary>
        private void SetBuildingMaterial()
        {
            if (!_currentMat)
                return;

            Material material = CanPlaceBuilding() ? _currentMat : invalidMaterial;
            _building.GetComponentInChildren<MeshRenderer>().sharedMaterial = material;
        }
        #endregion

        #region Design Patterns
        private void SingletonPattern()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }
        #endregion
    }

    [System.Serializable]
    public struct BuildingTag
    {
        [SerializeField] private string name;
        [SerializeField] private InteractibleBuilding buildingPrefab;

        public string Name => name;
        public InteractibleBuilding Prefab => buildingPrefab;
    }
}
