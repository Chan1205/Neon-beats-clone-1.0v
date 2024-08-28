using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    public bool isGround;

    [Header("Move")]
    float h;
    public int moveSpeed;
    bool isFacingRight = true;

    [Header("Jump")]
    public int jumpPower;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        Flip(); 
    }

    private void FixedUpdate()
    {
       h = Input.GetAxisRaw("Horizontal");

       rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        
       
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
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
}
