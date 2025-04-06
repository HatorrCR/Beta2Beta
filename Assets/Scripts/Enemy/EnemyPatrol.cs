using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Puntos de Patrullaje")]
    public Transform pointA; // Primer punto de patrullaje
    public Transform pointB; // Segundo punto de patrullaje

    [Header("Configuración de Movimiento")]
    public float moveSpeed = 3f; // Velocidad de movimiento
    public float changeDirectionDistance = 1f; // Distancia para cambiar de dirección al llegar a un punto

    private Transform currentTarget; // El punto de destino actual
    private bool isGrounded; // Estado de si el enemigo está en el suelo

    void Start()
    {
        // Iniciar el patrullaje en el punto A
        currentTarget = pointA;
    }

    void Update()
    {
        // Verificar si el enemigo está tocando el suelo
        isGrounded = CheckIfGrounded();

        if (isGrounded)
        {
            MoveBetweenPoints();
        }
    }

    void MoveBetweenPoints()
    {
        // Mover al enemigo hacia el punto de destino
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Hacer que el enemigo mire en la dirección de su movimiento
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);

        // Verificar si ha llegado al punto de destino
        if (Vector3.Distance(transform.position, currentTarget.position) < changeDirectionDistance)
        {
            // Cambiar de destino entre A y B
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }
        }
    }

    bool CheckIfGrounded()
    {
        // Verificar si el enemigo está tocando el suelo usando un raycast hacia abajo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            // Si el tag del objeto con el que colisiona es "Floor", significa que está en el suelo
            if (hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }
        return false;
    }

    // Dibujar Gizmos para visualizar los puntos de patrullaje en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointA.position, 0.3f);
        Gizmos.DrawWireSphere(pointB.position, 0.3f);
    }
}
