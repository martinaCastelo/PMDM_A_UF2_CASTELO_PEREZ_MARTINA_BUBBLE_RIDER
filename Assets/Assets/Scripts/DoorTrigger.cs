using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2; // Referencia a las puertas
    private bool hasTriggered = false;
    //[SerializeField] GameObject enemies;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player2"))
        {
            //enemies.SetActive(true);
            door1.SetActive(true);
            door2.SetActive(true);
            hasTriggered = true;
            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

}
