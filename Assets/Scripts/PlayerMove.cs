using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    GameManager mangaer;

    public TextMeshProUGUI winText;
    public GameObject resetBtn;


    [Header("Move")]
    float h;
    public int moveSpeed;
    bool isFacingRight = true;
    public bool isGround;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 boxSize;


    [Header("Jump")]
    public int jumpPower;

    [Header("WallJump")]
    public Transform wallCheck;
    public LayerMask wallLayer;

    bool isWallSliding;
    public float wallSlidingSpeed = 2f;

    bool isWallJumping;
    public float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [Header("CoyoteTime")]
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

  


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
       h = Input.GetAxisRaw("Horizontal");

        if (!isWallJumping)
        {
            rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            Time.timeScale = 0f;
            winText.gameObject.SetActive(true);
            winText.text = "Lost";
            resetBtn.gameObject.SetActive(true);
        }

        if(collision.gameObject.tag == "Finish")
        {
            Time.timeScale = 0f;
            winText.gameObject.SetActive(true);
            resetBtn.gameObject.SetActive(true);
        }
    }

    //void Jump()
    //{
    //    if (IsGround())
    //    {
    //        coyoteTimeCounter = coyoteTime;
    //    }
    //    else
    //    {
    //        coyoteTimeCounter -= Time.deltaTime;
    //    }




    //    if (Input.GetButtonDown("Jump") && IsGround() && coyoteTimeCounter > 0f)
    //    {
    //        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    //    }

    //    if (Input.GetButtonDown("Jump") && IsGround() && coyoteTimeCounter < 0f)
    //    {
    //        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    //        coyoteTimeCounter = 0f;
    //    }
    //}

    void Jump()
{
    // Update Coyote Time counter if the player is grounded
    if (IsGround())
    {
        coyoteTimeCounter = coyoteTime;  // Reset the coyote time if grounded
    }
    else
    {
        coyoteTimeCounter -= Time.deltaTime;  // Countdown the coyote time
    }

    // Jump if the player presses jump within the coyote time window
    if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
    {
            //rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            rigid.velocity = Vector2.up * jumpPower;
            coyoteTimeCounter = 0f;  // Reset coyote time counter after jumping
    }

    
}

    bool IsGround()
    {
        if (Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Flip()
    {
        if(isFacingRight && h < 0 || !isFacingRight && h > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    bool WallCheck()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    void WallSlide()
    {
        if (!IsGround() && WallCheck() && h != 0f)
        {
            isWallSliding = true;
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, .2f);

        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rigid.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
