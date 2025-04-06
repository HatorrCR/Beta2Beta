using UnityEngine;

public class CollectibleBounce : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationSpeed = 50f;  // Velocidad de rotación en grados por segundo

    [Header("Salto")]
    public float bounceHeight = 0.2f;  // Altura del salto
    public float bounceSpeed = 2f;     // Velocidad de oscilación

    private Vector3 startPosition;

    void Start()
    {
        // Guardar la posición inicial del objeto
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotación continua alrededor del eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Movimiento de salto (efecto flotante)
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
