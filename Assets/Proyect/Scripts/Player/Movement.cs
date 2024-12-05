using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("BasicMovement")]
    Rigidbody rb;
    [SerializeField]
    float speed;
    float horizontalMovement;
    bool isLookingRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private bool isGrounded = false;
    [SerializeField] private Transform groundChecker; 
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Dash")]
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashForce = 20f;
    [SerializeField]
    private float timeCanDash = 1f;
    private bool isDashing;
    private bool canDash = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {

    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);

        rb.linearVelocity = new Vector3(horizontalMovement * speed, rb.linearVelocity.y);
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
    public void ActivateDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Dash());
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
        Debug.Log(IsLookingRightToInt(isLookingRight) * dashForce);
        rb.linearVelocity = new Vector2(IsLookingRightToInt(isLookingRight) * dashForce, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.useGravity = true;
        yield return new WaitForSeconds(timeCanDash);
        canDash = true;
        Debug.Log("Hace");
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }
    void Flip()
    {
        /*Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        */

        transform.rotation = Quaternion.Euler(0, isLookingRight ? 180 : 0, 0);

        isLookingRight = !isLookingRight;

    }
}
