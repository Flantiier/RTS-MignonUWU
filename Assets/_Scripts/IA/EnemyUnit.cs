using UnityEngine;
using System.Collections;

public class EnemyUnit : UnitBehavior
{
    #region Variables
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private LayerMask detectMask;

    [Header("Combat")]
    [SerializeField] private float attackRate = 3f;
    private Coroutine _attackRoutine;
    #endregion

    #region Properties
    public int State { get; protected set; }
    #endregion

    #region Builts_In
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, detectRadius);
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
        if (Attacking || _attackRoutine != null)
            return;

        //Look at target
        if (!Target)
            Target = GetTarget();

        _attackRoutine = StartCoroutine("AttackRoutine");
    }

    /// <summary>
    /// Basic attack pattern
    /// </summary>
    private IEnumerator AttackRoutine()
    {
        Debug.Log("Attack");

        Attacking = true;
        //Set animation

        //Wait
        yield return new WaitForSecondsRealtime(attackRate);
        //Reset
        Attacking = false;
        _attackRoutine = null;
    }

    /// <summary>
    /// Get a target to look at 
    /// </summary>
    protected Transform GetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius, detectMask);

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
        return Physics.CheckSphere(transform.position, detectRadius, detectMask);
    }
    #endregion
}
