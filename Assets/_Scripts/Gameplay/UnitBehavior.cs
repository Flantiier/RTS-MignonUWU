using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Gameplay.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitBehavior : Entity
    {
        #region Variables
        [Header("Unit properties")]
        [SerializeField] private Animator animator;
        [SerializeField, Range(0, 0.1f)] private float smoothRotation = 0.05f;
        [SerializeField] protected float moveSpeed = 3f;

        [Header("Combat")]
        [SerializeField] protected float damages = 25f;
        [SerializeField] protected float attackDistance = 1f;
        [SerializeField] protected float attackRate = 3f;

        protected NavMeshAgent _navMesh;
        #endregion

        #region Properties
        public Transform Target { get; protected set; }
        public Vector3 Destination { get; protected set; }
        public bool Attacking { get; set; }
        public Animator Animator => animator;
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

        #region AI Methods
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

        protected float GetDistanceFromDestination()
        {
            return Vector3.Distance(Destination, transform.position);
        }
        #endregion

        #region Combat Methods
        /// <summary>
        /// Attack method => Used by animation event
        /// </summary>
        public virtual void Attack() { }

        /// <summary>
        /// Basic attack pattern
        /// </summary>
        protected virtual IEnumerator AttackRoutine(float attackRate)
        {
            Attacking = true;
            //Set animation
            Animator.SetTrigger("Attack");

            //Wait
            yield return new WaitForSecondsRealtime(attackRate);
            Attacking = false;
        }
        #endregion

        #endregion
    }
}
