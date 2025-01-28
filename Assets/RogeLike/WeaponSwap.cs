using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{

    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] int index = 0;

    void Start()
    {
        //InvokeRepeating(nameof(LoopThrowWeapons), 0f, 9.7f); // Calls every second

    }

    void Update()
    {

    }

    private void LoopThrowWeapons()
    {
        var currentIndex = weapons[index];
        Debug.Log($"Current Number: {currentIndex}");
        DisAbleWeapon();
        index = (index + 1) % weapons.Count;
        EnableWepaons();
    }


    private void DisAbleWeapon()
    {
        weapons[index].gameObject.SetActive(false);
    }

    private void EnableWepaons()
    {
        weapons[index].gameObject.SetActive(true);

    }



}
