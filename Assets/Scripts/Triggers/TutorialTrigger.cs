using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Texto del Tutorial")]
    public string tutorialText;  // Texto del tutorial a mostrar
    public float displayTime = 3f;  // Tiempo que el texto estará visible

    [Header("UI de Tutorial")]
    public GameObject tutorialPanel;  // Panel donde se muestra el texto
    public TextMeshProUGUI tutorialTextComponent;  // Componente de texto para mostrar el tutorial

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el jugador entra en el trigger
        if (other.CompareTag("Player"))
        {
            // Mostrar el panel y el texto del tutorial
            ShowTutorial();
        }
    }

    private void ShowTutorial()
    {
        tutorialPanel.SetActive(true);  // Activar el panel del tutorial
        tutorialTextComponent.text = tutorialText;  // Asignar el texto al componente TextMeshPro

        // Invocar el método para ocultar el tutorial después de X segundos
        Invoke(nameof(HideTutorial), displayTime);
    }

    private void HideTutorial()
    {
        tutorialPanel.SetActive(false);  // Desactivar el panel del tutorial
    }
}
