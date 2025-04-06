using UnityEngine;

public class CollectibleBounce : MonoBehaviour
{
    [Header("Rotaci�n")]
    public float rotationSpeed = 50f;  // Velocidad de rotaci�n en grados por segundo

    [Header("Salto")]
    public float bounceHeight = 0.2f;  // Altura del salto
    public float bounceSpeed = 2f;     // Velocidad de oscilaci�n

    private Vector3 startPosition;

    void Start()
    {
        // Guardar la posici�n inicial del objeto
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotaci�n continua alrededor del eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Movimiento de salto (efecto flotante)
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
