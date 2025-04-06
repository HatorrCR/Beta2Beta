using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Vida inicial del enemigo
    private int currentHealth; // Salud actual

    public GameObject deathEffect; // Efecto visual de muerte (opcional)
    public float despawnTime = 0.2f; // Tiempo antes de destruir el enemigo

    [Header("Efectos de Daño")]
    public Material blinkMaterial; // Material para el parpadeo
    private Material originalMaterial; // Material original del enemigo
    private Renderer enemyRenderer; // Referencia al Renderer del enemigo
    public float blinkDuration = 0.1f; // Duración del parpadeo
    private bool isBlinking = false; // Para evitar múltiples parpadeos simultáneos

    void Start()
    {
        currentHealth = health; // Inicializar la salud
        enemyRenderer = GetComponent<Renderer>(); // Obtener el Renderer del enemigo
        if (enemyRenderer != null)
        {
            originalMaterial = enemyRenderer.material; // Guardar el material original
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Obtener el componente Bullet (sin depender del tag)
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            TakeDamage(bullet.damage); // Aplicar daño según el tipo de bala
            Destroy(collision.gameObject); // Destruir la bala
        }
    }

    public void TakeDamage(int damage)
    {
        if (isBlinking) return; // Si el enemigo ya está parpadeando, no hacer nada

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibió {damage} de daño. Vida restante: {currentHealth}");

        // Activar el parpadeo al recibir daño
        StartCoroutine(Blink());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true; // Evitar múltiples parpadeos simultáneos

        if (enemyRenderer != null)
        {
            enemyRenderer.material = blinkMaterial; // Cambiar al material de parpadeo
            yield return new WaitForSeconds(blinkDuration); // Esperar la duración del parpadeo
            enemyRenderer.material = originalMaterial; // Restaurar el material original
        }

        isBlinking = false; // Permitir nuevos parpadeos
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        // Instanciar efecto de muerte si está asignado
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Desactivar el objeto antes de destruirlo
        gameObject.SetActive(false);
        Destroy(gameObject, despawnTime);
    }
}
