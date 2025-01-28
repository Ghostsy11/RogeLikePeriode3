using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] VFX hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            hitEffect.PlayVFX(collision.gameObject.transform);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            hitEffect.PlayVFX(collision.gameObject.transform);
            //Effect
        }
    }



    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }
}
