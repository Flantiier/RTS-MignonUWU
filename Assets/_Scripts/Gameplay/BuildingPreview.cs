using Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

public class BuildingPreview : MonoBehaviour
{
    #region Variables
    private NavMeshObstacle _navMeshObs;
    public SphereCollider Collider { get; private set; }
    public int NbCollisions { get; private set; } = 0;
    #endregion

    #region Builts_In
    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
        _navMeshObs = GetComponent<NavMeshObstacle>();
        Collider.isTrigger = true;
        _navMeshObs.enabled = false;
    }

    private void OnDestroy()
    {
        _navMeshObs.enabled = true;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasCollisionLayer(other.gameObject))
            return;

        NbCollisions++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!HasCollisionLayer(other.gameObject))
            return;

        NbCollisions--;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Check if the GO layer is in collision layermask
    /// </summary>
    private bool HasCollisionLayer(GameObject obj)
    {
        return BuildingManager.Instance.collisionMask == (BuildingManager.Instance.collisionMask | (1 << obj.layer));
    }
    #endregion
}
