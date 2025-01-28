using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ActionOnTime : MonoBehaviour
{

    private Action timeCallBack;
    private float timer;

    public void SetTimer(float timer, Action timeCallBack)
    {
        this.timer = timer;
        this.timeCallBack = timeCallBack;
    }
    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (IstimerComplete())
            {
                timeCallBack();
            }
        }
    }
    public bool IstimerComplete()
    {
        return timer <= 0f;
    }

}
