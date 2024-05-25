using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMesh : MonoBehaviour
{
    Transform target;

    NavMeshAgent agent;
    public float speed = 3f;
    public float rotationSpeed = 3f;
    Rigidbody2D enemyrb;
    // Start is called before the first frame update
    void Start() //script taken from this tutorial https://www.youtube.com/watch?v=HRX0pUSucW4
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemyrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle)); //ensures enemies look at the player
        agent.SetDestination(target.position); //this will allow enemies to continually follow the player
    }
    public void StopForces()
    {
        enemyrb.velocity = Vector2.zero;
        enemyrb.angularVelocity = 0; //Invoked in player to stop enemy pushback
    }
    //when using Navmeshes in 2d the NavMeshComponents folder is required from this git repo https://github.com/h8man/NavMeshPlus alongside the AI Navigation package in package manager
}
