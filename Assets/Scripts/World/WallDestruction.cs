using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    // Este método se llama cuando algo entra en el trigger del muro
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger es una bala especial
        if (other.CompareTag("SpecialBullet"))
        {
            // Destruir el muro al ser golpeado por la bala especial
            Destroy(gameObject);
        }
    }
}
