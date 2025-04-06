using UnityEngine;
using System.Collections;

public class FallTrigger : MonoBehaviour
{
    public int fallDamage = 10; // Daño que recibe el jugador al caer
    public Vector3 damageSourcePosition; // La posición desde la que ocurre el daño (puede ser la posición del trigger)

    [Header("Opciones de Teletransporte")]
    public Transform teleportTarget; // La plataforma a la que se teletransportará el jugador después de recibir daño
    public float teleportDelay = 0.5f; // Tiempo de retraso antes de teletransportarse (opcional)

    private bool isTeleporting = false;
    private Collider triggerCollider; // Referencia al collider del trigger

    private void Start()
    {
        // Guardamos la referencia del collider
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting) // Asegúrate de que el tag sea "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Llamamos a TakeDamage pasando el daño y la posición del trigger
                playerHealth.TakeDamage(fallDamage, transform.position); // 'transform.position' es la posición del trigger

                // Iniciar teletransporte al objetivo (si está configurado)
                if (teleportTarget != null)
                {
                    // Desactivamos temporalmente el trigger para evitar múltiples activaciones
                    triggerCollider.enabled = false;

                    // Iniciar corrutina para teletransportar después del daño
                    StartCoroutine(Teleport(other.gameObject));
                }
            }
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        isTeleporting = true;

        // Opción de retraso antes del teletransporte (si lo deseas)
        yield return new WaitForSeconds(teleportDelay);

        // Teletransportar al jugador a la plataforma de destino
        player.transform.position = teleportTarget.position;

        // Esperar hasta el siguiente FixedUpdate para asegurar la estabilidad del teletransporte
        yield return new WaitForFixedUpdate();

        // Reactivar el trigger después del teletransporte
        triggerCollider.enabled = true;

        isTeleporting = false;
    }
}
