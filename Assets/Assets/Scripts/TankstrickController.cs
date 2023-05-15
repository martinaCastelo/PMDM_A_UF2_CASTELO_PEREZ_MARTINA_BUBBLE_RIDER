using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankstrickController : MonoBehaviour
{
    const int HITS_TO_DESTROY = 25;
    int hitCount;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] GameObject explosion;
    GameManager game;
    Animator anim;
    [SerializeField] GameObject lifeDropPrefab;
    [SerializeField] bool dropLife = true;

    private Vector3 initialPosition;
    private bool movingRight = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("isHurting");
        ++hitCount;
        if (hitCount == HITS_TO_DESTROY)
            DestroyEnemy();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player2")
            game.LoseLife();

    }

    private void Start()
    {
        game = GameManager.GetInstance();
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Mathf.Abs(transform.position.x - initialPosition.x) >= maxDistance)
        {
            movingRight = !movingRight;
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
