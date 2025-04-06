using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton; // Bot�n para continuar
    public Button newGameButton; // Bot�n para comenzar un nuevo juego
    public Button optionsButton; // Bot�n para abrir el men� de opciones
    public Button exitButton; // Bot�n para salir del juego

    public GameDataManager gameDataManager; // Referencia al GameDataManager

    public GameObject optionsMenuPanel; // Panel del men� de opciones

    void Start()
    {
        // Si hay datos guardados, habilitar el bot�n de "Continue"
        if (gameDataManager.HasSavedData())
        {
            continueButton.gameObject.SetActive(true); // Habilitar bot�n de continuar
        }
        else
        {
            continueButton.gameObject.SetActive(false); // Deshabilitarlo si no hay datos guardados
        }

        // Asignar las acciones de los botones
        continueButton.onClick.AddListener(OnContinueButtonClick);
        newGameButton.onClick.AddListener(OnNewGameButtonClick);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Al presionar el bot�n "Continue"
    public void OnContinueButtonClick()
    {
        gameDataManager.LoadPlayerData(); // Cargar los datos del jugador

        // Cargar la escena del juego
        SceneManager.LoadScene("Mundo1"); // Nombre de la escena donde el jugador continuar�
    }

    // Al presionar el bot�n "New Game"
    public void OnNewGameButtonClick()
    {
        // Restablecer los datos del jugador y comenzar un nuevo juego
        gameDataManager.playerHealth = 100f; // Establecer salud inicial
        gameDataManager.playerPosition = Vector3.zero; // Establecer la posici�n inicial

        SceneManager.LoadScene("Mundo1"); // Cargar la escena del juego
    }

    // Funci�n para abrir el men� de opciones
    public void OpenOptionsMenu()
    {
        optionsMenuPanel.SetActive(true); // Mostrar el men� de opciones
    }

    // Funci�n para salir del juego
    public void ExitGame()
    {
        // Salir del juego
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
}
