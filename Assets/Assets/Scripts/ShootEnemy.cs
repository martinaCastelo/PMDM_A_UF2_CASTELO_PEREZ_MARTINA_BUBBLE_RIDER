using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float temp;

    void Update()
    {
        //actualizar temporizador
        temp -= Time.deltaTime;
        if (temp < 0)
            Destroy(gameObject);

        //actualizar posicion
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //destruimos el disparo
        Destroy(gameObject);
    }
}
