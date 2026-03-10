using Unity.VisualScripting;
using UnityEngine;

public class TutorialKeyAnimation : MonoBehaviour
{

    public float animationMoveSpeed = 0.15f;
    public float animationMoveDistance = 0.2f;

    private Vector3 startingPosition;
    private bool movingUp = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            transform.localPosition += Vector3.up * animationMoveSpeed * Time.deltaTime;

            if (transform.localPosition.y >= startingPosition.y + animationMoveDistance)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.localPosition -= Vector3.up * animationMoveSpeed * Time.deltaTime;

            if (transform.localPosition.y <= startingPosition.y)
            {
                movingUp = true;
            }
        }
    }
}
