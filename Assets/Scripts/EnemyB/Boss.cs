using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBehavior
{

    [SerializeField] GameObject player;
    [SerializeField] float speed = 1.0f;
    [SerializeField] Animator animator;
    [SerializeField] float lineOfSight = 10f;

    [SerializeField] float nextFireTime;
    [SerializeField] float fireRate;
    [SerializeField] float shootingRange;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootpoint;
    [SerializeField] GameObject gun;
    [SerializeField] bool readyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateGunTowardsPlayer(gun, player);
        SmartFollowThePlayerAndShootWhenItsClose(gameObject, player, speed, animator, lineOfSight, shootingRange, bullet, shootpoint.gameObject.transform, Quaternion.identity, readyToShoot);
        nextFireTime += Time.deltaTime;


        if (nextFireTime > fireRate && !readyToShoot)
        {
            nextFireTime = 0f;
            readyToShoot = true;
        }
        else
        {
            readyToShoot = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }






}
