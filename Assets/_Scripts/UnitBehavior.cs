using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitBehavior : MonoBehaviour
{
    #region Variables
    [Header("Unit properties")]
    [SerializeField, Range(0, 0.1f)] private float smoothRotation = 0.05f;

    protected NavMeshAgent _navMesh;
    #endregion

    #region Properties
    public Transform Target { get; protected set; }
    public Vector3 Destination { get; protected set; }
    public bool Attacking { get; set; }
    #endregion

    #region Builts_In
    protected virtual void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        HandleUnitBehavior();
        UpdateAnimations();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Handle the behavior of this IA entity
    /// </summary>
    protected virtual void HandleUnitBehavior() { }

    /// <summary>
    /// Update animator parameters
    /// </summary>
    protected virtual void UpdateAnimations() { }

    #region AI methods
    /// <summary>
    /// Face the current target
    /// </summary>
    protected void FaceTarget()
    {
        if (!Target)
            return;

        Quaternion rotation = Quaternion.LookRotation(Target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothRotation);
    }

    /// <summary>
    /// Set the navMesh agent speed
    /// </summary>
    /// <param name="speed"> Move speed </param>
    protected void Move(float speed)
    {
        _navMesh.isStopped = false;
        _navMesh.speed = speed;
    }

    /// <summary>
    /// Stop the navmesh agent
    /// </summary>
    protected void Stop()
    {
        if (_navMesh.isStopped)
            return;

        _navMesh.isStopped = true;
    }
    #endregion

    #endregion
}
