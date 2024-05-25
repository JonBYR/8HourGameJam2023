using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;
    public float speed = 3f;
    public float rotationSpeed = 3f;
    Rigidbody2D enemyrb;
    // Start is called before the first frame update
    void Start() //legacy script
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        enemyrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle)); //ensures enemies look at the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //enemies move towards player
    }
    public void StopForces()
    {
        enemyrb.velocity = Vector2.zero;
        enemyrb.angularVelocity = 0; //Invoked in player to stop enemy pushback
    }
}
