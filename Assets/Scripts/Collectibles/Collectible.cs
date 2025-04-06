using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1; // Cantidad que otorga al recoger
    public AudioClip collectSound; // Sonido opcional al recoger

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador toca el objeto
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddCoins(value); // Sumar la cantidad de monedas/minerales
            }

            // Reproducir sonido si está asignado
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position); // Reproducir sonido de recogida
            }

            Destroy(gameObject); // Destruir el objeto al ser recogido
        }
    }
}
