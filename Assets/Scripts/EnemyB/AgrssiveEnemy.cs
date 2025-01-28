using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgrssiveEnemy : EnemyBehavior
{
    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public float patrolRange = 5f;

    [Header("Player Detection")]
    public float detectionRange = 10f;
    public GameObject player;

    [Header("Rocket Launcher")]
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float launchSpeed = 5f;
    public float launchInterval = 1f;

    private float startingX;
    private Coroutine launchRoutine;

    private void Start()
    {
        startingX = transform.position.x;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Use inherited Patrol method
        Patrol(transform, patrolSpeed, patrolRange, startingX);

        // Use inherited IsTargetInRange method to check player proximity
        bool isPlayerInRange = IsTargetInRange(transform, player.transform, detectionRange);
        if (isPlayerInRange && launchRoutine == null)
        {
            // Use inherited StartLaunchingRockets method
            launchRoutine = StartLaunchingRockets(this, rocketPrefab, launchPoint, launchSpeed, launchInterval);
            ToggleVisibility(false); // Enemy becomes harder to see
        }
        else if (!isPlayerInRange && launchRoutine != null)
        {
            // Use inherited StopLaunchingRockets method
            StopLaunchingRockets(this, launchRoutine);
            launchRoutine = null;
            ToggleVisibility(true); // Enemy becomes visible
        }
    }

    /// <summary>
    /// Toggles visibility of the enemy.
    /// </summary>
    private void ToggleVisibility(bool isVisible)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = isVisible;
        }
    }
}
