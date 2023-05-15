using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalShootEnemy : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] float temp;
    [SerializeField] float direction;

    void Update()
    {
        //actualizar temporizador
        temp -= Time.deltaTime;
        if (temp < 0)
            Destroy(gameObject);

        //actualizar posicion
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //destruimos el disparo
        Destroy(gameObject);
    }
}