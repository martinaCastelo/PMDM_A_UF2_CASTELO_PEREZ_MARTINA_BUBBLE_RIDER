using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BubblelerTrigger : MonoBehaviour
{
    [SerializeField] GameObject bubbleler;
    public CinemachineVirtualCamera followCamera;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player2"))
        {
            hasTriggered = true;
            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player2");
            oldPlayer.SetActive(false);
            bubbleler.SetActive(true);
            //Debug.Log("Se ha activado el bubbleler");
            bubbleler.tag = "Player2";
            //Debug.Log("El tag actual del objeto es: " + player2.tag);
            followCamera.Follow = bubbleler.transform;



        }
    }

}
