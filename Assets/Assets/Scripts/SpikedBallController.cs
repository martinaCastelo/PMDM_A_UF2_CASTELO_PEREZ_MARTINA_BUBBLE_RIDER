using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distance = 2f;
    private Vector3 initialPosition;
    private Vector3 targetPositionHorizontal;
    private Vector3 targetPositionVertical;
    [SerializeField] bool movingRight;
    private SpriteRenderer spriteRenderer;
    GameManager game;
    [SerializeField] bool vertical = false;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player2")
            game.LoseLife();
    }

    void Start()
    {
        game = GameManager.GetInstance();
        initialPosition = transform.position;
        targetPositionHorizontal = initialPosition + new Vector3(distance, 0, 0);
        targetPositionVertical = initialPosition + new Vector3(0, distance, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!vertical)
        {
            if (movingRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionHorizontal, speed * Time.deltaTime);
                if (transform.position.x >= targetPositionHorizontal.x)
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
        else
        {
            if (movingRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionVertical, speed * Time.deltaTime);
                if (transform.position.y >= targetPositionVertical.y)
                {
                    movingRight = false;
                    spriteRenderer.flipY = true;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
                if (transform.position.y <= initialPosition.y)
                {
                    movingRight = true;
                    spriteRenderer.flipY = false;
                }
            }
        }
    }
}
