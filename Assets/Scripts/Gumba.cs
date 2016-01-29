using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]

public class Gumba : MonoBehaviour {
    //Control stuffs
    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sp;
    private CircleCollider2D cc2D;
    private AudioSource aud;

    //AI
    private float timer = 0;
    public float time = 0f;
    public float speed = 0f;
    private bool die = false;
    public float playerUpForce = 0f;
    public AudioClip smash;

    public enum direction { right, left }
    public direction currentDirection;

    void Awake()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        sp = this.GetComponent<SpriteRenderer>();
        cc2D = this.GetComponent<CircleCollider2D>();
        aud = this.GetComponent <AudioSource>();
    }

    void Update()
    {
        if (!die)
        {
            Move();
        }
    }

    void OnTriggerEnter2D(Collider2D coll2D)
    {
        if (coll2D.CompareTag("Player"))
        {
            die = true;
            Rigidbody2D player_rb2D = coll2D.GetComponent<Rigidbody2D>();
            player_rb2D.velocity = new Vector2(player_rb2D.velocity.x, 0f);
            player_rb2D.AddForce(Vector2.up * playerUpForce, ForceMode2D.Impulse);
            Die();
        }
    }

    void Die()
    {
        aud.PlayOneShot(smash, 7f);
        anim.SetTrigger("Die");
        rb2D.isKinematic = false;
        cc2D.enabled = false;
        Destroy(this.gameObject, 5f);

    }

    //AI Stupid
    void Move()
    {
        switch (currentDirection)
        {
            case direction.right:
                sp.flipX = true;
                if (timer < time)
                {
                    timer += Time.deltaTime;
                    this.transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else
                {
                    timer = 0f;
                    currentDirection = direction.left;
                }
                break;
            case direction.left:
                sp.flipX = false;
                if (timer < time)
                {
                    timer += Time.deltaTime;
                    this.transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                else
                {
                    timer = 0f;
                    currentDirection = direction.right;
                }
                break;
        }
    }
}
