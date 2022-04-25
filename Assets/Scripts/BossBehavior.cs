using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    //we will be using the same waypoint system as before
    private Transform target;
    private int wavePointIndex;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        //set the target to the first waypoint
        target = Waypoints.waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = target.position - transform.position;
        //move the boss in the direction of the target
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //check to see if the boss has reached the waypoint
        if(Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    //function for retrieving the next waypoint
    void GetNextWayPoint()
    {
        //check to see if there are more waypoints to find
        if(wavePointIndex < Waypoints.waypoints.Length - 1)
        {
            wavePointIndex++;
            target = Waypoints.waypoints[wavePointIndex];
        }
    }

    //check to see if the boss has touched any platforms
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Platform" || collision.transform.tag == "Spike")
        {
            Debug.Log("COLLIDED");
            Destroy(collision.gameObject);
        }
    }
}
