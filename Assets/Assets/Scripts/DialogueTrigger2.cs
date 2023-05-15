using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{
    public Dialogue dialogue;

    //public DialogueManager dialogueManager;
    public GameObject dialogueBox;
    private bool hasTriggered = false;
    public GameObject player2;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player2"))
        {
            //Debug.Log("Player entered collider");
            dialogueBox.SetActive(true);
            TriggerDialogue();
            hasTriggered = true;
            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }
            //GetComponent<Collider2D>().enabled = false;

        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.isDialogueActive = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }

}
