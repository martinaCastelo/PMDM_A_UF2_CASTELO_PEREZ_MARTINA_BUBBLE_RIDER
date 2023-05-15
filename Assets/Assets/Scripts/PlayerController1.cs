using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    //SCRIPT HOP SIN BUBBLEMASK
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    float moveX;
    bool jump;
    bool active = true;
    GameManager game;

    public bool canMove = true;
    AudioSource sfx;
    [SerializeField] AudioClip sfxJump;

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

    }



}
