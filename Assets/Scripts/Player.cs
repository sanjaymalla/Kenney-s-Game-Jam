using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    Rigidbody2D rb;
    bool isGrounded;

    public bool isMoving { get; private set; }
    public bool isJumping { get; private set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        isMoving = Mathf.Abs(moveX) >0.1f;

        if (isGrounded)
        {
            bool jump = Input.GetButtonDown("Jump");
            if(jump)
            {
                rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            }
        }

        isJumping = !isGrounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
