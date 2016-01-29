using UnityEngine;
using System.Collections;

public class movePlatform : MonoBehaviour {

    private float timer = 0;
    public float time = 0f;
    public float speed = 0f;

    public enum direction {right, left, up, down}
    public direction currentDirection;

    void Update()
    {
        Move();
    }


    void OnCollisionStay2D(Collision2D col2d)
    {
        if (col2d.collider.CompareTag("Player"))
        {
            col2d.transform.SetParent(this.transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D col2d)
    {
        if (col2d.collider.CompareTag("Player"))
        {
            col2d.transform.SetParent(null, true);
        }
    }

    void Move()
    {
        switch (currentDirection)
        {
            case direction.right:
                if(timer < time)
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
            case direction.up:
                if (timer < time)
                {
                    timer += Time.deltaTime;
                    this.transform.Translate(Vector2.up * speed * Time.deltaTime);
                }
                else
                {
                    timer = 0f;
                    currentDirection = direction.down;
                }
                break;
            case direction.down:
                if (timer < time)
                {
                    timer += Time.deltaTime;
                    this.transform.Translate(Vector2.down * speed * Time.deltaTime);
                }
                else
                {
                    timer = 0f;
                    currentDirection = direction.up;
                }
                break;
        }
    }
}
