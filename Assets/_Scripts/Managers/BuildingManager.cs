using Unity.VisualScripting;
using UnityEngine;
using Scripts.Gameplay.Building;
using ScriptableObjects;
using UnityEngine.EventSystems;

namespace Scripts.Managers
{
    public class BuildingManager : MonoBehaviour
    {
        #region Variables
        [Header("Building references")]
        [SerializeField] private Buildinginfos[] buildings;

        [Header("Building GUI")]
        [SerializeField] private Transform buildingContent;
        [SerializeField] private BuildingDropper slotPrefab;

        [Header("Building positionning")]
        [SerializeField] private Material invalidMaterial;
        public LayerMask collisionMask;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float checkRadius = 0.5f;

        private Buildinginfos _infos;
        private BuildingPreview _building;
        private Material _currentMat;
        #endregion

        #region Properties
        public static BuildingManager Instance { get; private set; }
        public bool IsDragging { get; private set; }
        #endregion

        #region Builts_In
        private void Awake()
        {
            SingletonPattern();
            InstantiateBuildingSlots();
        }

        private void Update()
        {
            if (!_building)
                return;

            UpdateBuildingPosition();
            SetBuildingMaterial();
        }
        #endregion

        #region Drag&Drop Methods
        /// <summary>
        /// Indicates if the player is dragging or not
        /// </summary>
        public void Dragging(bool value)
        {
            IsDragging = value;
            CanvasGroup canvasGroup = buildingContent.parent.GetComponent<CanvasGroup>();
            canvasGroup.alpha = value ? 0 : 1;
            canvasGroup.blocksRaycasts = !value;
        }
        #endregion

        #region Building Methods
        /// <summary>
        /// Instantiate a building by giving a name
        /// </summary>
        public void InstantiateBuilding(Buildinginfos infos)
        {
            foreach (Buildinginfos building in buildings)
            {
                if (infos != building)
                    continue;

                InteractibleBuilding instance = Instantiate(building.Prefab);
                instance.DisableCanvas();
                instance.enabled = false;

                _infos = infos;
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
        public void PlaceBuilding()
        {
            if (!CanPlaceBuilding())
            {
                Destroy(_building.gameObject);
                _infos = null;
                return;
            }

            //Place building
            _currentMat = null;
            InteractibleBuilding instance = _building.GetComponent<InteractibleBuilding>();
            instance.enabled = true;
            instance.IsPlaced = true;

            //Use resources
            foreach (UpgradeDatas resource in _infos.Resource.RequiredResources)
                GameManager.Instance.UseResource(resource.Resource, resource.Amount);

            Destroy(_building);
            _building = null;
            _infos = null;
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

        #region GUI Methods
        /// <summary>
        /// Instantiates all building slots
        /// </summary>
        private void InstantiateBuildingSlots()
        {
            foreach (Buildinginfos info in buildings)
            {
                BuildingDropper instance = Instantiate(slotPrefab, buildingContent);
                instance.UpdateInfos(info);
            }
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
}
