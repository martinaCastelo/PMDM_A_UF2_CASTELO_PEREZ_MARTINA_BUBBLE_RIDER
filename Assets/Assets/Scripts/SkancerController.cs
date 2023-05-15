using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkancerController : MonoBehaviour
{
    const int HITS_TO_DESTROY = 5;
    int hitCount;
    [SerializeField] GameObject shoot;
    [SerializeField] GameObject explosion;
    [SerializeField] float shootDelay;
    [SerializeField] float shootProb;
    Animator anim;
    GameManager game;

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("isHurting");
        ++hitCount;
        if (hitCount == HITS_TO_DESTROY)
        {
            DestroyEnemy();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player2")
            game.LoseLife();
    }

    void Start()
    {
        game = GameManager.GetInstance();
        anim = GetComponent<Animator>();
        StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyEnemy()
    {
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
                GameObject player = GameObject.FindWithTag("Player2");

                if (player != null &&
                    (transform.position.x > player.transform.position.x - 0.75f)
                    &&
                    (transform.position.x < player.transform.position.x + 0.75f))
                {
                    Instantiate(shoot, transform.position, Quaternion.identity);
                }

            }
        }
    }
}
