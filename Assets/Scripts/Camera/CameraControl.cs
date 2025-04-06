using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; // Referencia al jugador
    public Vector3 offset = new Vector3(0.5f, 1.5f, -3f); // Posici�n de la c�mara respecto al jugador
    public float sensitivity = 3.0f; // Sensibilidad del mouse
    public float smoothSpeed = 10f; // Velocidad de suavizado de la c�mara
    public float minYAngle = -30f, maxYAngle = 60f; // L�mites de inclinaci�n
    public LayerMask collisionMask; // M�scara de colisi�n para la c�mara

    public float rotationSpeed = 5f; // Velocidad a la que el jugador rota hacia la direcci�n del crosshair

    private float yaw = 0f; // Rotaci�n horizontal de la c�mara
    private float pitch = 0f; // Rotaci�n vertical de la c�mara

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Capturar la entrada del mouse para controlar la rotaci�n
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle); // Limitar la inclinaci�n de la c�mara

        // Rotar la c�mara alrededor del jugador
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Manejo de colisiones (evitar que la c�mara atraviese objetos)
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, collisionMask))
        {
            // Si la c�mara colisiona con algo, ajustamos la posici�n para evitar atravesar objetos
            if (hit.collider.CompareTag("Wall"))  // Verificamos si el objeto tiene el tag "Wall"
            {
                // Ajustar la posici�n de la c�mara ligeramente detr�s del punto de colisi�n
                desiredPosition = hit.point + (hit.normal * 0.2f); // Ajuste para evitar que atraviese
            }
        }

        // Suavizar la transici�n de la c�mara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position + Vector3.up * 1.5f); // Apuntar ligeramente por encima del jugador

        // Rotar al jugador hacia el crosshair
        RotatePlayerTowardsCrosshair();
    }

    private void RotatePlayerTowardsCrosshair()
    {
        // Obtener la posici�n del crosshair en el mundo
        Vector3 crosshairDirection = GetCrosshairWorldPosition() - transform.position;
        crosshairDirection.y = 0; // Ignorar la rotaci�n en el eje Y

        // Rotar al jugador hacia el crosshair
        if (crosshairDirection.magnitude > 0.1f) // Evitar movimientos cuando no hay direcci�n
        {
            Quaternion targetRotation = Quaternion.LookRotation(crosshairDirection);
            target.rotation = Quaternion.Slerp(target.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private Vector3 GetCrosshairWorldPosition()
    {
        // Obtener la posici�n en el mundo del crosshair desde la c�mara
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point; // Si el raycast encuentra algo, devuelve el punto de impacto
        }

        // Si no hay impacto, devolver un punto distante en la direcci�n del rayo
        return ray.GetPoint(1000f);
    }
}
