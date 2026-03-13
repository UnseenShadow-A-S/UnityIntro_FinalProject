using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public float jumpInterval = 3f;
    public float reactionForce = 3f;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float jumpTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpInterval;
    }

    void Update()
    {
        Move();
        JumpLogic();
    }

    void Move()
    {
        float direction = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void JumpLogic()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer = jumpInterval;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            movingRight = !movingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;

                playerRb.AddForce(direction * reactionForce, ForceMode2D.Impulse);
                rb.AddForce(-direction * reactionForce, ForceMode2D.Impulse);
            }
        }
    }
}