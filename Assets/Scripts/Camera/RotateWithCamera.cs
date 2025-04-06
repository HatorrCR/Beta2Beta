using UnityEngine;

public class RotateToCameraCenter : MonoBehaviour
{
    public Transform playerBody;  // El cuerpo del jugador que rotará
    public Camera playerCamera;   // Referencia a la cámara del jugador
    public float rotationSpeed = 10f; // Velocidad de rotación

    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        // Calcular la dirección hacia el centro de la cámara
        Vector3 direction = (playerCamera.transform.position - playerBody.position).normalized;
        direction.y = 0f; // Ignorar cualquier rotación vertical

        // Si la dirección tiene alguna magnitud, rotar el jugador hacia ella
        if (direction.magnitude >= 0.1f)
        {
            // Calcular la rotación que debe tener el jugador hacia el centro de la cámara
            Quaternion toRotation = Quaternion.LookRotation(direction);

            // Rotar suavemente el cuerpo del jugador hacia el objetivo
            playerBody.rotation = Quaternion.RotateTowards(playerBody.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
