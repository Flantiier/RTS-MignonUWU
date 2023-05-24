using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent responses;

        public void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }
        public void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void Raise()
        {
            responses?.Invoke();
        }
    }
}
