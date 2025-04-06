using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [Header("Configuración de Resurrección")]
    public float respawnTime = 5f;  // Tiempo en segundos para reaparecer

    private Vector3 initialPosition;  // Posición inicial del objeto

    private void Start()
    {
        // Guardar la posición inicial al inicio
        initialPosition = transform.position;
    }

    public void Disappear()
    {
        // Desactivar el objeto
        gameObject.SetActive(false);

        // Iniciar la corrutina para reaparecer después de un tiempo
        Invoke(nameof(Respawn), respawnTime);
    }

    private void Respawn()
    {
        // Restaurar la posición inicial
        transform.position = initialPosition;

        // Activar el objeto de nuevo
        gameObject.SetActive(true);
    }
}
