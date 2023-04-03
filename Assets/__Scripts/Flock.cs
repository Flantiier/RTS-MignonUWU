using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private int unitsCount = 20;
    [SerializeField] private Vector3 boundingBox = Vector3.one;
    [SerializeField] private float spawnOffset;

    [SerializeField] private List<GameObject> boids = new List<GameObject>();

    private void Awake()
    {
        CreateFlockUnits();
    }

    private void CreateFlockUnits()
    {
        if (!unitPrefab)
            return;

        for (int i = 0; i < unitsCount; i++)
        {
            Vector3 randomPosition = RandomPosition(boundingBox) + new Vector3(0f, spawnOffset, 0f);
            Quaternion randomRotation = RandomRotation();
            GameObject instance = Instantiate(unitPrefab, randomPosition, randomRotation);

            boids.Add(instance);
        }
    }

    private Vector3 RandomPosition(Vector3 bounds)
    {
        return new Vector3(RandomValue(-bounds.x, bounds.x), RandomValue(-bounds.y, bounds.y), RandomValue(-bounds.z, bounds.z));
    }

    private Quaternion RandomRotation()
    {
        return Quaternion.Euler(0f, RandomValue(0, 360), 0f);
    }

    private float RandomValue(float min, float max)
    {
        return Random.Range(min, max);
    }
}
