using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intreact : MonoBehaviour
{
    [Header("Genreal Settings")]
    [Tooltip("Tweak The values If you wish too")]
    [SerializeField] float intractionRange = 4f;
    [SerializeField] LayerMask canBeIntracted;
    [SerializeField] LayerMask canBeIntractedOnce;

    [Header("Debugging Section")]
    [SerializeField] Collider2D collider2d;
    [SerializeField] Collider2D singleUseCollider;
    [SerializeField] SFX nothingHitSound;
    [Header("Weapon Debugging Section")]
    [SerializeField] LayerMask weaponPickUp;
    [SerializeField] float rayLineLength = 5f;
    RaycastHit2D hit;


    void Update()
    {
        Intraction();

        SingleIntraection();

        WeaponIntraction();
    }

    private void FixedUpdate()
    {

    }

    private void Intraction()
    {
        collider2d = Physics2D.OverlapCircle(transform.position, intractionRange, canBeIntracted);
        if (collider2d != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(collider2d.gameObject.name);
            collider2d.GetComponent<IIntractable>().IntreactableObjects();
        }
        else
        {
            return;
        }
    }

    private void SingleIntraection()
    {
        singleUseCollider = Physics2D.OverlapCircle(transform.position, intractionRange, canBeIntractedOnce);

        if (singleUseCollider != null && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(singleUseCollider.gameObject.name);
            singleUseCollider.GetComponent<IIntractable>().IntreactableSingleUse();
        }
        else { return; }

    }

    private void WeaponIntraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector2 rayOragin = transform.position;
            Vector2 direation = Vector2.up;

            // Perform the raycast.
            hit = Physics2D.Raycast(rayOragin, direation, rayLineLength, weaponPickUp);
            if (hit.collider != null)
            {
                var weapon = hit.collider.GetComponent<IIntractable>();
                if (weapon != null)
                {
                    weapon.pickUpOrDropWeapon();
                }

                //Debug.DrawRay(rayOragin, direation * rayLineLength, Color.yellow);
                print(hit.collider.name);

            }
            else
            {
                // Debug.DrawRay(rayOragin, direation * rayLineLength, Color.white);
                print("Nothing Hit");
                nothingHitSound.PlayThisSound();

            }
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, intractionRange);
    }

}


/*
 *         if (Input.GetKeyDown(KeyCode.F))
        {
            hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), weaponPickUp);
            if (hit != null)
            {

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                print("Did hit");

                // Weapon Logic
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 5, Color.white);
                //Debug.Log("Did not Hit");
                print("Did not hit");
                return;
            }
        }
 */