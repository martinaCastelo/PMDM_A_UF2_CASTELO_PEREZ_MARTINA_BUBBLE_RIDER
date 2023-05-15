using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController : MonoBehaviour
{
    const int HITS_TO_DESTROY = 5;
    public float amplitude = 1f; // amplitud de la oscilación
    public float frequency = 1f; // frecuencia de la oscilación
    private float timeOffset = 0f; // desplazamiento de tiempo
    private Vector3 startPos; // posición inicial de la medusa
    int hitCount;
    Animator anim;
    GameManager game;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject lifeDropPrefab;
    [SerializeField] bool dropLife = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("isHurting");
        ++hitCount;
        if (hitCount == HITS_TO_DESTROY)
            DestroyEnemy();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player2")
            game.LoseLife();
    }

    void Start()
    {
        startPos = transform.position;
        game = GameManager.GetInstance();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timeOffset += Time.deltaTime;
        Vector3 pos = startPos;
        pos.y += amplitude * Mathf.Sin(2 * Mathf.PI * frequency * timeOffset);
        transform.position = pos;
    }

    void DestroyEnemy()
    {
        if (dropLife)
        {
            Instantiate(lifeDropPrefab, transform.position, Quaternion.identity);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
