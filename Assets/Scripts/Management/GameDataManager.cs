using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    // Variables para la vida y la posición
    public float playerHealth = 100f;
    public Vector3 playerPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Guardar datos del jugador
    public void SavePlayerData()
    {
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.z);
        PlayerPrefs.Save();
    }

    // Cargar datos del jugador
    public void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("PlayerHealth") && PlayerPrefs.HasKey("PlayerPositionX"))
        {
            playerHealth = PlayerPrefs.GetFloat("PlayerHealth");
            playerPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerPositionX"),
                PlayerPrefs.GetFloat("PlayerPositionY"),
                PlayerPrefs.GetFloat("PlayerPositionZ")
            );
        }
    }

    // Verificar si hay datos guardados
    public bool HasSavedData()
    {
        return PlayerPrefs.HasKey("PlayerHealth") && PlayerPrefs.HasKey("PlayerPositionX");
    }
}
