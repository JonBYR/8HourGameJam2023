using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public Camera cam;
    private bool facingRight = true;
    private bool facingUp;
    public float playerSpeed = 2f;
    Vector2 movement;
    public float rotationSpeed;
    int lives = 3;
    public float knockbackForce = 0.5f;
    public bool godMode = false;
    public VolumeProfile vol;
    UnityEngine.Rendering.Universal.FilmGrain grain;
    UnityEngine.Rendering.Universal.ColorAdjustments col;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (vol.TryGet<FilmGrain>(out grain))
        {
            grain.intensity.Override(1f); //gets post prossing effect and manipulates in script
        }
        if (vol.TryGet<ColorAdjustments>(out col))
        {
            col.contrast.Override(-24f);
            col.postExposure.Override(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //gets mouse position
        if(godMode)
        {
            GodPowers();
        }
    }
    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * playerSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - playerRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        playerRb.rotation = angle; //ensures player faces the correct direction
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(lives >= 0 && col.gameObject.tag == "Enemy")
        {
            lives--;
            if (lives == 0) SceneManager.LoadScene("DeathScene");
            Vector2 direction = (col.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            EnemyController forceStopper = col.gameObject.GetComponent<EnemyController>();
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse); //if enemies hit the player they have an impulsive force directed onto them
            forceStopper.Invoke("StopForces", 2f);
        }
    }
    void GodPowers()
    {
        lives = 10000000; //ensures that player is never likely to run out of lives
        if(vol.TryGet<FilmGrain>(out grain))
        {
            grain.intensity.Override(0f);
        }
        if (vol.TryGet<ColorAdjustments>(out col))
        {
            col.contrast.Override(15f);
            col.postExposure.Override(3f);
        }
    }
}
