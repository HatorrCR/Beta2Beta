using UnityEngine;

public class CanvasAdjustToCamera : MonoBehaviour
{
    public Camera mainCamera;  // La cámara que deseas usar para ajustar el Canvas
    public Canvas canvas;      // El Canvas que deseas ajustar

    void Start()
    {
        // Asegurarse de que se tenga una cámara si no se asigna manualmente
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (canvas != null && mainCamera != null)
        {
            // Configurar el Canvas para que se ajuste a la cámara
            SetCanvasToCamera();
        }
        else
        {
            Debug.LogError("No se ha asignado un Canvas o una cámara.");
        }
    }

    void SetCanvasToCamera()
    {
        // Establecer el Render Mode a "Screen Space - Camera"
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        // Asignar la cámara para el Canvas
        canvas.worldCamera = mainCamera;

        // Ajustar la escala del Canvas para que se ajuste a la vista de la cámara
        // Esto es útil cuando la distancia de la cámara cambia, como en cámaras en primera persona o en 3D.
        float distance = Vector3.Distance(mainCamera.transform.position, canvas.transform.position);
        float scaleFactor = distance / 10f; // Ajusta el 10f según el tamaño deseado
        canvas.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
