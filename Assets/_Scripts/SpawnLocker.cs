using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnLocker : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<EnemyUnit> units;
    [SerializeField] private UnitSpawner spawner;
    #endregion

    #region Builts_In
    private void Awake()
    {
        if (!spawner)
            return;

        spawner.SetCollider(false);
    }

    private void Update()
    {
        CheckSpawnerState();
    }
    #endregion

    #region Methods
    private void CheckSpawnerState()
    {
        if (units.Count <= 0)
        {
            EnableSpawner();
            gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < units.Count; i++)
        {
            EnemyUnit unit = units[i];

            //Remove from list if no health
            if (!unit)
            {
                units.RemoveAt(i);
                continue;
            }

            if (unit.CurrentHealth <= 0)
                units.Remove(unit);
            else
                continue;
        }
    }

    private void EnableSpawner()
    {
        if (!spawner)
            return;

        spawner.SetCollider(true);
    }
    #endregion
}
