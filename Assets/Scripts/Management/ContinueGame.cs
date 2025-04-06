using UnityEngine;

public class ContinueGame : MonoBehaviour
{
    public GameDataManager gameDataManager;  // Referencia al GameDataManager

    void Start()
    {
        if (gameDataManager == null)
        {
            Debug.LogError("No se ha asignado GameDataManager en ContinueGame.");
        }
    }

    // Función que se llama cuando se presiona el botón de continuar
    public void OnContinueButtonClick()
    {
        gameDataManager.LoadPlayerData();  // Cargar los datos guardados del jugador
        // Puedes aquí agregar código para cargar la escena donde el jugador continuará jugando
        // Por ejemplo: SceneManager.LoadScene("Mundo1");
    }
}
