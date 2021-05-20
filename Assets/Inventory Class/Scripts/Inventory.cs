using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventory = new List<Item>();
    [SerializeField] private bool showIMGUIInventory = true;
    private Item selectedItem = null;

    #region Canvas Inventory
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject inventoryGameObject;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private GameObject filterContent;
    #endregion

    #region Display Inventory
    private Vector2 scrollposition;
    private string sortType = "All";
    #endregion


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryGameObject.SetActive(true);
            DisplayItemsCanvas();
        }
    }
    private void DisplayFilterCanvas()
    {
        //to do
    }
    private void ChangeFilter(string itemType)
    {
        //to do
    }
    void DestroyAllChildren(Transform parent)
    {
        //to do
    }

    private void DisplayItemsCanvas()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGo = Instantiate<Button>(buttonPrefab, inventoryContent.transform);
                Text buttonText = buttonGo.GetComponentInChildren<Text>();
                buttonGo.name = inventory[i].Name + " button";
                buttonText.text = inventory[i].Name;
            }
        }
    }
      

    
    private void OnGUI()
    {
        if (showIMGUIInventory)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
            itemTypes.Insert(0, "All");

            for (int i = 0; i < itemTypes.Count; i++)
            {
                if(GUI.Button(new Rect((Screen.width/ itemTypes.Count) * i,10,Screen.width / itemTypes.Count, 20), itemTypes[i]))
                {
                    sortType = itemTypes[i];
                }
            }
            Display();
            if(selectedItem != null)
            {
                DisplaySelectedItem();
            }
        }
    }
    

    private void DisplaySelectedItem()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 3, Screen.width / 5, Screen.height / 5), selectedItem.Icon);
        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5), Screen.width / 5, Screen.height / 15), selectedItem.Name);
        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3), Screen.width / 5, Screen.height / 5), selectedItem.Description + 
            "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
    }

    private void Display()
    {
        scrollposition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40), scrollposition, new Rect(0, 0, 0, inventory.Count * 30), false, true);
        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                if(GUI.Button(new Rect(30,0 +(count * 30), 200, 30), inventory[i].Name))
                {
                    selectedItem = inventory[i];
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }
    




    

    
}
