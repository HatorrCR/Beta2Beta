using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTriggerS : MonoBehaviour
{
    [Header("Di�logo")]
    public string[] dialogueLines;  // L�neas de di�logo a mostrar
    public float timeBetweenLines = 2f;  // Tiempo entre cada l�nea

    [Header("UI de Di�logo")]
    public GameObject dialoguePanel;  // Panel donde se muestra el di�logo
    public TextMeshProUGUI dialogueText;  // Texto del di�logo

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
        dialoguePanel.SetActive(true);  // Activar el panel de di�logo

        // Pausar el juego mientras se muestran los di�logos
        Time.timeScale = 0f;

        foreach (string line in dialogueLines)
        {
            dialogueText.text = line;  // Asignar cada l�nea de di�logo al texto
            yield return new WaitForSecondsRealtime(timeBetweenLines);  // Usar WaitForSecondsRealtime para que no se vea afectado por el Time.timeScale
        }

        // Reanudar el juego despu�s de que se termine el di�logo
        Time.timeScale = 1f;

        dialoguePanel.SetActive(false);  // Desactivar el panel de di�logo
        isDialogueActive = false;
        gameObject.SetActive(false);  // Desactivar el trigger
    }
}
