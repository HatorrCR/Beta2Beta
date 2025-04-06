using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de la bala
    private Vector3 direction;  // Direcci�n en la que se mueve la bala

    void Start()
    {
        // La direcci�n debe ser establecida al inicio, cuando se crea la bala.
    }

    void Update()
    {
        // Mover la bala en la direcci�n establecida
        transform.position += direction * speed * Time.deltaTime;
    }

    // M�todo para establecer la direcci�n de la bala
    public void SetDirection(Vector3 targetPosition)
    {
        // Calcular la direcci�n hacia la posici�n del jugador
        direction = (targetPosition - transform.position).normalized;  // Direcci�n desde la bala hacia el jugador
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Llamar a TakeDamage en el jugador al hacer contacto
                playerHealth.TakeDamage(10, transform.position); // El da�o es 10 por ejemplo
            }

            Destroy(gameObject);  // Destruir la bala despu�s de hacer da�o
        }
    }
}
