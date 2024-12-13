using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("BasicMovement")]
    Rigidbody rb;
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    float acceleration = 1.0f;
    [SerializeField]
    float maxSpeed = 10f;
    float horizontalMovement;
    bool isLookingRight = true;
    float movementSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private bool isGrounded = false;
    [SerializeField] private Transform groundChecker; 
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float doubleJumpForce;
    private bool doubleJump = false;
    [SerializeField] bool canJump;

    [Header("Dash")]
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashForce = 20f;
    [SerializeField]
    private float timeCanDash = 1f;
    private bool isDashing;
    private bool canDash = true;
    [SerializeField]
    TrailRenderer dashTrailRenderer;

    [Header("Animator")]
    [SerializeField]
    Animator playerAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        playerAnimator.SetFloat("Velocity", Mathf.Abs(horizontalMovement));
        movementSpeed = horizontalMovement * currentSpeed;
        //Acceleration
        currentSpeed += acceleration * Time.deltaTime;
        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        if (isDashing)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);

        rb.linearVelocity = new Vector3(movementSpeed, rb.linearVelocity.y);
        if(isGrounded == true)
        {
            canJump = true;
            doubleJump = true;
            playerAnimator.SetBool("InTheAir", false);
        }
        if(rb.linearVelocity.y <0 && isGrounded == false)
        {
            playerAnimator.SetBool("InTheAir", true);
            playerAnimator.SetBool("Jump", false);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            horizontalMovement = context.ReadValue<Vector2>().x;

        }
        if (context.canceled)
        {
            horizontalMovement = 0;
        }
        if (horizontalMovement < 0 && isLookingRight)
        {
            Flip();
        }
        if (horizontalMovement > 0 && !isLookingRight)
        {
            Flip();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump == true)
        {
            if (context.performed && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canJump = false;
                doubleJump = true;
                playerAnimator.SetBool("Jump", true);
            }
            if(context.performed && doubleJump == true)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
                doubleJump = false;
                dashTrailRenderer.emitting = true;
                Invoke("DesactivateDashTrailRenderer", 0.8f);
            }
        }
    }
    public void ActivateDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Dash());
            dashTrailRenderer.emitting = true;
            Invoke("DesactivateDashTrailRenderer", 0.8f);
        }
    }
    private int IsLookingRightToInt(bool isLookingRight)
    {
        if (isLookingRight)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.useGravity = false;
        rb.linearVelocity = new Vector2(IsLookingRightToInt(isLookingRight) * dashForce, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.useGravity = true;
        yield return new WaitForSeconds(timeCanDash);
        canDash = true;
    }
    void DesactivateDashTrailRenderer()
    {
        dashTrailRenderer.emitting = false;
    }
    void Flip()
    {
        /*Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;*/
        

        transform.rotation = Quaternion.Euler(0, isLookingRight ? 180 : 0, 0);

        isLookingRight = !isLookingRight;

    }
}
