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

    // Funci�n que se llama cuando se presiona el bot�n de continuar
    public void OnContinueButtonClick()
    {
        gameDataManager.LoadPlayerData();  // Cargar los datos guardados del jugador
        // Puedes aqu� agregar c�digo para cargar la escena donde el jugador continuar� jugando
        // Por ejemplo: SceneManager.LoadScene("Mundo1");
    }
}
