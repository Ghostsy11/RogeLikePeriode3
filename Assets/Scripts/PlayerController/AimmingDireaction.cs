using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimmingDireaction : MonoBehaviour
{


    [SerializeField] float gundistance;
    [SerializeField] Transform gun;
    [SerializeField] bool gunFacingRight;
    float lookAngle;

    private void Awake()
    {
        gunFacingRight = true;
    }

    void Update()
    {
        Aimming();
    }


    private void Aimming()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direaction = mousePos - transform.position;

        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direaction.y, direaction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direaction.y, direaction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gundistance, 0, 0);

        GunFlipContoller(mousePos);

    }

    private void GunFlipContoller(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
        {
            GunFilp();
        }
        else if (mousePos.x > gun.position.x & !gunFacingRight)
        {
            GunFilp();
        }
    }

    private void GunFilp()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }
}
