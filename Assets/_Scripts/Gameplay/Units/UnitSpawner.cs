using System;
using UnityEngine;
using System.Collections;
using Scripts.Gameplay.Units;
using ScriptableObjects;

namespace Scripts.Gameplay.Building
{
    public class UnitSpawner : InteractibleBuilding
    {
        #region Variables
        [Header("Spawner properties")]
        [SerializeField] private UnitProperties[] units;
        [SerializeField] private SpawnUnitButton[] buttons;
        [SerializeField] private UpgradeDatas[] resources;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private float spawnCooldown = 2f;
        [SerializeField] private bool goRight = true;

        private bool _spawnWait = false;
        #endregion

        #region Builts_In
        private void Start()
        {
            for (int i = 0; i < units.Length; i++)
                buttons[i].SetUnit(units[i], resources[i]);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Spawn a new unit
        /// </summary>
        public void SpawnUnit(UnitProperties unit)
        {
            if (_spawnWait)
                return;

            int index = Array.FindIndex(units, x => x == unit);
            SelectableUnit instance = Instantiate(units[index].Prefab, spawnPos.position, Quaternion.LookRotation(spawnPos.position));
            Vector3 direction = goRight ? spawnPos.right * 1.5f : spawnPos.right * -1.5f;
            instance.EnterMoveState(spawnPos.transform.position + direction);

            StartCoroutine(SpawnRoutine());
        }

        /// <summary>
        /// Spawn wait
        /// </summary>
        private IEnumerator SpawnRoutine()
        {
            _spawnWait = true;
            yield return new WaitForSecondsRealtime(spawnCooldown);
            _spawnWait = false;
        }
        #endregion
    }
}
