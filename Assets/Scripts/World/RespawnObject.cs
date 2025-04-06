using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [Header("Configuraci�n de Resurrecci�n")]
    public float respawnTime = 5f;  // Tiempo en segundos para reaparecer

    private Vector3 initialPosition;  // Posici�n inicial del objeto

    private void Start()
    {
        // Guardar la posici�n inicial al inicio
        initialPosition = transform.position;
    }

    public void Disappear()
    {
        // Desactivar el objeto
        gameObject.SetActive(false);

        // Iniciar la corrutina para reaparecer despu�s de un tiempo
        Invoke(nameof(Respawn), respawnTime);
    }

    private void Respawn()
    {
        // Restaurar la posici�n inicial
        transform.position = initialPosition;

        // Activar el objeto de nuevo
        gameObject.SetActive(true);
    }
}
