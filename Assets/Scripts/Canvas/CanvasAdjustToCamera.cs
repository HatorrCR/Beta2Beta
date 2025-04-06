using UnityEngine;

public class CanvasAdjustToCamera : MonoBehaviour
{
    public Camera mainCamera;  // La c�mara que deseas usar para ajustar el Canvas
    public Canvas canvas;      // El Canvas que deseas ajustar

    void Start()
    {
        // Asegurarse de que se tenga una c�mara si no se asigna manualmente
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (canvas != null && mainCamera != null)
        {
            // Configurar el Canvas para que se ajuste a la c�mara
            SetCanvasToCamera();
        }
        else
        {
            Debug.LogError("No se ha asignado un Canvas o una c�mara.");
        }
    }

    void SetCanvasToCamera()
    {
        // Establecer el Render Mode a "Screen Space - Camera"
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        // Asignar la c�mara para el Canvas
        canvas.worldCamera = mainCamera;

        // Ajustar la escala del Canvas para que se ajuste a la vista de la c�mara
        // Esto es �til cuando la distancia de la c�mara cambia, como en c�maras en primera persona o en 3D.
        float distance = Vector3.Distance(mainCamera.transform.position, canvas.transform.position);
        float scaleFactor = distance / 10f; // Ajusta el 10f seg�n el tama�o deseado
        canvas.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
