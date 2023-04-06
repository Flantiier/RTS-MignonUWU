using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : UnitBehavior
{
    #region Variables
    public enum UnitStateMachine { Waiting, GoToPoint, Combat }

    [Header("References")]
    [SerializeField] private GameObject selector;

    [Header("Unit properties")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 1;
    #endregion

    #region Properties
    public UnitStateMachine CurrentState { get; protected set; }
    #endregion

    #region Builts_In
    protected override void OnEnable()
    {
        //Health
        base.OnEnable();

        CurrentState = UnitStateMachine.Waiting;
        SetSelector(false);
    }

    protected override void Update()
    {
        HandleUnitBehavior();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Handle the behaviour of this unit
    /// </summary>
    protected override void HandleUnitBehavior()
    {
        switch (CurrentState)
        {
            case UnitStateMachine.GoToPoint:
                GoToPointState();
                break;
            case UnitStateMachine.Combat:
                CombatState();
                break;
            default:
                break;
        }
    }

    #region PointState Methods
    /// <summary>
    /// Set the destination of the navMesh agent
    /// </summary>
    /// <param name="destination"> Target position <param>
    public void EnterMoveState(Vector3 destination)
    {
        Destination = destination;
        CurrentState = UnitStateMachine.GoToPoint;
    }

    /// <summary>
    /// Move until reaching the current destintation
    /// </summary>
    private void GoToPointState()
    {
        //Set move speed
        Move(moveSpeed);
        _navMesh.SetDestination(Destination);

        //Destination not reached yet
        if (_navMesh.remainingDistance > 0.5f)
            CurrentState = UnitStateMachine.GoToPoint;

        //Reached destination
        CurrentState = UnitStateMachine.Waiting;
    }
    #endregion

    #region Combat Methods
    /// <summary>
    /// Set the current state on Combat
    /// </summary>
    /// <param name="target"> Target to fight </param>
    public void EnterCombatState(Transform target)
    {
        Target = target;
        CurrentState = UnitStateMachine.Combat;
    }

    private void CombatState()
    {
        //Too Far to attack
        if ((Target.position - transform.position).magnitude > attackDistance)
        {
            _navMesh.SetDestination(Target.position);
            return;
        }
        //Can attack
        FaceTarget();
        Stop();
    }
    #endregion

    #region Select Methods
    public virtual void Select()
    {
        SetSelector(true);
    }

    public virtual void Deselect()
    {
        SetSelector(false);
    }

    private void SetSelector(bool state)
    {
        if (!selector)
            return;

        selector.SetActive(state);
    }
    #endregion

    #endregion
}
