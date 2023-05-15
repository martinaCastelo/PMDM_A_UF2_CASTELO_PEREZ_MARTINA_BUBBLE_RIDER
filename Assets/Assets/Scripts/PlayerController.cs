using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //SCRIPT HOP CON BUBBLEMASK
    const float SHOOT_OFFSET = 0.3f;
    float shootOffsetX = 0.5f;
    [SerializeField] GameObject shoot;
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    float moveX;
    bool jump;
    bool active = true;
    GameManager game;

    AudioSource sfx;
    [SerializeField] AudioClip sfxJump;
    [SerializeField] AudioClip sfxHurt;

    public bool canMove = true;

    void Start()
    {
        Cursor.visible = false;
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        game = GameManager.GetInstance();
    }

    private void FixedUpdate()
    {
        if (!DialogueManager.isDialogueActive)
        {
            Run();
            Flip();
            Jump();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        if (!jump && Input.GetButtonDown("Jump"))
            jump = true;

        if (active && !game.isGamePaused() && Input.GetKeyDown(KeyCode.X))
        {
            GameObject projectile = Instantiate(shoot,
                new Vector3(transform.position.x + SHOOT_OFFSET, transform.position.y + SHOOT_OFFSET, 0), Quaternion.identity);

            ShootController projectileScript = projectile.GetComponent<ShootController>();
            if (transform.localScale.x > 0)
            {
                projectileScript.speed *= -1;
                shootOffsetX *= -1;
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ShootEnemy")
        {
            anim.SetTrigger("isHurting");
            sfx.clip = sfxHurt;
            sfx.Play();
            game.LoseLife();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            anim.SetTrigger("isHurting");
            sfx.clip = sfxHurt;
            sfx.Play();
            game.LoseLife();
        }
    }
    void Run()
    {
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = vel;

        anim.SetBool("isRunning", Mathf.Abs(rb.velocity.x) > Mathf.Epsilon);
    }

    void Flip()
    {
        float vx = rb.velocity.x;
        if (Mathf.Abs(vx) > Mathf.Epsilon)
            transform.localScale = new Vector2(Mathf.Sign(vx), 1);
    }

    void Jump()
    {
        if (!jump)
            return;

        jump = false;

        if (!col.IsTouchingLayers(LayerMask.GetMask("Terrain")))
            return;

        rb.velocity += new Vector2(0, jumpSpeed);
        sfx.clip = sfxJump;
        sfx.Play();
        anim.SetTrigger("isJumping");
    }



}
