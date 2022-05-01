using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float nextWaypointDistance = 3f;
    public float seekRange;
    private float slugDistance;
    private Transform currentDirection;

    public Transform enemySprite;

    Path path;
    private int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && slugDistance <= seekRange)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        //we will constantly be checking the distance between the enemy object and the player
        slugDistance = Vector3.Distance(this.transform.position, target.position);

    }

    
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if(slugDistance <= seekRange)
        {
            rb.AddForce(force);
        }
        

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //check to see if the enemy needs to rotate
        if(force.x >= 0.01f)
        {
            enemySprite.localScale = new Vector3(-2f, 2f, 1f);
        }
        else if(force.x <= -0.01f)
        {
            enemySprite.localScale = new Vector3(2f, 2f, 1f);
        }
    }
}
