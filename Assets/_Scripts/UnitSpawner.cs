using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private SelectableUnit[] units;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject spawnerUI;
    [SerializeField] private Collider collider;
    [SerializeField] private LayerMask unitMask;
    [SerializeField] private bool goLeft = false;
    [SerializeField] private float radius = 0.5f;

    private void Awake()
    {
        spawnerUI.SetActive(false);
    }

    public void SpawnUnit(int index)
    {
        if (index < 0 || index >= units.Length)
            return;

        if (Physics.CheckSphere(spawnPosition.position, 0.5f, unitMask))
            return;

        SelectableUnit unit = units[index];
        Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        SelectableUnit instance = Instantiate(unit, spawnPosition.position, randomRotation);

        Vector3 direction = goLeft ? -Vector3.right : Vector3.right;
        instance.EnterMoveState(spawnPosition.position + direction * 2f);
    }

    private void OnMouseDown()
    {
        if (!spawnerUI)
            return;

        spawnerUI.gameObject.SetActive(!spawnerUI.activeSelf);
    }

    public void SetCollider(bool state)
    {
        collider.enabled = state;
    }
}
