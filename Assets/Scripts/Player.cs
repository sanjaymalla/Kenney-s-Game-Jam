using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    Rigidbody2D rb;
    bool isGrounded;

    private Animator anim;
    private HealthManager healthManager;

    public bool isMoving { get; private set; }
    public bool isJumping { get; private set; }
    public bool isRecharge { get; private set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        healthManager = FindAnyObjectByType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //flipping player left and right while moving
        if (horizontalInput > 0.1f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        isMoving = Mathf.Abs(horizontalInput) >0.1f;

        if (isGrounded)
        {
            bool jump = Input.GetButtonDown("Jump");
            if(jump)
            {
                rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            }
        }

        isJumping = !isGrounded;

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if(collision.gameObject.CompareTag("Recharge"))
        {
            isRecharge = true;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Recharge"))
        {
            isRecharge = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Battery"))
        {
            if(healthManager != null)
            {
                healthManager.Heal(30f);
            }

            Destroy(collision.gameObject);
        }
    }


}
