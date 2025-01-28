using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public void FollowPlayer(Transform originPoint, Transform playerPoint, float speed)
    {

        Vector3 velocity = Vector3.zero;

        transform.position = Vector3.SmoothDamp(originPoint.transform.position, playerPoint.transform.position, ref velocity, speed);

    }

    public void RotateGunTowardsPlayer(GameObject gun, GameObject player)
    {
        // Get the position of the gun and player
        Vector3 gunPosition = gun.transform.position;
        Vector3 playerPosition = player.transform.position;

        // Calculate the direction from the gun to the player
        Vector3 direction = playerPosition - gunPosition;

        // Calculate the angle in radians and convert it to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the gun (only on the Z-axis for 2D rotation)
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    public void LookAtPlayer()
    {


    }
    public void ShootPlayer(Transform originPoint, Transform playerPoint)
    {

    }

    public void Look360AndShootAround()
    {


    }



    public void SmartFollowThePlayerAndShootWhenItsClose(GameObject enemy, GameObject player, float speed, Animator animator, float lineOfSight, float shootingRange, GameObject bullet, Transform shootingPoint, Quaternion rotation, bool readyToShoot)
    {
        float xDirection = 0f;
        float yDirection = 0f;
        float distance = Vector2.Distance(enemy.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - enemy.transform.position;

        if (distance < lineOfSight && distance > shootingRange)
        {
            direction.Normalize();
            transform.position = Vector2.MoveTowards((enemy.transform.position), player.transform.position, speed * Time.deltaTime);

            xDirection = Mathf.Clamp(direction.x, -1f, 1f);
            yDirection = Mathf.Clamp(direction.y, -1f, 1f);

        }
        else if (distance <= shootingRange && readyToShoot)
        {
            Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        }
        else
        {
            xDirection = 0f;
            yDirection = 0f;
        }


        AnimateEnemy(animator, xDirection, yDirection);

    }


    public void AnimateEnemy(Animator animator, float xDireactionNormalized, float yDireactionNormalized)
    {
        animator.SetFloat("Xaxis", xDireactionNormalized);
        animator.SetFloat("Yaxis", yDireactionNormalized);
    }

    public void AnimateEnemy(Animator animator, Vector2 enemyVector, string idle, string moveUp, string moveDown)
    {

    }
    public void AnimateEnemy(Animator animator, Vector2 enemyVector, string moveRight, string moveLeft)
    {

    }

    protected void Patrol(Transform objTransform, float speed, float range, float startingX)
    {
        float newX = startingX + Mathf.PingPong(Time.time * speed, range * 2) - range;
        objTransform.position = new Vector3(newX, objTransform.position.y, objTransform.position.z);
    }

    protected bool IsTargetInRange(Transform source, Transform target, float detectionRange)
    {
        float distance = Vector2.Distance(source.position, target.position);
        return distance <= detectionRange;
    }

    protected IEnumerator LaunchRockets(GameObject rocketPrefab, Transform launchPoint, float launchSpeed, float interval)
    {
        while (true)
        {
            for (int i = 0; i < 360; i += 45) // Launch in 8 directions
            {
                float angleRad = i * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

                // Instantiate the rocket
                GameObject rocket = Instantiate(rocketPrefab, launchPoint.position, Quaternion.identity);

                // Set the rocket's velocity
                Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * launchSpeed;
                }

                // Set the rocket's rotation to match its direction
                float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rocket.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
            }

            yield return new WaitForSeconds(interval);
        }
    }

    protected Coroutine StartLaunchingRockets(MonoBehaviour context, GameObject rocketPrefab, Transform launchPoint, float launchSpeed, float interval)
    {
        return context.StartCoroutine(LaunchRockets(rocketPrefab, launchPoint, launchSpeed, interval));
    }

    protected void StopLaunchingRockets(MonoBehaviour context, Coroutine launchRoutine)
    {
        if (launchRoutine != null)
        {
            context.StopCoroutine(launchRoutine);
        }
    }

}