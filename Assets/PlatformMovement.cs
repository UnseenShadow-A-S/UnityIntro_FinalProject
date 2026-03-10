using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveDistance = 5f;
    
    private Vector3 startPos;
    private bool isGoingRight = true;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x >= startPos.x + moveDistance)
            {
                isGoingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;

            if (transform.position.x <= startPos.x - moveDistance)
            {
                isGoingRight = true;
            }
        }
    }
}
