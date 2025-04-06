using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Salud del jugador")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI de la salud")]
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public Image healthBarFill;

    [Header("Colores de la barra de vida")]
    public Gradient healthGradient;

    [Header("Efectos de daño")]
    public float invincibilityTime = 0.2f;
    public float knockbackForce = 10f;
    public float knockbackMultiplier = 0.2f;
    public Material blinkMaterial;
    private Material originalMaterial;

    [Header("Reinicio de escena")]
    public float restartDelay = 2f; // ⬅️ Tiempo antes de recargar la escena tras morir

    private bool isInvincible = false;
    private Rigidbody rb;
    private Vector3 knockbackDirection;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        originalMaterial = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int amount, Vector3 damageSourcePosition)
    {
        if (isInvincible) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        StartCoroutine(InvincibilityCoroutine());
        Knockback(damageSourcePosition, amount);
        StartCoroutine(Blink());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Knockback(Vector3 damageSourcePosition, int damage)
    {
        if (rb != null)
        {
            float adjustedKnockback = knockbackForce + (damage * knockbackMultiplier);
            knockbackDirection = transform.position - damageSourcePosition;
            knockbackDirection.y = 0;
            knockbackDirection.Normalize();
            rb.AddForce(knockbackDirection * adjustedKnockback, ForceMode.Impulse);
        }
    }

    private System.Collections.IEnumerator Blink()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = blinkMaterial;
            yield return new WaitForSeconds(0.1f);
            renderer.material = originalMaterial;
        }
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();
        }
        else
        {
            Debug.Log("La vida está completa, no se puede curar más.");
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp((int)health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBar.value = healthPercentage;

            if (healthBarFill != null)
            {
                healthBarFill.color = healthGradient.Evaluate(healthPercentage);
            }
        }

        if (healthText != null)
        {
            healthText.text = currentHealth + " / " + maxHealth;
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("El jugador ha muerto.");

        // Reiniciar escena tras retardo
        Invoke(nameof(RestartScene), restartDelay);
    }

    private void RestartScene()
    {
        isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
