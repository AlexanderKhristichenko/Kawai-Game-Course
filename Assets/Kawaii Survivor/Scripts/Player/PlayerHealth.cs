using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxHealth;
    int currentHealth;

    [Header("Elements")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();

    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, currentHealth);
        currentHealth -= damage;

        UpdateUI();

        if (currentHealth <= 0) PassAway();
    }

    void PassAway()
    {
        Debug.Log("Dead");
        SceneManager.LoadScene(0);
    }

    void UpdateUI()
    {
        float healthBarValue = (float)currentHealth / maxHealth;
        healthSlider.value = healthBarValue;
        healthText.text = $"{currentHealth} / {maxHealth}";
    }
}
