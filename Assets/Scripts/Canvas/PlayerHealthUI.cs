using UnityEngine;
using UnityEngine.UI; // Para la UI
using TMPro; // Si usas TextMeshPro

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referencias a la UI")]
    public Slider healthBar; // Barra de vida
    public TextMeshProUGUI healthText; // Texto de vida (cambiar a Text si no usas TMP)

    private PlayerHealth playerHealth; // Referencia al script de salud del jugador

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // Buscar automáticamente el script de salud en la escena

        if (playerHealth != null)
        {
            UpdateHealthUI(); // Actualizar al inicio
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            UpdateHealthUI();
        }
    }

    public void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = playerHealth.currentHealth + " / " + playerHealth.maxHealth;
        }
    }
}
