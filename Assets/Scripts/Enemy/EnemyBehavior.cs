using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Comportamiento del Enemigo")]
    public float detectionRange = 10f;  // Rango de detecci�n del enemigo
    public float moveSpeed = 3f;        // Velocidad de movimiento hacia el jugador
    public float stopDistance = 2f;     // Distancia a la que el enemigo se detiene cerca del jugador
    public float explosionDamage = 50f; // Da�o de la explosi�n
    public float explosionDelay = 1f;   // Tiempo de parpadeo antes de la explosi�n
    public int health = 3; // Vida del enemigo


    private GameObject player;          // Referencia al jugador
    private Renderer enemyRenderer;     // Renderer del enemigo
    private Color originalColor;        // Color original del enemigo
    private bool isInRange = false;     // Si el enemigo est� en el rango de detecci�n
    private bool isExploding = false;   // Si el enemigo est� explotando
    private EnemyPatrol enemyPatrol;   // Referencia al script de patrullaje

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Buscar al jugador por su tag
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
        enemyPatrol = GetComponent<EnemyPatrol>(); // Obtener el componente EnemyPatrol
    }

    void Update()
    {
        // Verificar si el jugador est� dentro del rango de detecci�n
        if (!isExploding)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                isInRange = true;
                // Desactivar el movimiento de patrullaje si el jugador est� dentro del rango
                if (enemyPatrol != null)
                {
                    enemyPatrol.enabled = false; // Desactiva el patrullaje
                }
            }
            else
            {
                isInRange = false;
                // Activar el movimiento de patrullaje si el jugador sale del rango
                if (enemyPatrol != null && !enemyPatrol.enabled)
                {
                    enemyPatrol.enabled = true; // Reactiva el patrullaje
                }
            }

            // Si el jugador est� dentro del rango de detecci�n y no est� demasiado cerca
            if (isInRange && distanceToPlayer > stopDistance)
            {
                MoveTowardsPlayer();
            }
            else if (isInRange && distanceToPlayer <= stopDistance)
            {
                StartExplosionSequence();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Mover al enemigo hacia el jugador
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void StartExplosionSequence()
    {
        // Iniciar el parpadeo y la explosi�n despu�s de un corto retraso
        if (!isExploding)
        {
            isExploding = true;
            StartCoroutine(ExplosionEffect());
        }
    }

    System.Collections.IEnumerator ExplosionEffect()
    {
        // Parpadeo de color blanco y rojo durante 1 segundo
        float elapsedTime = 0f;
        while (elapsedTime < explosionDelay)
        {
            enemyRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            enemyRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.2f;
        }

        // Restaurar el color original antes de la explosi�n
        enemyRenderer.material.color = originalColor;

        // Explotar y hacer da�o al jugador
        Explode();
    }

    public void Explode()
    {
        // Causar da�o al jugador
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Convertir explosionDamage a int antes de aplicarlo
                // Pasar tambi�n la posici�n del enemigo como fuente del da�o
                playerHealth.TakeDamage((int)explosionDamage, transform.position); // 'transform.position' es la posici�n del enemigo
            }
        }

        // Desaparecer el enemigo
        Destroy(gameObject);
    }



    // Dibujar el rango de acci�n del enemigo en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // El color del gizmo
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Dibuja una esfera en el espacio de detecci�n
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Restar la vida del enemigo
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Eliminar el enemigo al quedarse sin vida
    }

}
