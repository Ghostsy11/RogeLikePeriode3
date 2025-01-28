using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    private GameObject playerQuest;
    private void Start()
    {
        playerQuest = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            var objective = collision.gameObject.GetComponent<ObjectHealth>();
            if (objective != null && objective.objectHealth > 0)
            {
                objective.OnHit();

            }
            else if (objective != null && objective.objectHealth <= 0)
            {
                if (objective.gameObject.tag == "Player")
                {
                    objective.gameObject.SetActive(false);
                }
                else if (objective.gameObject.tag == "Enemy")
                {


                    playerQuest.GetComponent<Quest>().UpdateQuest();
                    objective.gameObject.SetActive(false);


                }
            }
            else
            {
                return;
            }
        }
    }
}
