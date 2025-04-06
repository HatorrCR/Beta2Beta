using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton; // Botón para continuar
    public Button newGameButton; // Botón para comenzar un nuevo juego
    public Button optionsButton; // Botón para abrir el menú de opciones
    public Button exitButton; // Botón para salir del juego

    public GameDataManager gameDataManager; // Referencia al GameDataManager

    public GameObject optionsMenuPanel; // Panel del menú de opciones

    void Start()
    {
        // Si hay datos guardados, habilitar el botón de "Continue"
        if (gameDataManager.HasSavedData())
        {
            continueButton.gameObject.SetActive(true); // Habilitar botón de continuar
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

    // Al presionar el botón "Continue"
    public void OnContinueButtonClick()
    {
        gameDataManager.LoadPlayerData(); // Cargar los datos del jugador

        // Cargar la escena del juego
        SceneManager.LoadScene("Mundo1"); // Nombre de la escena donde el jugador continuará
    }

    // Al presionar el botón "New Game"
    public void OnNewGameButtonClick()
    {
        // Restablecer los datos del jugador y comenzar un nuevo juego
        gameDataManager.playerHealth = 100f; // Establecer salud inicial
        gameDataManager.playerPosition = Vector3.zero; // Establecer la posición inicial

        SceneManager.LoadScene("Mundo1"); // Cargar la escena del juego
    }

    // Función para abrir el menú de opciones
    public void OpenOptionsMenu()
    {
        optionsMenuPanel.SetActive(true); // Mostrar el menú de opciones
    }

    // Función para salir del juego
    public void ExitGame()
    {
        // Salir del juego
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
}
