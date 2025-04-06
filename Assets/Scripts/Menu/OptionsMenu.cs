using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para usar TextMeshPro

public class OptionsMenu : MonoBehaviour
{
    [Header("Configuración de Sonido")]
    public Slider volumeSlider; // Control deslizante para el volumen
    public AudioSource audioSource; // Fuente de audio principal para controlar el volumen

    [Header("Configuración de Pantalla")]
    public Toggle fullscreenToggle; // Toggle para cambiar entre pantalla completa y ventana

    [Header("Configuración de Resolución")]
    public TextMeshProUGUI resolutionText; // Texto que muestra la resolución actual
    private int currentResolutionIndex;
    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution { width = 1280, height = 720 },  // Resolución baja
        new Resolution { width = 1920, height = 1080 }, // Resolución media
        new Resolution { width = 2560, height = 1440 }  // Resolución alta
    };

    void Start()
    {
        // Cargar configuración guardada
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        //audioSource.volume = volumeSlider.value;

        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = fullscreenToggle.isOn;

        // Configuración de resolución
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
        ApplyResolution();

        // Agregar listeners
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    // Función para actualizar el volumen
    void OnVolumeChanged(float value)
    {
        //audioSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    // Función para cambiar entre pantalla completa y ventana
    void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    // Función para cambiar la resolución entre las opciones disponibles
    public void ChangeResolution()
    {
        currentResolutionIndex = (currentResolutionIndex + 1) % resolutions.Length;
        ApplyResolution();
    }

    // Aplicar la resolución seleccionada
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
