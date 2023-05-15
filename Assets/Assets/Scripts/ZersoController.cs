using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZersoController : MonoBehaviour
{
    const int HITS_TO_DESTROY = 50;
    int hitCount;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject lifeDropPrefab;
    [SerializeField] bool dropLife = true;
    private int lifeDropsAmount = 4;
    private Vector3 initialPosition;
    private bool movingRight = false;
    [SerializeField] GameObject shoot;
    [SerializeField] float shootDelay;
    [SerializeField] float shootProb;

    GameManager game;
    Animator anim;

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
        anim = GetComponent<Animator>();
        game = GameManager.GetInstance();
        initialPosition = transform.position;
        StartCoroutine("Shoot");
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
            for (int i = 0; i < lifeDropsAmount; i++)
            {
                Instantiate(lifeDropPrefab, transform.position, Quaternion.identity);
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootDelay);

            if (Random.Range(0f, 1f) < shootProb)
            {
                GameObject projectile = Instantiate(shoot, transform.position, Quaternion.identity);
                HorizontalShootEnemy projectileScript = projectile.GetComponent<HorizontalShootEnemy>();
                if (transform.localScale.x > 0)
                    projectileScript.speed *= -1;
            }
        }
    }
}
