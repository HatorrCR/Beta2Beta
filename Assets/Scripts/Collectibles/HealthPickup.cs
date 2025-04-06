using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 25; // Cantidad de vida que cura
    public AudioClip pickupSound; // Sonido opcional

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verificar que sea el jugador
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Verificar si la salud del jugador no está completa
                if (playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    // Curar al jugador
                    playerHealth.Heal(healAmount);

                    // Reproducir sonido si está asignado
                    if (pickupSound != null)
                    {
                        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                    }

                    // Destruir el objeto de curación
                    Destroy(gameObject);
                }
                else
                {
                    // Si la vida está completa, no recoger el botiquín (opcional: mensaje de debug)
                    Debug.Log("Vida completa, no se puede recoger el botiquín.");
                }
            }
        }
    }
}
