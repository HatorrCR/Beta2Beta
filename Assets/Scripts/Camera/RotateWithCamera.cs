using UnityEngine;

public class RotateToCameraCenter : MonoBehaviour
{
    public Transform playerBody;  // El cuerpo del jugador que rotar�
    public Camera playerCamera;   // Referencia a la c�mara del jugador
    public float rotationSpeed = 10f; // Velocidad de rotaci�n

    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        // Calcular la direcci�n hacia el centro de la c�mara
        Vector3 direction = (playerCamera.transform.position - playerBody.position).normalized;
        direction.y = 0f; // Ignorar cualquier rotaci�n vertical

        // Si la direcci�n tiene alguna magnitud, rotar el jugador hacia ella
        if (direction.magnitude >= 0.1f)
        {
            // Calcular la rotaci�n que debe tener el jugador hacia el centro de la c�mara
            Quaternion toRotation = Quaternion.LookRotation(direction);

            // Rotar suavemente el cuerpo del jugador hacia el objetivo
            playerBody.rotation = Quaternion.RotateTowards(playerBody.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
