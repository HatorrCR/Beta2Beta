using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Vida inicial del enemigo
    private int currentHealth; // Salud actual

    public GameObject deathEffect; // Efecto visual de muerte (opcional)
    public float despawnTime = 0.2f; // Tiempo antes de destruir el enemigo

    [Header("Efectos de Da�o")]
    public Material blinkMaterial; // Material para el parpadeo
    private Material originalMaterial; // Material original del enemigo
    private Renderer enemyRenderer; // Referencia al Renderer del enemigo
    public float blinkDuration = 0.1f; // Duraci�n del parpadeo
    private bool isBlinking = false; // Para evitar m�ltiples parpadeos simult�neos

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
            TakeDamage(bullet.damage); // Aplicar da�o seg�n el tipo de bala
            Destroy(collision.gameObject); // Destruir la bala
        }
    }

    public void TakeDamage(int damage)
    {
        if (isBlinking) return; // Si el enemigo ya est� parpadeando, no hacer nada

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} recibi� {damage} de da�o. Vida restante: {currentHealth}");

        // Activar el parpadeo al recibir da�o
        StartCoroutine(Blink());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true; // Evitar m�ltiples parpadeos simult�neos

        if (enemyRenderer != null)
        {
            enemyRenderer.material = blinkMaterial; // Cambiar al material de parpadeo
            yield return new WaitForSeconds(blinkDuration); // Esperar la duraci�n del parpadeo
            enemyRenderer.material = originalMaterial; // Restaurar el material original
        }

        isBlinking = false; // Permitir nuevos parpadeos
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        // Instanciar efecto de muerte si est� asignado
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Desactivar el objeto antes de destruirlo
        gameObject.SetActive(false);
        Destroy(gameObject, despawnTime);
    }
}
