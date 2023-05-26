using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private LayerMask jumpableGround;

    private float directionX;
    private bool isJumping;
    private bool isDoubleJump;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField]
    private float dashingPower = 24f;
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashingCooldown = 1f;

    [SerializeField]
    private ParticleSystem moveEffect;
    [SerializeField]
    private ParticleSystem jumpEffect;

    [Range(0,10)]
    [SerializeField]
    private int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField]
    private float dustFormationPeriod;

    private float counter;

    private enum MovementState
    {
        Idle,
        Running,
        Jumping,
        Falling
    }
    private MovementState movementState;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        trailRenderer.emitting = false;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        counter += Time.deltaTime;
        if(IsGrounded() && Mathf.Abs(rb.velocity.x) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)
            {
                moveEffect.Play();
                counter = 0;
            }
        }

        directionX = Input.GetAxisRaw("Horizontal");
        ChangeDirection();
        UpdateAnimation();


        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_DASH);
            }
        }

        Jumping();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Moving(); 
    }

    private void ChangeDirection()
    {
        if(directionX > 0) // nhan vat quay sang phai
        {
            spriteRenderer.flipX = false;
        }

        if(directionX < 0) // nhan vat quay sang trai
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Moving()
    {
        rb.velocity = new Vector2(directionX * playerSpeed, rb.velocity.y);     
    }

    private void UpdateAnimation()
    {
        if(directionX != 0)
        {
            //Di chuyen
            movementState = MovementState.Running;
        }
        else
        {
            //Dung yen
            movementState = MovementState.Idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            //Nhay
            movementState = MovementState.Jumping;
        }

        if(rb.velocity.y < -0.1f)
        {
            //Roi
            movementState = MovementState.Falling;
        }

        animator.SetInteger("State", (int)movementState);
    }

    private void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if(IsGrounded() && !isJumping)
        {
            isDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || isDoubleJump)
            {
                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_JUMP);
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                isDoubleJump = !isDoubleJump;
                animator.SetBool("DoubleJump", !isDoubleJump);
                //Play effect jump
                if (!isDoubleJump)
                {
                    jumpEffect.Play();
                }
            }  
        }  
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center,
            boxCollider2D.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            jumpableGround);
    }

    private IEnumerator Dash()
    {
        canDash = false; //khi nhat vat luot, set = false de nhan vat ko luot lien tuc 2 lan
        isDashing = true; //ngan chan input cua nhan vat khi dang luot
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0; // set trong luc = 0 de khi nhan vat dang nhay ma luot se k bi roi
        rb.velocity = new Vector2(directionX * dashingPower, 0f); // set khoang cach luot cua nhan vat
        trailRenderer.emitting = true; //hien thi duong ve khi luot
        yield return new WaitForSeconds(dashingTime); // khoang thoi gian khi nhan vat luot
        trailRenderer.emitting = false; //tat duong ve khi luot xong
        rb.gravityScale = originalGravity; //dat trong luc ve gia tri ban dau
        isDashing = false; // cho nhan vat di chuyen binh thuong
        yield return new WaitForSeconds(dashingCooldown); //thoi gian cho de luot lan tiep theo
        canDash = true; // cho nhan vat luot lan tiep theo
    }
}
