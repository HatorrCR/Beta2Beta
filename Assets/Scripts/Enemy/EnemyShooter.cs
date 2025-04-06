using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala
    public float shootDelay = 1f;    // Tiempo de espera entre disparos
    public float shootRange = 10f;   // Rango de disparo del enemigo
    private GameObject player;       // Referencia al jugador
    private float lastShootTime;     // Registro del �ltimo disparo

    void Start()
    {
        // Buscar al jugador por su tag (asegurarte de que el jugador tenga el tag "Player")
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Verificar si el jugador est� en el rango de visi�n del enemigo
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Si est� en rango y el tiempo para disparar ha pasado, disparar
            if (distanceToPlayer <= shootRange && Time.time >= lastShootTime + shootDelay)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Crear la bala en la posici�n del enemigo
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Asignar la direcci�n de la bala hacia la posici�n del jugador
        BulletEnemy bulletScript = bullet.GetComponent<BulletEnemy>();
        if (bulletScript != null && player != null)
        {
            // Pasamos la posici�n del jugador a la bala para que se mueva hacia all�
            bulletScript.SetDirection(player.transform.position); // Direcci�n hacia el jugador
        }

        lastShootTime = Time.time; // Registrar el momento del disparo
    }

    // Dibujar el rango de disparo en el editor (Gizmo)
    void OnDrawGizmosSelected()
    {
        // Dibujar un gizmo que muestre el rango de disparo del enemigo
        Gizmos.color = Color.red; // Color del rango de visi�n
        Gizmos.DrawWireSphere(transform.position, shootRange); // Dibuja la esfera del rango
    }
}
