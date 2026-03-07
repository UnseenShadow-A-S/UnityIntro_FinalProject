using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 1f;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position +  offset;
        
        transform.position = Vector3.Lerp(
            transform.position,
            playerPosition,
            moveSpeed * Time.deltaTime);
    }
}
