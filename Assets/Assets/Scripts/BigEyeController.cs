using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyeController : MonoBehaviour
{
    AudioSource sfx;
    [SerializeField] AudioClip sfxDestroy;
    const int HITS_TO_DESTROY = 4;
    int hitCount;
    [SerializeField] GameObject shoot;
    [SerializeField] GameObject explosion;
    [SerializeField] float shootDelay;
    [SerializeField] float shootProb;
    public float speed = 5f;
    public float zigzagWidth = 5f;
    public float zigzagHeight = 5f;

    private Vector3 startPosition;
    private float time;
    GameManager game;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ++hitCount;
        if (hitCount == HITS_TO_DESTROY)
            DestroyEnemy();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player2")
            game.LoseLife();
    }

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        game = GameManager.GetInstance();
        startPosition = transform.position;
        StartCoroutine("Shoot");
    }

    private void Update()
    {
        time += Time.deltaTime;

        float x = Mathf.Sin(time * speed) * zigzagWidth;
        float y = Mathf.Cos(time * speed) * zigzagHeight;
        Vector3 newPosition = startPosition + new Vector3(x, y, 0f);

        transform.position = newPosition;
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


