using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Gameplay
{
    public class UnitSpawner : InteractibleBuilding
    {
        #region Variables
        [Header("Spawner properties")]
        [SerializeField] private SelectableUnit[] units;
        [SerializeField] private float innerRadius = 3f;
        [SerializeField] private float outerRadius = 5f;
        [SerializeField] private float spawnCooldown = 2f;

        private bool _spawnWait = false;
        #endregion

        #region Methods
        public void SpawnUnit(int index)
        {
            if (_spawnWait)
                return;

            /*SelectableUnit unit = Instantiate(units[index], spawnPos.position, Quaternion.LookRotation(spawnPos.position));
            Vector3 direction = transform.forward * 1.5f;
            unit.EnterMoveState(direction);*/
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(Random.Range(-outerRadius, -innerRadius), Random.Range(innerRadius, outerRadius));
            spawnPos.z = Random.Range(Random.Range(-outerRadius, -innerRadius), Random.Range(innerRadius, outerRadius));
            Vector3 direction = (spawnPos - transform.position).normalized;

            return spawnPos;
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
