using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTriggerS : MonoBehaviour
{
    [Header("Diálogo")]
    public string[] dialogueLines;  // Líneas de diálogo a mostrar
    public float timeBetweenLines = 2f;  // Tiempo entre cada línea

    [Header("UI de Diálogo")]
    public GameObject dialoguePanel;  // Panel donde se muestra el diálogo
    public TextMeshProUGUI dialogueText;  // Texto del diálogo

    private int currentLine = 0;
    private bool isDialogueActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDialogueActive)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    private IEnumerator ShowDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);  // Activar el panel de diálogo

        // Pausar el juego mientras se muestran los diálogos
        Time.timeScale = 0f;

        foreach (string line in dialogueLines)
        {
            dialogueText.text = line;  // Asignar cada línea de diálogo al texto
            yield return new WaitForSecondsRealtime(timeBetweenLines);  // Usar WaitForSecondsRealtime para que no se vea afectado por el Time.timeScale
        }

        // Reanudar el juego después de que se termine el diálogo
        Time.timeScale = 1f;

        dialoguePanel.SetActive(false);  // Desactivar el panel de diálogo
        isDialogueActive = false;
        gameObject.SetActive(false);  // Desactivar el trigger
    }
}
