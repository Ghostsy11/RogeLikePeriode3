using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Genereal Settings")]
    #region

    [Tooltip("How fast wish you the player go")]
    [SerializeField] float playerSpeed;
    [SerializeField] bool playerFacingRight;
    #endregion
    #region
    [Header("Refreances Managed through code")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [Tooltip("Vector value of the moving player")]
    [SerializeField] Vector2 moveMent;
    Animator playerAnimator;
    Vector2 lastMoveValue;

    [Header("Refreances Managed Manually not via code")]
    [SerializeField] SpriteRenderer spriteRenderer;
    #endregion
    #region
    [Header("Dash Settengis")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDasing;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCoolDown = 1f;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] bool isDashBottonDown;
    [SerializeField] RaycastHit2D raycastHit2D;
    [SerializeField] LayerMask dashLayerMask;

    #endregion

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
        playerFacingRight = true;


    }

    void Update()
    {

        // Input handling
        Movemeant();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canDash)
            {
                isDashBottonDown = true;
            }
            else
            {
                return;
            }
        }


    }

    void FixedUpdate()
    {

        // PlayerMovement
        rigidbody2D.MovePosition(rigidbody2D.position + Movemeant() * playerSpeed * Time.fixedDeltaTime);
        StartCoroutine(DashUp());

    }

    public Vector2 Movemeant()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX == 0 && moveY == 0 && (moveMent.x != 0 || moveMent.y != 0))
        {
            lastMoveValue = moveMent;

            playerAnimator.SetFloat("LastMoveX", lastMoveValue.x);
            playerAnimator.SetFloat("LastMoveY", lastMoveValue.y);
        }

        moveMent.x = Input.GetAxisRaw("Horizontal");
        moveMent.y = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("Horizontal", moveMent.x);
        playerAnimator.SetFloat("Vertical", moveMent.y);

        playerAnimator.SetFloat("Speed", moveMent.sqrMagnitude);
        return moveMent.normalized;
    }


    private IEnumerator DashUp()
    {
        if (isDashBottonDown && canDash)
        {
            isDashBottonDown = false;
            canDash = false;
            isDasing = true;
            trailRenderer.emitting = true;

            Vector2 dashPosition = rigidbody2D.position + Movemeant() * dashingPower;

            raycastHit2D = Physics2D.Raycast(rigidbody2D.position, Movemeant(), dashingPower, dashLayerMask);
            if (raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;

            }
            rigidbody2D.MovePosition(dashPosition);
            yield return new WaitForSeconds(dashingTime);


            trailRenderer.emitting = false;
            isDasing = false;
            yield return new WaitForSeconds(dashingCoolDown);

            canDash = true;
        }
        StopCoroutine(DashUp());

    }
}
