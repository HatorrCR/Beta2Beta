using UnityEngine;
using System.Collections;

public class FallTrigger : MonoBehaviour
{
    public int fallDamage = 10; // Da�o que recibe el jugador al caer
    public Vector3 damageSourcePosition; // La posici�n desde la que ocurre el da�o (puede ser la posici�n del trigger)

    [Header("Opciones de Teletransporte")]
    public Transform teleportTarget; // La plataforma a la que se teletransportar� el jugador despu�s de recibir da�o
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
        if (other.CompareTag("Player") && !isTeleporting) // Aseg�rate de que el tag sea "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Llamamos a TakeDamage pasando el da�o y la posici�n del trigger
                playerHealth.TakeDamage(fallDamage, transform.position); // 'transform.position' es la posici�n del trigger

                // Iniciar teletransporte al objetivo (si est� configurado)
                if (teleportTarget != null)
                {
                    // Desactivamos temporalmente el trigger para evitar m�ltiples activaciones
                    triggerCollider.enabled = false;

                    // Iniciar corrutina para teletransportar despu�s del da�o
                    StartCoroutine(Teleport(other.gameObject));
                }
            }
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        isTeleporting = true;

        // Opci�n de retraso antes del teletransporte (si lo deseas)
        yield return new WaitForSeconds(teleportDelay);

        // Teletransportar al jugador a la plataforma de destino
        player.transform.position = teleportTarget.position;

        // Esperar hasta el siguiente FixedUpdate para asegurar la estabilidad del teletransporte
        yield return new WaitForFixedUpdate();

        // Reactivar el trigger despu�s del teletransporte
        triggerCollider.enabled = true;

        isTeleporting = false;
    }
}
