using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;
    [SerializeField] float bulletSpeed;
    Rigidbody2D bulletRB;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        bulletRB = GetComponent<Rigidbody2D>();

        Vector2 moveDireaction = (target.transform.position - transform.position).normalized * bulletSpeed;
        bulletRB.velocity = moveDireaction;
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
