using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    // UI to capture the name of the person who is talking
    public TextMeshProUGUI nameText;
    // UI to capture the dialogue message
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public float timeNeeded;
    public float textSpeed = 0.5f;




    public static DialogueManager dialogueManager { get; private set; }

    public static bool dialogueRunning;



    // Queue to hold the sentences. FIFO so we can enqueue and dequeue when we want to add and display sentences
    private Queue<string> sentences;

    private void Awake()
    {

        // Singleton
        if (dialogueManager != null)
        {
            Debug.LogError("There shant be more than 1 dialogue manager");
        }
        dialogueManager = this;



        sentences = new Queue<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //sentences = new Queue<string>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This will the start the entire interaction
    public void StartDialogue(Dialogue dialogue)
    {

        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }
        

        if (nameText != null)
        {
            nameText.text = dialogue.name;
        }


        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();


    }




    // Display the next sentence
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        // This is if we want it all to update at once, however if we want 1 letter at a time, we can use a Coroutine
        //dialogueText.text = sentence;



        // The player is likely to stop the sentence before it's completely finished. If that happens, we want to stop the current animation before starting the new one
        // If TypeSentence is already running, it will stop before starting the new one
        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));

    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        timeNeeded = sentence.Length * textSpeed;

        foreach (char character in sentence.ToCharArray())
        {
            dialogueText.text += character;
            //yield return null; // This only waits a single frame
            yield return new WaitForSeconds(textSpeed * Time.deltaTime);

            //yield return new WaitForSeconds(textSpeed);

        }
    }

    public float getTimeNeeded()
    {
        return timeNeeded;
    }

    public void EndDialogue()
    {
        if (animator != null)
        {
            animator.SetBool("IsOpen", false);
        }
        
    }
}

