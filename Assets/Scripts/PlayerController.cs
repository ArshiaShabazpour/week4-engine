using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;
    private bool gameOver = false;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {
        if (DLLConfigLoader.IsLoaded)
        {
            moveSpeed = DLLConfigLoader.PlayerMoveSpeed;
            jumpForce = DLLConfigLoader.PlayerJumpForce;
        }
    }

    void Update()
    {
        if (gameOver) return; 

        float h = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(h * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts.Length > 0 && col.contacts[0].normal.y > 0.5f)
            isGrounded = true;

        if (col.gameObject.CompareTag("Finish"))
        {
            WinGame();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Finish"))
            isGrounded = false;
    }

    void WinGame()
    {
        gameOver = true;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("🎉 You Win! 🎉");

        Time.timeScale = 0f;
    }
}
