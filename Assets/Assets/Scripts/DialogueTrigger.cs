using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //public DialogueManager dialogueManager;
    public GameObject dialogueBox;
    public GameObject mask;
    public GameObject barrier;
    private bool hasTriggered = false;
    public bool isMaskCollider = false;
    public bool isAtticus;
    public GameObject player2;
    public CinemachineVirtualCamera followCamera;
    GameManager game;

    private void Start()
    {
        game = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            dialogueBox.SetActive(true);
            TriggerDialogue();
            hasTriggered = true;
            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }
            //GetComponent<Collider2D>().enabled = false;

            if (isMaskCollider)
            {
                GameObject maskObject = GameObject.FindGameObjectWithTag("Mask");
                maskObject.SetActive(false);
                GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
                oldPlayer.SetActive(false);
                //GameObject newPlayer = GameObject.FindGameObjectWithTag("Player2");
                player2.SetActive(true);
                //PlayerController playerController = player2.GetComponent<PlayerController>();
                //playerController.masked = true;
                barrier.SetActive(false);
                player2.tag = "Player";
                //Debug.Log("El tag actual del objeto es: " + player2.tag);
                followCamera.Follow = player2.transform;
            }

            if (isAtticus)
            {
                mask.SetActive(true);
            }

        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.isDialogueActive = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
