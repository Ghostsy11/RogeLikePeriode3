using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int objectiveCount;
    [SerializeField] GameObject UIwinning;

    private void Start()
    {
        UIwinning.SetActive(false);
    }

    public void ResetQuest()
    {
        objectiveCount = 0;
        text.text = objectiveCount.ToString();
        UIwinning.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void UpdateQuest()
    {
        objectiveCount++;
        text.text = objectiveCount.ToString();
        if (objectiveCount == 6)
        {
            UIwinning.SetActive(true);
        }
        else
        {
            return;
        }
    }

}
