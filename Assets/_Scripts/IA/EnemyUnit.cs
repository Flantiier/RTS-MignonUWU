using UnityEngine;

public class EnemyUnit : UnitBehavior
{
    #region Variables
    [Header("Detection")]
    [SerializeField] private LayerMask detectMask;
    #endregion

    #region Properties
    public int State { get; protected set; }
    #endregion

    #region Builts_In
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    #endregion

    #region Methods
    protected override void HandleUnitBehavior()
    {
        //Update enemy state => 0 : Idle // 1 => Combat
        State = DetectUnits() ? 1 : 0;

        switch (State)
        {
            case 1:
                FaceTarget();
                CombatState();
                break;
            default:
                break;
        }
    }

    private void CombatState()
    {
        //Look at target
        if (!Target)
            Target = GetCloseTarget();

        //Already attacking
        if (Attacking)
            return;

        //Start Attack routine
        StartCoroutine(AttackRoutine(attackRate));
    }

    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDistance, detectMask);

        if (colliders.Length <= 0)
            return;

        foreach (Collider col in colliders)
        {
            if (!col || !col.TryGetComponent(out SelectableUnit unit))
                continue;

            unit.DealDamages(damages);
        }
    }

    /// <summary>
    /// Get a target to look at 
    /// </summary>
    protected Transform GetCloseTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDistance, detectMask);

        if (colliders.Length <= 0)
            return null;

        return colliders[0].transform;
    }

    /// <summary>
    /// Create a spherical detection around this entity
    /// </summary>
    /// <returns></returns>
    protected bool DetectUnits()
    {
        return Physics.CheckSphere(transform.position, attackDistance, detectMask);
    }
    #endregion
}
