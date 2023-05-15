using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] float temp;

    [SerializeField] float direction;
    [SerializeField] GameObject hitExplosion;

    // Update is called once per frame
    void Update()
    {
        //actualizar mi temporizador
        temp -= Time.deltaTime;
        if (temp < 0)
            Destroy(gameObject);
        //actualizar la posicion
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //animacion explosion
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        //destruimos el disparo
        Destroy(gameObject);
    }
}
