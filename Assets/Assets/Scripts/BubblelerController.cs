using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblelerController : MonoBehaviour
{
    [SerializeField] float force;
    Rigidbody2D rb;
    [SerializeField] GameObject shoot;
    bool active = true;
    float shootOffsetX = 0.5f;
    GameManager game;
    Transform shootOriginTop;
    Transform shootOriginBottom;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        shootOriginTop = transform; // Asigna el transform del jugador a la variable
        shootOriginTop.position += new Vector3(0, 0.5f, 0); // Agrega un offset de 0.5 en la coordenada Y
        shootOriginBottom = transform; // Asigna el transform del jugador a la variable
        shootOriginBottom.position -= new Vector3(0, 0.5f, 0); // Resta un offset de 0.5 en la coordenada Y


        rb = GetComponent<Rigidbody2D>();
        game = GameManager.GetInstance();
    }


    void FixedUpdate()
    {
        CheckMove();
    }

    void CheckMove()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction.Normalize();

        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (active && !game.isGamePaused())
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                GameObject projectile = Instantiate(shoot, transform.position + Vector3.left * shootOffsetX, Quaternion.identity);
                ShootController projectileScript = projectile.GetComponent<ShootController>();
                projectileScript.speed = -Mathf.Abs(projectileScript.speed) * Mathf.Sign(transform.localScale.x);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                GameObject projectile = Instantiate(shoot, transform.position + Vector3.right * shootOffsetX, Quaternion.identity);
                ShootController projectileScript = projectile.GetComponent<ShootController>();
                projectileScript.speed = Mathf.Abs(projectileScript.speed) * Mathf.Sign(transform.localScale.x);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject projectile = Instantiate(shoot, transform.position + Vector3.up * shootOffsetX, Quaternion.identity);
                ShootController projectileScript = projectile.GetComponent<ShootController>();
                projectileScript.speed = Mathf.Abs(projectileScript.speed) * Mathf.Sign(transform.localScale.y);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GameObject projectile = Instantiate(shoot, transform.position + Vector3.down * shootOffsetX, Quaternion.identity);
                ShootController projectileScript = projectile.GetComponent<ShootController>();
                projectileScript.speed = -Mathf.Abs(projectileScript.speed) * Mathf.Sign(transform.localScale.y);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ShootEnemy")
        {
            game.LoseLife();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            game.LoseLife();
        }
    }

}
