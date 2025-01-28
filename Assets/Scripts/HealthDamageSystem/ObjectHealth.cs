using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHealth : MonoBehaviour, IDamable
{
    public int objectHealth = 100; // Current health
    [SerializeField] int maxHealth = 100; // Maximum health
    [SerializeField] int damageAmount = 20; // Damage taken per hit
    [SerializeField] int restoreHealth = 10; // Health restored instantly
    [SerializeField] int restoreHealthAmount = 1; // Health restored incrementally
    [SerializeField] float restoreHealthInterval = 0.5f; // Time between each restore step
    [SerializeField] Scrollbar scrollbar; // UI Scrollbar representing health
    [SerializeField] GameOver gameOver;

    private Coroutine restoreHealthCoroutine;

    private void Start()
    {

        gameOver = GetComponent<GameOver>();
        // Initialize scrollbar size based on object health
        UpdateHealthBar();
    }

    private void Update()
    {
        // For testing: Press space to take damage, press R to restore health instantly
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(damageAmount);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreHealth();
        }

        // For testing: Press T to start restoring health over time
        if (Input.GetKeyDown(KeyCode.T))
        {
            RestoreHealthOverTime();
        }
    }

    /// <summary>
    /// Updates the scrollbar size based on the current health.
    /// </summary>
    private void UpdateHealthBar()
    {
        if (scrollbar != null)
        {
            float normalizedHealth = (float)objectHealth / maxHealth; // Normalize health to a value between 0 and 1
            scrollbar.size = normalizedHealth; // Set the size to reflect current health
            scrollbar.value = 1f; // Keep the health bar anchored to the right side
        }
    }

    /// <summary>
    /// Reduces the object's health and updates the health bar.
    /// </summary>
    /// <param name="damage">Amount of damage to take.</param>
    public void TakeDamage(int damage)
    {
        objectHealth = Mathf.Clamp(objectHealth - damage, 0, maxHealth); // Ensure health doesn't go below 0
        UpdateHealthBar(); // Update the scrollbar
    }

    /// <summary>
    /// Instantly restores a fixed amount of health and updates the health bar.
    /// </summary>
    public void RestoreHealth()
    {
        objectHealth = Mathf.Clamp(objectHealth + restoreHealth, 0, maxHealth); // Ensure health doesn't exceed max
        UpdateHealthBar(); // Update the scrollbar
    }

    /// <summary>
    /// Starts a coroutine to restore health incrementally over time.
    /// </summary>
    public void RestoreHealthOverTime()
    {
        if (restoreHealthCoroutine == null)
        {
            restoreHealthCoroutine = StartCoroutine(RestoreHealthWhenItsLow());
        }
    }

    /// <summary>
    /// Coroutine that restores health over time.
    /// </summary>
    private IEnumerator RestoreHealthWhenItsLow()
    {
        while (objectHealth < maxHealth)
        {
            objectHealth = Mathf.Clamp(objectHealth + restoreHealthAmount, 0, maxHealth); // Increment health
            UpdateHealthBar(); // Update the scrollbar
            yield return new WaitForSeconds(restoreHealthInterval); // Wait for the next increment
        }

        restoreHealthCoroutine = null; // Reset the coroutine reference when done
    }

    public void OnHit()
    {
        TakeDamage(damageAmount);
        if (gameObject.tag == "Player")
        {
            gameOver.CheckIfPlayerIsDead();

        }
        else
        {
            return;
        }
    }
}
