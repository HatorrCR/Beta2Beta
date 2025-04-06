using UnityEngine;

public class CanvasAlwaysOnTop : MonoBehaviour
{
    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();

        // Asegurar que este objeto tiene el tag "UI"
        if (!CompareTag("UI"))
        {
            Debug.LogWarning("El objeto Canvas no tiene el tag 'UI'. Asignándolo automáticamente.");
            gameObject.tag = "UI"; // Asignar el tag si no está correctamente configurado
        }

        // Forzar que el Canvas esté siempre en primer plano
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main; // Asignar la cámara principal
            canvas.planeDistance = 1f; // Ajustar la distancia para que no se esconda detrás de objetos
        }

        // Asegurar un sorting order alto para que esté por encima de otros elementos
        canvas.sortingOrder = 1000;
    }
}
