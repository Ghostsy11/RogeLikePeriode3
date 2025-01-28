using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : EnemyBehavior
{

    [SerializeField] GameObject player;
    [SerializeField][Range(0, 1)] float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        FollowPlayer(gameObject.transform, player.transform, speed);

    }
}
