using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxHealth;
    int currentHealth;

    [Header("Elements")]
    [SerializeField] Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = 1;

    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, currentHealth);
        currentHealth -= damage;

        float healthBarValue = (float)currentHealth / maxHealth;
        healthSlider.value = healthBarValue;

        if (currentHealth <= 0) PassAway();
    }

    void PassAway()
    {
        Debug.Log("Dead");
    }
}
