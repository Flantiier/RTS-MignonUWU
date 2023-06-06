using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Gameplay.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SelectableUnit : UnitBehavior
    {
        #region Variables
        public enum UnitStateMachine { Waiting, GoToPoint, Combat }

        //References
        [Header("References")]
        [SerializeField] private GameObject selector;
        [SerializeField] private Resource resource;
        #endregion

        #region Properties
        public UnitStateMachine CurrentState { get; protected set; }
        public Resource Resource => resource;
        #endregion

        #region Builts_In
        protected override void OnEnable()
        {
            //Health
            base.OnEnable();

            CurrentState = UnitStateMachine.Waiting;
            SetSelector(false);
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

        protected override void UpdateAnimations()
        {
            if (!Animator)
                return;

            Animator.SetBool("Walk", _navMesh.velocity.magnitude >= 1);
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
            Destination = target.position;
            CurrentState = UnitStateMachine.Combat;
        }

        private void CombatState()
        {
            float distance = GetDistanceFromDestination();

            if (!Target)
            {
                CurrentState = distance >= 5f ? UnitStateMachine.GoToPoint : UnitStateMachine.Waiting;
                return;
            }

            //Too Far to attack
            if (distance > attackDistance)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(Target.position);
            }
            //Close target => Attack
            else
            {
                if (!Attacking)
                    StartCoroutine(AttackRoutine(attackRate));

                FaceTarget();
                Stop();
            }
        }

        /// <summary>
        /// Start to attack the current target
        /// </summary>
        public override void Attack()
        {
            if (!Target || !Target.TryGetComponent(out Entity enemy))
                return;

            enemy.DealDamages(GetDamages());
        }

        /// <summary>
        /// Get attack damages value
        /// </summary>
        public float GetDamages()
        {
            if (!resource)
                return properties.GetDamagesValue();

            if (Target.TryGetComponent(out DestructibleResource target) && target.Resource == resource)
                return properties.GetDamagesValue();

            return damages;
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
}
