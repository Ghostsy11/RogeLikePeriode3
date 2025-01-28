using System.Collections;
using UnityEngine;

public class PlayerMovementVersionTwo : MonoBehaviour
{
    Animator animator;

    private Rigidbody2D rigidbody2;
    private Vector3 moveDireaction;
    private bool isdashBottonDown;
    private Vector3 lastPos;
    [Header("Gerneral Settings")]
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float dashPower;
    [Tooltip("Layers that play player cant dast through")]
    [SerializeField] LayerMask layerMask;

    void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        HandleMovement();
    }
    private void FixedUpdate()
    {
        rigidbody2.velocity = moveDireaction * playerSpeed;
        Dash();

    }



    private void HandleMovement()
    {
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        moveDireaction = new Vector3(moveX, moveY, 0).normalized;

        if (moveX != 0 || moveY != 0)
        {
            lastPos = moveDireaction;
            PlayIdleAnimationOnStop();
        }

        lastPos.x = moveDireaction.x;
        lastPos.y = moveDireaction.y;

        PlayAnimation();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isdashBottonDown = true;
        }

    }

    private void Dash()
    {
        if (isdashBottonDown)
        {
            Vector3 dashPoition = transform.position + moveDireaction * dashPower;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDireaction, dashPower, layerMask);
            if (raycastHit2D.collider != null)
            {
                dashPoition = raycastHit2D.point;
            }

            rigidbody2.MovePosition(dashPoition);
            isdashBottonDown = false;

        }
    }

    private void PlayAnimation()
    {
        animator.SetFloat("Horizontal", moveDireaction.x);
        animator.SetFloat("Vertical", moveDireaction.y);
        animator.SetFloat("Speed", moveDireaction.sqrMagnitude);
    }
    private void PlayIdleAnimationOnStop()
    {

        animator.SetFloat("LastMoveX", lastPos.x);
        animator.SetFloat("LastMoveY", lastPos.y);
    }

}


// state machien
// behavoir tree