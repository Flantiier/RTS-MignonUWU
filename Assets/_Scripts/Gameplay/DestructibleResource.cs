using UnityEngine;

public class DestructibleResource : Entity
{
    #region Variables
    [Header("Resource properties")]
    [SerializeField] private Resource resource;
    [SerializeField] private int amount = 25;

    private Animator _animator;
    private Collider _collider;

    public Resource Resource => resource;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
    }
    #endregion

    #region Methods
    protected override void HandleDeath()
    {
        resource.amount += amount;
        healthSlider.transform.parent.gameObject.SetActive(false);

        _animator.enabled = true;
        _collider.enabled = false;

        Destroy(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length + 0.1f);
    }
    #endregion
}
