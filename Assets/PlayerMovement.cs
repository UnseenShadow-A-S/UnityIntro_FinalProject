using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;

    private Collider2D col;

    private float move;

    public float speed;
    public float jump;
    public float wallPush;
    public int jumpDirection;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer; 
    
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private Transform groundCheckPosition;

    [SerializeField] private float wallDistanceCheck = 0.3f;
    [SerializeField] private Transform wallCheckPosition; 
    
    private Vector2 groundRayOrigin;
    private Vector2 wallRayOriginRight;
    private Vector2 wallRayOriginLeft;
    
    private bool isGrounded;
    private bool isWallTouching;
    private bool isWallTouchingRight;
    private bool isWallTouchingLeft;
    
    bool wallSliding; 
    float jumpForce;

    private Vector3 playerPosition = new Vector3();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpForce = 10;
        wallPush = 500;
        playerPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.Log("Player collidor2D is null");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
        move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
        
        Bounds bounds = col.bounds;
        
        //Ground Ray (starting from the bottom/Min(Y-Axis), center(X-Axis))
        groundRayOrigin = new Vector2(bounds.center.x, bounds.min.y);
        Debug.DrawRay(groundRayOrigin, Vector2.down * groundDistance, Color.yellow);
        
        //Wall Ray - Right side (starting from the center(Y-Axis), Right/Max(X-Axis)
        wallRayOriginRight = new Vector2(bounds.max.x, bounds.center.y);
        Debug.DrawRay(wallRayOriginRight, Vector2.right * wallDistanceCheck, Color.red);
        
        //Wall Ray - Left side (starting from the center(Y-Axis), Left/Min(X-Axis)
        wallRayOriginLeft = new Vector2(bounds.min.x, bounds.center.y);
        Debug.DrawRay(wallRayOriginLeft, Vector2.left * wallDistanceCheck, Color.blue);

        // isGrounded = Physics2D.Raycast(
        //     groundCheckPosition.position, // Raycast origin
        //     Vector2.down, // Raycast direction
        //     groundDistance, // Raycast distance
        //     groundLayer); // LayerMask (to check for specific layer
        // Debug.DrawRay(groundCheckPosition.position,
        //     Vector2.down * groundDistance,
        //     isGrounded ? Color.green : Color.red
        //     );
        //
        // isWallTouchingRight = Physics2D.Raycast(
        //     transform.position,
        //     Vector2.right,
        //     wallDistanceCheck,
        //     wallLayer); 
        //
        // // Debug.DrawRay(wallCheckPosition.position,
        // //     Vector2.right * wallDistanceCheck,
        // //     isWallTouchingRight ? Color.green : Color.red
        // //     );
        //
        // isWallTouchingLeft = Physics2D.Raycast(
        //     transform.position,
        //     Vector2.left,
        //     wallDistanceCheck,
        //     wallLayer);
        // // Debug.DrawRay(wallCheckPosition.position,
        // //     Vector2.left * wallDistanceCheck,
        // //     isWallTouchingLeft ? Color.green : Color.red
        // //     );

        isWallTouching = isWallTouchingLeft || isWallTouchingRight;
        wallSliding = isWallTouching && !isGrounded;
            
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GroundJump();
        }
        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            WallJump();
        }
    }

    void GroundJump()
    {
        rb.AddForce(new Vector2(rb.linearVelocity.x, jump * jumpForce));
    }

    void WallJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(new Vector2(wallPush * jumpDirection * 10, rb.linearVelocity.y));
        rb.AddForce(new Vector2(rb.linearVelocity.x, jump * jumpForce));

    }
    
//     void OnCollisionEnter2D(Collision2D other)
//     {
//         if (other.gameObject.tag == "Ground")
//         {
//             grounded = true;
//             Debug.Log("Player is Grounded");
//         }
//         else if (other.gameObject.tag == "Wall")
//         {
//             wallSliding = true;
//             Debug.Log("Player is sliding against the wall");
//         }
//     }
//
//     void OnCollisionStay2D(Collision2D other)
//     {
//         if (other.gameObject.tag == "Wall")
//         {
//             Vector2 wallDirection = other.GetContact(0).normal;
//
//             if (wallDirection.x > 0) // wall on the left
//             {
//                 jumpDirection = 1;
//             }
//             else if (wallDirection.x < 0)
//             {
//                 jumpDirection = -1;
//             }
//         }
//     }
//
//     void OnCollisionExit2D(Collision2D other)
//     {
//         if (other.gameObject.tag == "Ground")
//         {
//             grounded = false;
//             Debug.Log("Player has jumped");
//         }
//         else if (other.gameObject.tag == "Wall")
//         {
//             wallSliding = false;
//             Debug.Log("Player has left the wall");
//         }
//     }
}
