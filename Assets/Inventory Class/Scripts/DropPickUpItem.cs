using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUpItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform dropPoint;
    //[SerializeField] private Camera cameraItem;
    
    /// <summary>
    /// Drops the selected item. Spawns the items mesh and attaches the DroppedItem script.
    /// </summary>
    public void DropItem()
    {
        if (inventory.selectedItem == null)
        {
            return;
        }
        GameObject mesh = inventory.selectedItem.Mesh;
        if (mesh != null)
        {
            GameObject spawnedMesh = Instantiate(mesh, null);
            spawnedMesh.transform.position = dropPoint.position;

            
            DroppedItem droppedItem = mesh.GetComponent<DroppedItem>();
            if (droppedItem == null)
            {
                droppedItem = spawnedMesh.AddComponent<DroppedItem>();
            }
            if (droppedItem != null)
            {
                droppedItem.item = new Item(inventory.selectedItem, 1);
            }
        }

        inventory.selectedItem.Amount--; // Subtract from the item amount
        inventory.DisplaySelectedItemOnCanvas(inventory.selectedItem); // Update displayed item
        if (inventory.selectedItem.Amount <= 0) // If there is nothing left in the inventory
        {
            inventory.RemoveItem(inventory.selectedItem); //remove it from the inventory
            inventory.selectedItem = null; // make selected item null as there will still be a reference to it
            inventory.DisplaySelectedItemOnCanvas(inventory.selectedItem); //Update displayed item

        }
    }
}
