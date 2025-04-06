using UnityEngine;

public class UIRenderOrder : MonoBehaviour
{
    void Start()
    {
        // Buscar todos los objetos en la capa "UI"
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");

        foreach (GameObject uiObject in uiObjects)
        {
            Canvas canvas = uiObject.GetComponent<Canvas>();

            if (canvas != null)
            {
                // Configurar el canvas para que siempre esté en primer plano
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 1000;  // Un valor alto para que siempre esté por encima
            }
        }
    }
}
