using UnityEngine;

public class ExitOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Saliendo del juego...");
            QuitGame();
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        // Para salir del modo de juego en el editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Para cerrar el juego en una build
        Application.Quit();
#endif
    }
}
