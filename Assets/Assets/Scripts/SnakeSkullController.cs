using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSkullController : MonoBehaviour
{
    const int HITS_TO_DESTROY = 6;
    int hitCount;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distance = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject lifeDropPrefab;
    [SerializeField] bool dropLife = true;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    GameManager game;
    Animator anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("isHurting");
        ++hitCount;
        if (hitCount == HITS_TO_DESTROY)
            DestroyEnemy();
    }
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Enemy: Colision");
    }*/

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player2")
            game.LoseLife();

    }
    void Start()
    {
        game = GameManager.GetInstance();
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(distance, 0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position.x >= targetPosition.x)
            {
                movingRight = false;
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            if (transform.position.x <= initialPosition.x)
            {
                movingRight = true;
                spriteRenderer.flipX = false;
            }
        }
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
