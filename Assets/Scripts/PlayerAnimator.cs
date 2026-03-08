using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleMotion(Vector2 moveVector)
    {
        animator.SetBool("isMoving", moveVector.magnitude > 0);
        if (moveVector.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveVector.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void handleJump()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isGrounded", false);
    }

    public void handleFall()
    {
        animator.SetBool("isJumping", false);
    }

    public void handleLanding()
    {
        animator.SetBool("isGrounded", true);
    }
}
