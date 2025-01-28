using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimmingDireaction : MonoBehaviour
{


    [SerializeField] float gunDistance;
    public List<GameObject> gun;
    public int index;
    [SerializeField] bool gunFacingRight;

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

        if (index >= 0 && gun.Count > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direaction = mousePos - transform.position;
            gun[index].transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direaction.y, direaction.x) * Mathf.Rad2Deg));


            float angle = Mathf.Atan2(direaction.y, direaction.x) * Mathf.Rad2Deg;
            gun[index].transform.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

            GunFlipContoller(mousePos);
        }
        else
        {
            return;
        }

    }

    private void GunFlipContoller(Vector3 mousePos)
    {
        if (mousePos.x < gun[index].transform.position.x && gunFacingRight)
        {
            GunFilp();
        }
        else if (mousePos.x > gun[index].transform.position.x & !gunFacingRight)
        {
            GunFilp();
        }
    }

    private void GunFilp()
    {
        gunFacingRight = !gunFacingRight;
        gun[index].transform.localScale = new Vector3(gun[index].transform.localScale.x, gun[index].transform.localScale.y * -1, gun[index].transform.localScale.z);
    }
}
