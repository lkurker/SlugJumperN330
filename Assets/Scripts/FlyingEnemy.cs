using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float nextWaypointDistance = 3f;
    public float seekRange;
    private float slugDistance;
    private Transform currentDirection;
    private Vector2 startingPosition;
    private bool hasKilledRecently;

    public Transform enemySprite;

    Path path;
    private int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = this.transform.position;
        //we will set the 
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        //bool to check if the target has recently died
        hasKilledRecently = PlayerManager.hasDiedRecently;
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && slugDistance <= seekRange && hasKilledRecently == false)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

        //if the player is not in range or has recently died
        else if(seeker.IsDone() && (hasKilledRecently == true || slugDistance >= seekRange))
        {
            seeker.StartPath(rb.position, startingPosition, OnPathComplete);
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

        //we will keep updating the bool so that slug boy does not continuously get killed
        hasKilledRecently = PlayerManager.hasDiedRecently;

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

        if(slugDistance <= seekRange && reachedEndOfPath == false)
        {
            rb.AddForce(force);
        }

        else if((slugDistance >= seekRange || hasKilledRecently == true) && reachedEndOfPath == false)
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
