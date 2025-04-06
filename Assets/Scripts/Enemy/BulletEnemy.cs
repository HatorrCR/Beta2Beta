using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de la bala
    private Vector3 direction;  // Dirección en la que se mueve la bala

    void Start()
    {
        // La dirección debe ser establecida al inicio, cuando se crea la bala.
    }

    void Update()
    {
        // Mover la bala en la dirección establecida
        transform.position += direction * speed * Time.deltaTime;
    }

    // Método para establecer la dirección de la bala
    public void SetDirection(Vector3 targetPosition)
    {
        // Calcular la dirección hacia la posición del jugador
        direction = (targetPosition - transform.position).normalized;  // Dirección desde la bala hacia el jugador
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Llamar a TakeDamage en el jugador al hacer contacto
                playerHealth.TakeDamage(10, transform.position); // El daño es 10 por ejemplo
            }

            Destroy(gameObject);  // Destruir la bala después de hacer daño
        }
    }
}
