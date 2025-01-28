using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Behavior : MonoBehaviour, IIntractable
{
    [Header("General Settings")]
    [Tooltip("Sting Parameter most be manually typed")]
    [SerializeField] string AnimatorStringParameter = "open";

    [Header("Animator Settings")]
    [Tooltip("Animator most be manually connected")]
    [SerializeField] Animator animator;

    [Header("Debugging Settings")]
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] bool openClose;
    [SerializeField] bool openOnce;

    [Tooltip("Ref Will Be Manually Handled")]
    [SerializeField] GameObject player;
    [SerializeField] bool pickOrDrop;
    [SerializeField] AimmingDireaction AimmingDireaction;
    [SerializeField] GameObject reward;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AimmingDireaction = FindObjectOfType<AimmingDireaction>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }


    private void Start()
    {
        if (reward != null)
        {

            reward.SetActive(false);
        }
        else { return; }
    }

    // every region represent an Interface Method and Implmeantion
    // Weapon Pick Up
    #region
    public void pickUpOrDropWeapon()
    {
        WeaponHandler();
    }

    private void WeaponHandler()
    {
        if (rigidbody2D.bodyType != RigidbodyType2D.Dynamic)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
        if (!pickOrDrop)
        {
            pickOrDrop = !pickOrDrop;
            if (AimmingDireaction.gun.Count < 3)
            {

                transform.parent = player.transform;
                gameObject.transform.position = player.transform.position;
                AimmingDireaction.gun.Add(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Invetory Is Full");
            }

        }
        else
        {

            pickOrDrop = !pickOrDrop;
            transform.parent = null;
            AimmingDireaction.gun.Remove(gameObject);
            gameObject.gameObject.SetActive(true);

        }
    }

    #endregion
    // Single use intraction
    #region
    public void IntreactableSingleUse()
    {
        SingleUse(animator);
    }

    private void SingleUse(Animator animator)
    {
        boxCollider2D.isTrigger = false;
        animator.SetBool(AnimatorStringParameter, openOnce = true);
    }
    #endregion
    // Multie intraction
    #region
    // More Than One Use
    public void IntreactableObjects()
    {
        if (animator != null)
        {
            OpenClose();
        }
    }
    private void OpenDoor(Animator animator)
    {
        boxCollider2D.isTrigger = true;
        animator.SetBool(AnimatorStringParameter, openClose);
    }

    private void CloseDoor(Animator animator)
    {
        boxCollider2D.isTrigger = false;
        animator.SetBool(AnimatorStringParameter, openClose);
    }

    private void OpenClose()
    {
        if (!openClose)
        {
            if (reward != null)
            {

                reward.gameObject.SetActive(true);
            }
            openClose = !openClose;
            OpenDoor(animator);

        }
        else
        {
            openClose = !openClose;
            CloseDoor(animator);
        }

    }
    #endregion

}