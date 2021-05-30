using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUpItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private Camera cameraItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cameraItem.ViewportPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 50f))
            {
                DroppedItem droppedItem = hitInfo.collider.gameObject.GetComponent<DroppedItem>();
                if (droppedItem != null)
                {
                    inventory.AddItem(droppedItem.item);
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
    }


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
        if (inventory.selectedItem.Amount <= 0) // If there is nothing left in the inventory
        {
            inventory.RemoveItem(inventory.selectedItem); //remove it from the inventory
            inventory.selectedItem = null; // make selected item null as there will still be a reference to it
        }
    }
}
