using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [Header("Configuraci�n del Crosshair")]
    public RectTransform crosshair;  // Referencia al crosshair de la UI
    public float minSize = 0.5f;     // Tama�o m�nimo del crosshair
    public float maxSize = 2f;       // Tama�o m�ximo del crosshair
    public float smoothSpeed = 5f;   // Velocidad de suavizado del cambio de tama�o

    [Header("Configuraci�n del Raycast")]
    public float maxDistance = 100f; // Distancia m�xima para el raycast
    public LayerMask interactableLayer;  // Capa para filtrar los objetos con los que interactuar

    [Header("C�mara")]
    public Camera playerCamera;      // C�mara del jugador

    private void Update()
    {
        // Llamar a la funci�n para ajustar el tama�o del crosshair
        AdjustCrosshairSize();
    }

    private void AdjustCrosshairSize()
    {
        // Realizar un raycast desde el centro de la c�mara (punto de visi�n del jugador)
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Raycast desde el centro de la pantalla

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            // Si el raycast golpea un objeto, usamos la distancia a ese objeto para ajustar el tama�o del crosshair
            float distanceToObject = hit.distance;
            float targetSize = Mathf.Clamp(distanceToObject, 1f, maxDistance) / maxDistance; // Normalizar la distancia entre el objeto y el jugador
            targetSize = Mathf.Lerp(minSize, maxSize, targetSize); // Ajustamos el tama�o del crosshair en funci�n de la distancia
            crosshair.localScale = Vector3.Lerp(crosshair.localScale, new Vector3(targetSize, targetSize, 1), Time.deltaTime * smoothSpeed); // Suavizamos el cambio de tama�o
        }
        else
        {
            // Si no golpea nada, devolvemos el crosshair al tama�o m�s peque�o o predeterminado
            crosshair.localScale = Vector3.Lerp(crosshair.localScale, new Vector3(minSize, minSize, 1), Time.deltaTime * smoothSpeed);
        }
    }
}
