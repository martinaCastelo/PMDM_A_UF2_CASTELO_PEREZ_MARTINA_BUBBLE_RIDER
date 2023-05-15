using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEmAll : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2; // Referencia a las puertas
    private int initialEnemyCount; // Número inicial de enemigos en la sala

    private void Start()
    {
        initialEnemyCount = transform.childCount;
    }

    private void Update()
    {
        if (initialEnemyCount > 0 && transform.childCount == 0)
        {
            // Abre las puertas
            door1.SetActive(false);
            door2.SetActive(false);

            initialEnemyCount = 0;
        }
    }
}
