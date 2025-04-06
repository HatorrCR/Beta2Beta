using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Velocidad de la bala
    public int damage = 1; // Da�o base (bala normal)
    public float lifetime = 3f; // Tiempo de vida de la bala
    public bool isSpecialBullet = false; // Indica si la bala es especial

    private bool hasHit = false; // Variable para evitar m�ltiples impactos

    void Start()
    {
        // Si es una bala especial, cambia el da�o a 10
        if (isSpecialBullet)
        {
            damage = 10;
        }

        // Destruir la bala despu�s de un tiempo
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
        // Evitar que la bala siga movi�ndose y colisionando si ya ha impactado
        if (!hasHit)
        {
            // Obtener el objeto con el que choc�
            GameObject hitObject = collision.gameObject;

            // Verificar si el objeto tiene el tag "Wall", "Floor" o "Enemy"
            if (hitObject.CompareTag("Wall") || hitObject.CompareTag("Enemy"))
            {
                // Si es un enemigo, aplicar da�o
                if (hitObject.CompareTag("Enemy"))
                {
                    EnemyHealth enemy = hitObject.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        Debug.Log($"Bala impact� en {hitObject.name} e hizo {damage} de da�o.");
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
