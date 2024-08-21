using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpDuration;

    private Vector2 moveInput;
    private Vector2 position;
    private float jumpTime;
    private bool isJumping;

    void Update() 
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        position.x = moveInput.x * moveSpeed;
        position.y = rigidBody.velocity.y;
        rigidBody.velocity = position;

        
        if (Input.GetKeyDown(KeyCode.X) && !isJumping) 
        {
            isJumping = true;
        }

        if (isJumping) 
        {
            jumpTime += Time.deltaTime;
            position.y = jumpTime < jumpDuration * 0.5f ? jumpSpeed : -jumpSpeed;
            rigidBody.velocity = position;
            
            if (jumpTime >= jumpDuration)
            {
                isJumping = false;
                position.y = 0;
                jumpTime = 0f;
                rigidBody.velocity = position;
            }
        }
    }
}
