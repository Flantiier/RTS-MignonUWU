using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    #region Variables
    [Header("Entity properties")]
    [SerializeField] private float health = 100f;
    [SerializeField] private Slider healthSlider;
    #endregion

    #region Properties
    public float CurrentHealth { get; protected set; }
    #endregion

    #region Builts_In
    protected virtual void OnEnable()
    {
        InitHealthSlider();
        FullHealth();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Reset the health value to its maximum
    /// </summary>
    protected void FullHealth()
    {
        CurrentHealth = health;
        UpdateHealthSlider();
    }

    /// <summary>
    /// Increase the health value by a given amount
    /// </summary>
    /// <param name="amount"> Health gained </param>
    protected void RestoreHealth(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, health);
        UpdateHealthSlider();
    }

    /// <summary>
    /// Decrease the current health value
    /// </summary>
    /// <param name="damages"> Damages taken </param>
    public virtual void DealDamages(float damages)
    {
        //Decrease health
        CurrentHealth -= damages;
        UpdateHealthSlider();

        //Health lower than 0
        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }

    /// <summary>
    /// Initialize the maximum slider value
    /// </summary>
    private void InitHealthSlider()
    {
        if (!healthSlider)
            return;

        healthSlider.maxValue = health;
    }

    /// <summary>
    /// Set the current value of the slider
    /// </summary>
    private void UpdateHealthSlider()
    {
        if (!healthSlider)
            return;

        healthSlider.value = CurrentHealth;
    }
    #endregion
}
