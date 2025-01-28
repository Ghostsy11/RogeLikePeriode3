using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagment : MonoBehaviour
{
    [Header("InventorySlot1")]
    [SerializeField] bool pickOrDrop1;
    [SerializeField] Button button1;
    [SerializeField] Image gun1Image;
    [Header("InventorySlot1")]
    [SerializeField] bool pickOrDrop2;
    [SerializeField] Button button2;
    [SerializeField] Image gun2Image;
    [Header("InventorySlot1")]
    [SerializeField] bool pickOrDrop3;
    [SerializeField] Button button3;
    [SerializeField] Image gun3Image;


    // Refreancing and chacing fields
    AimmingDireaction aimmingDireaction;
    private List<GameObject> cachedGuns = new List<GameObject>(); // Cache of guns to track changes
    private int previousGunCount = -1; // Tracks previous gun count to avoid unnecessary updates

    private void Awake()
    {
        aimmingDireaction = FindObjectOfType<AimmingDireaction>();
        gun1Image.sprite = null;
        gun2Image.sprite = null;
        gun3Image.sprite = null;
    }

    private void Update()
    {
        // Check if guns in the inventory have changed
        if (HasGunListChanged())
        {
            UpdateInventoryVisuals();
        }

        // Handle key input for selecting guns
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SelectGun(0);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SelectGun(1);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SelectGun(2);
        }
    }

    private bool HasGunListChanged()
    {
        // Check if gun list count has changed
        if (cachedGuns.Count != aimmingDireaction.gun.Count)
        {
            cachedGuns = new List<GameObject>(aimmingDireaction.gun); // Copy the gun list
            return true;
        }

        // Check if any item in the list has changed
        for (int i = 0; i < cachedGuns.Count; i++)
        {
            if (cachedGuns[i] != aimmingDireaction.gun[i])
            {
                cachedGuns = new List<GameObject>(aimmingDireaction.gun); // Copy the gun list
                return true;
            }

        }
        return false; // No changes detected
    }

    private void UpdateInventoryVisuals()
    {
        // Update visuals for each slot
        if (aimmingDireaction.gun.Count > 0)
        {
            InventoryVisuals(0, gun1Image);
        }
        else
        {
            gun1Image.sprite = null;
        }

        if (aimmingDireaction.gun.Count > 1)
        {
            InventoryVisuals(1, gun2Image);
        }
        else
        {
            gun2Image.sprite = null;
        }

        if (aimmingDireaction.gun.Count > 2)
        {
            InventoryVisuals(2, gun3Image);
        }
        else
        {
            gun3Image.sprite = null;
        }
    }

    private void SelectGun(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < aimmingDireaction.gun.Count && aimmingDireaction.gun[index] != null)
        {
            // Deactivate the current gun
            aimmingDireaction.gun[aimmingDireaction.index].gameObject.SetActive(false);

            // Activate the selected gun
            aimmingDireaction.index = index;
            aimmingDireaction.gun[index].gameObject.SetActive(true);
        }
    }
    private void InventoryVisuals(int index, Image gunImage)
    {
        if (index >= 0 && index < aimmingDireaction.gun.Count)
        {
            gunImage.sprite = aimmingDireaction.gun[index].GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            gunImage.sprite = null;
        }
    }

}
