using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueIntro : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup introCanvasGroup;
    public TextMeshProUGUI dialogueText;
    public float fadeDuration = 1f;
    public float dialogueDuration = 3f;

    [Header("Dialogues")]
    [TextArea(2, 5)]
    public string[] dialogues;

    private int currentDialogueIndex = 0;
    private bool isSkipping = false;

    void Start()
    {
        // Pausar el juego al iniciar
        Time.timeScale = 0f;

        // Iniciar con la pantalla negra
        introCanvasGroup.alpha = 1;
        StartCoroutine(ShowDialogues());
    }

    void Update()
    {
        // Saltar diálogos si se presiona Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isSkipping = true;
        }
    }

    IEnumerator ShowDialogues()
    {
        while (currentDialogueIndex < dialogues.Length)
        {
            // Mostrar el diálogo actual
            dialogueText.text = dialogues[currentDialogueIndex];

            // Esperar el tiempo del diálogo o salir si se presiona Esc
            float timer = 0;
            while (timer < dialogueDuration && !isSkipping)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            // Si se presionó Esc, salir del bucle
            if (isSkipping)
                break;

            currentDialogueIndex++;
        }

        // Fade out antes de empezar el juego
        yield return StartCoroutine(FadeOut());

        // Desactivar el Canvas y reanudar el juego
        introCanvasGroup.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    IEnumerator FadeOut()
    {
        float time = 0;
        float startAlpha = introCanvasGroup.alpha;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            introCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            yield return null;
        }

        introCanvasGroup.alpha = 0;
    }
}
