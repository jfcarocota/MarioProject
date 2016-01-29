using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Mario : MonoBehaviour {

    //Move
    public float speed = 0f;
    public float MinVelocity = 0f;
    public float MaxVelocity = 0f;
    private bool accelerationEnabled = true;

    //Jump
    private bool jump_button;
    public float jumpForce = 0f;
    public AudioClip jump_audio;

    //GroundChecking
    private bool Ground = false;
    public Transform groundCheck;
    public float Radius;
    public LayerMask groundLayer;

    //Iputs
    private float h = 0;

    //control stuffs
    private Rigidbody2D rb2D;
    private SpriteRenderer sp;
    private Animator anim;
    private AudioSource aud;

    void Awake()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        sp = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        aud = this.GetComponent<AudioSource>();
    }

    void Move()
    {
        accelerationEnabled = rb2D.velocity.x > MinVelocity && rb2D.velocity.x < MaxVelocity;
        if (accelerationEnabled)
        {
            rb2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
        if(h == 0f && Ground)
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }

    void OnVelocityChange()
    {
        if((rb2D.velocity.x > 0f && h < 0f) || (rb2D.velocity.x < 0f && h > 0f))
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }

    void Jump()
    {
        if (jump_button)
        {
            anim.SetTrigger("Jump");
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            aud.PlayOneShot(jump_audio, 7f);
        }
    }

    void Flip()
    {
        if (h > 0f)
        {
            sp.flipX = false;
        }
        if (h < 0f)
        {
            sp.flipX = true;
        }
    }

    void FixedUpdate()
    {
        Ground = Physics2D.OverlapCircle(groundCheck.position,Radius,groundLayer);
        Move();
        OnVelocityChange();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal") * speed;
        jump_button = Input.GetButtonDown("Jump") && Ground;
        anim.SetFloat("h", Mathf.Abs(h));
        anim.SetBool("Ground", Ground);

    }

    void LateUpdate()
    {
        Flip();
        Jump();
        //if (Ground) { Debug.Log("Ground"); } 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, Radius);
    }
}
