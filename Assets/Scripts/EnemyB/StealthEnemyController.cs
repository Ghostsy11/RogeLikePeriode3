using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthEnemyController : EnemyBehavior
{
    [Header("Patrol Settings")]
    public float patrolSpeed = 4f; // Faster patrol speed
    public float patrolRange = 7f;

    [Header("Player Detection")]
    public float detectionRange = 12f; // Larger detection range
    public GameObject player;

    [Header("Rocket Launcher")]
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float launchSpeed = 8f; // Faster rocket speed
    public float launchInterval = 1.5f; // faster launch rate

    private float startingX;
    private Coroutine launchRoutine;

    private void Start()
    {
        startingX = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Patrol(transform, patrolSpeed, patrolRange, startingX);

        // Override: Stealth behavior while patrolling
        ToggleVisibility(false);

        if (IsTargetInRange(transform, player.transform, detectionRange))
        {
            if (launchRoutine == null)
            {
                // Use the new StartLaunchingRocketsIn4Directions method
                launchRoutine = StartLaunchingRocketsIn4Directions(this, rocketPrefab, launchPoint, launchSpeed, launchInterval);
            }
        }
        else if (launchRoutine != null)
        {
            StopLaunchingRockets(this, launchRoutine);
            launchRoutine = null;
        }
    }

    private IEnumerator LaunchRocketsIn4Directions(GameObject rocketPrefab, Transform launchPoint, float launchSpeed, float interval)
    {
        while (true)
        {
            for (int i = 0; i < 360; i += 90) // Launch in 4 directions (N, E, S, W)
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

    private Coroutine StartLaunchingRocketsIn4Directions(MonoBehaviour context, GameObject rocketPrefab, Transform launchPoint, float launchSpeed, float interval)
    {
        return context.StartCoroutine(LaunchRocketsIn4Directions(rocketPrefab, launchPoint, launchSpeed, interval));
    }

    private void ToggleVisibility(bool isVisible)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = isVisible;
        }
    }
}
