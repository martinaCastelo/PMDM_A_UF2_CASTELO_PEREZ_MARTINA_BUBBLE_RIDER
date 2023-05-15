using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalActivate : MonoBehaviour
{
    [SerializeField] GameObject deadPedestal;
    [SerializeField] GameObject alivePedestal;
    [SerializeField] GameObject bubblelerTrigger;
    [SerializeField] GameObject enemies;
    [SerializeField] GameObject compuerta;


    private void OnTriggerEnter2D(Collider2D other)
    {
        deadPedestal.SetActive(false);
        alivePedestal.SetActive(true);
        bubblelerTrigger.SetActive(true);
        enemies.SetActive(true);
        compuerta.SetActive(false);
    }
}
