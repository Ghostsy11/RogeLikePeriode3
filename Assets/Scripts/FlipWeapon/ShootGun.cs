using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    [SerializeField] GameObject hand1;
    [SerializeField] GameObject hand2;
    [SerializeField] SpriteRenderer shootGun;

    private void Start()
    {

        shootGun = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (shootGun.flipX == true)
        {
            hand1.transform.localPosition = new Vector3(-0.05f, 0.2f, 0f);
            hand2.transform.localPosition = new Vector3(-1.2f, 0.2f, 0f);
        }
        else
        {
            hand1.transform.localPosition = new Vector3(0.01f, 0.01f, 0f);
            hand2.transform.localPosition = new Vector3(1.2f, 0.2f, 0f);
        }

        // Calculate the gun's rotation angle in degrees
        float angle = transform.rotation.eulerAngles.z;

        // Ensure the angle is in the range -180 to 180
        if (angle > 180) angle -= 360;

        // Flip the gun sprite and scale based on the rotation angle
        if (angle > 90 || angle < -90)
        {
            // Flip along the X-axis when aiming left
            transform.localScale = new Vector3(1, -1, 1); // Flip vertically
        }
        else
        {
            // Reset flip when aiming right
            transform.localScale = new Vector3(1, 1, 1); // Reset vertical scale
        }

    }

}
