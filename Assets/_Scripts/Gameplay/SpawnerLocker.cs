using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Building;
using Scripts.Gameplay.Units;

public class SpawnerLocker : MonoBehaviour
{
	#region Variables
	[SerializeField] private UnitSpawner spawner;
	[SerializeField] private List<EnemyUnit> enemies;
    #endregion

    #region Builts_In
    private void Update()
    {
		CheckEnemiesList();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Check if all the enemies are defeated
    /// </summary>
    private void CheckEnemiesList()
	{
		if (enemies.Count <= 0)
			return;

		for (int i = 0; i < enemies.Count; i++)
		{
			EnemyUnit enemy = enemies[i];

            if (!enemy || enemy.CurrentHealth <= 0)
			{
				enemies.RemoveAt(i);
				continue;
			}
        }

        UnlockSpawner();
    }

    /// <summary>
    /// Unlock the spawner when all the enemies are defeated
    /// </summary>
    private void UnlockSpawner()
	{
		if (enemies.Count > 0)
			return;

		spawner.IsPlaced = true;
		enemies.Clear();
		enabled = false;
	}
	#endregion
}
