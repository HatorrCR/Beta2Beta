using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Referencias del UI")]
    public GameObject pauseMenuPanel;
    public GameObject optionsMenuPanel;
    public Button resumeButton;
    public Button optionsButton;
    public Button saveAndQuitButton;
    public Button exitGameButton;

    private bool isPaused = false;
    public GameDataManager gameDataManager; // Referencia al GameDataManager

    void Start()
    {
        pauseMenuPanel.SetActive(false); // Aseguramos que el menú de pausa esté desactivado al inicio
        optionsMenuPanel.SetActive(false); // Aseguramos que el menú de opciones esté oculto al inicio

        // Asignar acciones a los botones
        resumeButton.onClick.AddListener(ResumeGame);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        saveAndQuitButton.onClick.AddListener(SaveAndQuit);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // Comprobar si el menú de pausa está activo pero el juego no está pausado
        if (pauseMenuPanel.activeSelf && !isPaused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);

        // Mostrar el cursor al pausar
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);

        // Ocultar el cursor al reanudar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Guardar datos del jugador y salir al menú principal
    public void SaveAndQuit()
    {
        gameDataManager.SavePlayerData(); // Guardar los datos
        SceneManager.LoadScene("Menu");  // Volver al menú principal
    }

    // Guardar y salir del juego
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");  // Volver al menú principal
    }

    // Función para abrir el menú de opciones
    public void OpenOptionsMenu()
    {
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    // Volver al menú de pausa
    public void CloseOptionsMenu()
    {
        optionsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}
