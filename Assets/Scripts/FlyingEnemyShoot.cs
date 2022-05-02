using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyShoot : MonoBehaviour
{

    //public transform that will track the player
    private Transform slug;
    public GameObject projectile;

    [Header("FiringAttributes")]

    public Transform firePoint;
    public float fireRate;
    private float fireCountdown = 0f;
    public float shootingRange;
    public float projectileSpeed;
    private bool hasKilledRecently;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        slug = GameObject.FindGameObjectWithTag("Player").transform;
        animator = this.GetComponent<Animator>();
        hasKilledRecently = PlayerManager.hasDiedRecently;
    }

    // Update is called once per frame
    void Update()
    {
        hasKilledRecently = PlayerManager.hasDiedRecently;
        //we will check to see if slugboy is within range of the flying enemy's fire range
        if (slug != null)
        {
            //if slugboy is still in the game, calculate the distance
            float distance = Vector3.Distance(this.transform.position, slug.position);
            
            //now we will check to see if slugboy is within firing range of the enemy
            if(distance < shootingRange && hasKilledRecently == false)
            {
                if(fireCountdown <= 1f)
                {
                    animator.SetBool("chargingShot", true);
                }
                

                if (fireCountdown <= 0f)
                {
                    animator.SetBool("chargingShot", false);
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }

            else
            {
                animator.SetBool("chargingShot", false);
                fireCountdown = 1f / fireRate;
            }
        }
    }

    //method to instantiate bullets towards the player
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(projectile, firePoint.position, firePoint.rotation);
        projectileMovement bullet = bulletGO.GetComponent<projectileMovement>();

        //if the bullet exists, call the seek method
        if (bullet != null)
        {
            bullet.chaseTarget(slug, projectileSpeed);
        }
    }
}
