using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimmingToMouseVersionTwo : MonoBehaviour
{

    [SerializeField] Camera camera;
    private Vector3 mousePosition;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePosition - transform.position;

        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotz);

    }
}
