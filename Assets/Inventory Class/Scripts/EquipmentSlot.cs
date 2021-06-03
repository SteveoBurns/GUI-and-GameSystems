using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EquipmentSlot
{
    [SerializeField] private Item item;

    public Item EquipedItem
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            itemEquiped.Invoke(this); // change to this
        }
    }
    public Transform visualLocation;
    public Vector3 offset;

    public delegate void ItemEquiped(EquipmentSlot item); // Change to equipment slot
    public event ItemEquiped itemEquiped;

    
}
