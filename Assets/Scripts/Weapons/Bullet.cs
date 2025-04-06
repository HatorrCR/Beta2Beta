using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Velocidad de la bala
    public int damage = 1; // Daño base (bala normal)
    public float lifetime = 3f; // Tiempo de vida de la bala
    public bool isSpecialBullet = false; // Indica si la bala es especial

    private bool hasHit = false; // Variable para evitar múltiples impactos

    void Start()
    {
        // Si es una bala especial, cambia el daño a 10
        if (isSpecialBullet)
        {
            damage = 10;
        }

        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mover la bala hacia adelante solo si no ha impactado
        if (!hasHit)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Evitar que la bala siga moviéndose y colisionando si ya ha impactado
        if (!hasHit)
        {
            // Obtener el objeto con el que chocó
            GameObject hitObject = collision.gameObject;

            // Verificar si el objeto tiene el tag "Wall", "Floor" o "Enemy"
            if (hitObject.CompareTag("Wall") || hitObject.CompareTag("Enemy"))
            {
                // Si es un enemigo, aplicar daño
                if (hitObject.CompareTag("Enemy"))
                {
                    EnemyHealth enemy = hitObject.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        Debug.Log($"Bala impactó en {hitObject.name} e hizo {damage} de daño.");
                    }
                }

                // Marcar la bala como impactada y destruirla
                hasHit = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la bala ya ha impactado
        if (!hasHit)
        {
            // Marcar la bala como "impactada" y destruirla
            hasHit = true;
            Destroy(gameObject);
        }
    }
}
