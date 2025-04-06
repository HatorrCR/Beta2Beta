using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Di�logo")]
    public string[] dialogueLines;  // L�neas de di�logo a mostrar
    public float timeBetweenLines = 2f;  // Tiempo entre cada l�nea

    [Header("UI de Di�logo")]
    public GameObject dialoguePanel;  // Panel donde se muestra el di�logo
    public TextMeshProUGUI dialogueText;  // Texto del di�logo

    [Header("Tiempo")]
    [Range(0.1f, 1f)] public float slowdownFactor = 0.3f;  // Factor de ralentizaci�n ajustable

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
        dialoguePanel.SetActive(true);

        // Ralentizar el tiempo
        Time.timeScale = slowdownFactor;

        foreach (string line in dialogueLines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(timeBetweenLines);  // Se ve afectado por Time.timeScale
        }

        // Restaurar el tiempo
        Time.timeScale = 1f;

        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        gameObject.SetActive(false);
    }
}
