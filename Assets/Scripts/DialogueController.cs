using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Events;


[System.Serializable]
public class DialogueLine
{
    public Sprite speakerImage;
    [TextArea(2, 5)]
    public string text;
    public float displayDuration = 2f;
    public UnityEvent onLineStart;
}


public class DialogueController : MonoBehaviour
{
    [Header("UI References")]
    public Image speakerImageUI;
    public TextMeshProUGUI dialogueTextUI;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.04f;
    public AudioClip typewriterClick;

    [Header("Dialogue Sequence")]
    public DialogueLine[] dialogueLines;

    private Coroutine dialogueCoroutine;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        dialogueCoroutine = StartCoroutine(PlayDialogueSequence());
    }

    private IEnumerator PlayDialogueSequence()
    {
        foreach (DialogueLine line in dialogueLines)
        {
            line.onLineStart?.Invoke();
            speakerImageUI.sprite = line.speakerImage;
            yield return StartCoroutine(TypeText(line.text));
            yield return new WaitForSeconds(line.displayDuration);
        }

        dialogueTextUI.text = "";
        gameObject.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {
        dialogueTextUI.text = text;
        dialogueTextUI.ForceMeshUpdate();
        int totalVisibleChars = dialogueTextUI.textInfo.characterCount;

        for (int i = 0; i <= totalVisibleChars; i++)
        {
            dialogueTextUI.maxVisibleCharacters = i;
            audioSource.PlayOneShot(typewriterClick);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
