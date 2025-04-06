using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala
    public float shootDelay = 1f;    // Tiempo de espera entre disparos
    public float shootRange = 10f;   // Rango de disparo del enemigo
    private GameObject player;       // Referencia al jugador
    private float lastShootTime;     // Registro del último disparo

    void Start()
    {
        // Buscar al jugador por su tag (asegurarte de que el jugador tenga el tag "Player")
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Verificar si el jugador está en el rango de visión del enemigo
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Si está en rango y el tiempo para disparar ha pasado, disparar
            if (distanceToPlayer <= shootRange && Time.time >= lastShootTime + shootDelay)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Crear la bala en la posición del enemigo
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Asignar la dirección de la bala hacia la posición del jugador
        BulletEnemy bulletScript = bullet.GetComponent<BulletEnemy>();
        if (bulletScript != null && player != null)
        {
            // Pasamos la posición del jugador a la bala para que se mueva hacia allí
            bulletScript.SetDirection(player.transform.position); // Dirección hacia el jugador
        }

        lastShootTime = Time.time; // Registrar el momento del disparo
    }

    // Dibujar el rango de disparo en el editor (Gizmo)
    void OnDrawGizmosSelected()
    {
        // Dibujar un gizmo que muestre el rango de disparo del enemigo
        Gizmos.color = Color.red; // Color del rango de visión
        Gizmos.DrawWireSphere(transform.position, shootRange); // Dibuja la esfera del rango
    }
}
