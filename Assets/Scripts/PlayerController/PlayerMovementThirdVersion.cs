using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovementThirdVersion : MonoBehaviour
{

    private Rigidbody2D rigidbody2;
    private Vector3 moveDireaction;
    private Vector3 rollDireeaction;
    [SerializeField] float rollSpeed;
    [SerializeField] float rollSpeedDropMutiplayer;
    private Vector3 lastMoveDir;
    private enum State
    {
        Normal,
        Rolling
    }

    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float dashPower;
    private bool isdashBottonDown;
    [SerializeField] LayerMask layerMask;
    private State state;

    void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }

    void Update()
    {

        HandleMovement();

    }
    private void FixedUpdate()
    {

        // rigidbody2.velocity = moveDireaction * playerSpeed;
        rigidbody2.velocity = moveDireaction * playerSpeed;
        Dash();

    }



    private void HandleMovement()
    {
        switch (state)
        {
            case State.Normal:
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
                    lastMoveDir = moveDireaction;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isdashBottonDown = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDireeaction = lastMoveDir;
                    rollSpeed = 250f;
                    state = State.Rolling;

                }
                break;
            case State.Rolling:
                rollSpeed -= rollSpeed * rollSpeedDropMutiplayer * Time.deltaTime;

                float rollSpeedMinimum = 50f;
                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                }

                break;
        }
    }

    private void Dash()
    {
        switch (state)
        {
            case State.Normal:

                if (isdashBottonDown)
                {
                    Vector3 dashPoition = transform.position + lastMoveDir * dashPower;

                    RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, lastMoveDir, dashPower, layerMask);
                    if (raycastHit2D.collider != null)
                    {
                        dashPoition = raycastHit2D.point;
                    }

                    rigidbody2.MovePosition(dashPoition);
                    isdashBottonDown = false;

                }
                break;
            case State.Rolling:
                rigidbody2.velocity = rollDireeaction * rollSpeed;
                break;
        }
    }

}

