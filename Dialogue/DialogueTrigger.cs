using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    // Attach this to the thing you want to trigger the dialogue

    public Dialogue dialogue;


    // We need a way to feed this into our dialogue manager
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
