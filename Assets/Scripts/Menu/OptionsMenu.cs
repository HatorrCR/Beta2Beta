using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para usar TextMeshPro

public class OptionsMenu : MonoBehaviour
{
    [Header("Configuraci�n de Sonido")]
    public Slider volumeSlider; // Control deslizante para el volumen
    public AudioSource audioSource; // Fuente de audio principal para controlar el volumen

    [Header("Configuraci�n de Pantalla")]
    public Toggle fullscreenToggle; // Toggle para cambiar entre pantalla completa y ventana

    [Header("Configuraci�n de Resoluci�n")]
    public TextMeshProUGUI resolutionText; // Texto que muestra la resoluci�n actual
    private int currentResolutionIndex;
    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution { width = 1280, height = 720 },  // Resoluci�n baja
        new Resolution { width = 1920, height = 1080 }, // Resoluci�n media
        new Resolution { width = 2560, height = 1440 }  // Resoluci�n alta
    };

    void Start()
    {
        // Cargar configuraci�n guardada
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        //audioSource.volume = volumeSlider.value;

        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = fullscreenToggle.isOn;

        // Configuraci�n de resoluci�n
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
        ApplyResolution();

        // Agregar listeners
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    // Funci�n para actualizar el volumen
    void OnVolumeChanged(float value)
    {
        //audioSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    // Funci�n para cambiar entre pantalla completa y ventana
    void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    // Funci�n para cambiar la resoluci�n entre las opciones disponibles
    public void ChangeResolution()
    {
        currentResolutionIndex = (currentResolutionIndex + 1) % resolutions.Length;
        ApplyResolution();
    }

    // Aplicar la resoluci�n seleccionada
    private void ApplyResolution()
    {
        Resolution selectedResolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        if (resolutionText != null)
        {
            resolutionText.text = selectedResolution.width + "x" + selectedResolution.height;
        }

        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
    }
}
