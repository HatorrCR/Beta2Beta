using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class SceneTriggerZone : MonoBehaviour
{
    [Header("Configuraci�n del Trigger")]
    public string sceneToLoad; // Nombre de la escena que se quiere cargar
    public bool loadOnEnter = true; // Si es true, se carga la escena cuando el jugador entra en la zona
    public bool loadOnInteract = false; // Si es true, se carga cuando el jugador interact�a (opcional)

    // Se ejecuta cuando el jugador entra en la zona del trigger
    private void OnTriggerEnter(Collider other)
    {
        if (loadOnEnter && other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    // Funci�n para cargar la escena
    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no est� definido.");
        }
    }

    // Opci�n para activar la carga por interacci�n
    public void Interact()
    {
        if (loadOnInteract)
        {
            LoadScene();
        }
    }
}
