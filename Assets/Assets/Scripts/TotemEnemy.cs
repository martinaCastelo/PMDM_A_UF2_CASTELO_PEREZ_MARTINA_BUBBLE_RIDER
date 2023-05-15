using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemEnemy : MonoBehaviour
{
    const int HITS_TO_DESTROY = 10;
    int hitCount;
    [SerializeField] GameObject shoot;
    [SerializeField] float shootDelay;
    [SerializeField] float shootProb;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject lifeDropPrefab;
    [SerializeField] bool dropLife = true;
    Animator anim;
    GameManager game;
    void Start()
    {
        anim = GetComponent<Animator>();
        game = GameManager.GetInstance();
        StartCoroutine("Shoot");
    }

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
    void Update()
    {

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
