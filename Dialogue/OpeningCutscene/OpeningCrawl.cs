using System.Collections;
using UnityEngine;

public class OpeningCrawl : MonoBehaviour
{

    public DialogueTrigger dialogueTrigger;

    public DialogueManager dialogueManager = DialogueManager.dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();


        StopAllCoroutines();
        StartCoroutine(openingTextCrawl());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator openingTextCrawl()
    {
        dialogueTrigger.TriggerDialogue();

        yield return new WaitForSeconds(dialogueManager.getTimeNeeded() + 1f);

        dialogueManager.DisplayNextSentence();

        yield return new WaitForSeconds(dialogueManager.getTimeNeeded() + 1f);

        dialogueManager.DisplayNextSentence();

        dialogueManager.EndDialogue();

    }
}
