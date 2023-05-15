using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerKEA : MonoBehaviour
{
    private bool hasTriggered = false;
    [SerializeField] GameObject enemies;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player2"))
        {
            enemies.SetActive(true);
            hasTriggered = true;
            if (GetComponent<Collider2D>() != null)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
