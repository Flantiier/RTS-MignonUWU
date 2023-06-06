using UnityEngine;
using System.Collections;
using Scripts.Gameplay.Units;

namespace Scripts.Gameplay.Building
{
    public class UnitSpawner : InteractibleBuilding
    {
        #region Variables
        [Header("Spawner properties")]
        [SerializeField] private SelectableUnit[] units;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private float spawnCooldown = 2f;

        private bool _spawnWait = false;
        #endregion

        #region Builts_In
        private void Awake()
        {
            IsPlaced = true;
        }
        #endregion

        #region Methods
        public void SpawnUnit(int index)
        {
            if (_spawnWait)
                return;

            SelectableUnit unit = Instantiate(units[index], spawnPos.position, Quaternion.LookRotation(spawnPos.position));
            Vector3 direction = (transform.position - spawnPos.position).normalized * 1.5f;
            unit.EnterMoveState(direction);

            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            _spawnWait = true;
            yield return new WaitForSecondsRealtime(spawnCooldown);
            _spawnWait = false;
        }
        #endregion
    }
}
