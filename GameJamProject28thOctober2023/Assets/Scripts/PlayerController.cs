using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private bool facingRight = true;
    private bool facingUp;
    public float playerSpeed = 2f;
    Vector2 movement;
    public float rotationSpeed;
    int lives = 3;
    public float knockbackForce = 0.5f;
    public bool godMode = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement.normalized * playerSpeed * Time.fixedDeltaTime);

        if(movement.y > 0 && movement.x == 0)
        {
            transform.rotation = Quaternion.FromToRotation(playerRb.position, Vector3.down);
        }
        else if (movement.y < 0 && movement.x == 0)
        {
            transform.rotation = Quaternion.FromToRotation(playerRb.position, Vector3.up);
        }
        else if (movement.y == 0 && movement.x > 0)
        {
            transform.rotation = Quaternion.FromToRotation(playerRb.position, Vector3.left);
        }
        else if (movement.y == 0 && movement.x < 0)
        {
            transform.rotation = Quaternion.FromToRotation(playerRb.position, Vector3.right);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(lives >= 0 && col.gameObject.tag == "Enemy")
        {
            lives--;
            Vector2 direction = (col.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            EnemyController forceStopper = col.gameObject.GetComponent<EnemyController>();
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
            forceStopper.Invoke("StopForces", 2f);
        }
    }
}
