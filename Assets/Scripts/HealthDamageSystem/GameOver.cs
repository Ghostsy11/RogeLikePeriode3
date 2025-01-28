using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public delegate void GameOverReset();
    public GameOverReset gameOver;
    [SerializeField] ObjectHealth PlayerHealth;
    [SerializeField] GameObject UIGameOver;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Scrollbar scrollbar; // UI Scrollbar representing health

    private void Start()
    {
        PlayerHealth = GetComponent<ObjectHealth>();
        gameOver += GetHealth;
        UIGameOver.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            CheckIfPlayerIsDead();

        }
    }

    public void CheckIfPlayerIsDead()
    {
        print("Working");
        gameOver?.Invoke();

    }

    public void GetHealth()
    {
        if (PlayerHealth.objectHealth <= 0)
        {
            print("Player is dead reset the world not by resetting the scene");
            UIGameOver.SetActive(true);


        }
        else if (PlayerHealth.objectHealth > 0)
        {
            print("Player is still alive");
        }
        else
        {
            return;
        }
    }

    public void ResetGame()
    {
        gameObject.SetActive(true);
        PlayerHealth.objectHealth = 100;
        scrollbar.size = 1f;
        gameObject.transform.position = new Vector3(0, 0, 0);

        UIGameOver.SetActive(false);
        foreach (var enemy in enemies)
        {
            // reset enemy position
            enemy.gameObject.SetActive(true);
            enemy.gameObject.GetComponent<ObjectHealth>().objectHealth = 100;

        }
    }



}
