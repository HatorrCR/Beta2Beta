using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [Header("Configuración del Crosshair")]
    public RectTransform crosshair;  // Referencia al crosshair de la UI
    public float minSize = 0.5f;     // Tamaño mínimo del crosshair
    public float maxSize = 2f;       // Tamaño máximo del crosshair
    public float smoothSpeed = 5f;   // Velocidad de suavizado del cambio de tamaño

    [Header("Configuración del Raycast")]
    public float maxDistance = 100f; // Distancia máxima para el raycast
    public LayerMask interactableLayer;  // Capa para filtrar los objetos con los que interactuar

    [Header("Cámara")]
    public Camera playerCamera;      // Cámara del jugador

    private void Update()
    {
        // Llamar a la función para ajustar el tamaño del crosshair
        AdjustCrosshairSize();
    }

    private void AdjustCrosshairSize()
    {
        // Realizar un raycast desde el centro de la cámara (punto de visión del jugador)
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Raycast desde el centro de la pantalla

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            // Si el raycast golpea un objeto, usamos la distancia a ese objeto para ajustar el tamaño del crosshair
            float distanceToObject = hit.distance;
            float targetSize = Mathf.Clamp(distanceToObject, 1f, maxDistance) / maxDistance; // Normalizar la distancia entre el objeto y el jugador
            targetSize = Mathf.Lerp(minSize, maxSize, targetSize); // Ajustamos el tamaño del crosshair en función de la distancia
            crosshair.localScale = Vector3.Lerp(crosshair.localScale, new Vector3(targetSize, targetSize, 1), Time.deltaTime * smoothSpeed); // Suavizamos el cambio de tamaño
        }
        else
        {
            // Si no golpea nada, devolvemos el crosshair al tamaño más pequeño o predeterminado
            crosshair.localScale = Vector3.Lerp(crosshair.localScale, new Vector3(minSize, minSize, 1), Time.deltaTime * smoothSpeed);
        }
    }
}
