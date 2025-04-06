using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [Header("Referencias a la Salud")]
    public PlayerHealth playerHealth; // Referencia al script de salud del jugador

    // Claves para guardar los datos en PlayerPrefs
    private string healthKey = "PlayerHealth";
    private int initialHealth; // Salud inicial del jugador

    void Start()
    {
        // Guardar la salud inicial al comenzar la escena
        SaveInitialHealth();

        // Cargar la salud guardada
        LoadHealth();
    }

    public void SaveInitialHealth()
    {
        // Guardamos la salud inicial al comenzar la escena
        initialHealth = playerHealth.maxHealth;

        // Guardamos la salud inicial en PlayerPrefs
        PlayerPrefs.SetInt(healthKey, initialHealth);
        PlayerPrefs.Save(); // Aseguramos que los cambios se guarden
    }

    public void LoadHealth()
    {
        // Cargar la salud guardada, pero asegurándonos de que no sea menor que 1
        int savedHealth = PlayerPrefs.GetInt(healthKey, initialHealth);
        playerHealth.currentHealth = savedHealth;

        // Actualizar la UI después de cargar la salud
        playerHealth.UpdateHealthUI();
    }

    // Función para reiniciar la escena cuando la vida llega a 0
    public void OnPlayerDeath()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Si quieres resetear la salud cuando el jugador reaparece
    public void ResetHealthOnRespawn()
    {
        playerHealth.currentHealth = initialHealth;
        playerHealth.UpdateHealthUI(); // Asegurar que la UI se actualiza
    }
}
