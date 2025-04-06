using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance { get; private set; }

    void Awake()
    {
        // Singleton para asegurar que solo haya una instancia del SceneManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Asegurarse de que el GameManager no sea destruido al cargar nuevas escenas
        DontDestroyOnLoad(gameObject);
    }

    // M�todo para cargar una escena por su nombre
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Cargar la escena
            SceneManager.LoadScene(sceneName);

            // Asegurarse de que el juego est� en "Play" (tiempo normal) al cargar la escena
            Time.timeScale = 1f;  // Reactivar el tiempo si estaba pausado
        }
        else
        {
            Debug.LogWarning("No se ha especificado un nombre de escena.");
        }
    }
}
