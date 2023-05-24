using UnityEngine;

namespace Scripts.Gameplay.Units
{
    public class BossUnit : EnemyUnit
    {
        [Header("Events")]
        [SerializeField] private GameEvent defeatedEvent;

        protected override void HandleDeath()
        {
            if (defeatedEvent)
                defeatedEvent.Raise();

            base.HandleDeath();
        }
    }
}
