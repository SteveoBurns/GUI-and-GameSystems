using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventory = new List<Item>();
    [SerializeField] private bool showIMGUIInventory = true;
    [NonSerialized] public Item selectedItem = null; // wont show in the inspector

    #region Canvas Inventory
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject inventoryGameObject;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private GameObject filterContent;

    [Header("Selected Item Display")]
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;

    #endregion

    #region Display Inventory
    private Vector2 scrollposition;
    private string sortType = "All";
    #endregion


    private void Start()
    {
        DisplayFilterCanvas();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryGameObject.activeSelf)
            {
                inventoryGameObject.SetActive(false);
            }
            else
            {
                inventoryGameObject.SetActive(true);
                DisplayItemsCanvas();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void DisplayFilterCanvas()
    {
        List<string> itemType = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
        itemType.Insert(0, "All");

        for (int i = 0; i < itemType.Count; i++)
        {
            Button buttonGo = Instantiate<Button>(buttonPrefab, filterContent.transform);
            Text buttonText = buttonGo.GetComponentInChildren<Text>();
            buttonGo.name = itemType[i] + " filter";
            buttonText.text = itemType[i];

            int x = i;
            buttonGo.onClick.AddListener(() => { sortType = itemType[x]; });
            buttonGo.onClick.AddListener(delegate { ChangeFilter(itemType[x]); });

        }
    }
    private void ChangeFilter(string itemType)
    {
        sortType = itemType;
        DisplayItemsCanvas();
    }
    void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddItem(Item _item)
    {
        AddItem(_item, _item.Amount);
    }

    public void AddItem(Item _item, int count)
    {
        Item foundItem = inventory.Find((x) => x.Name == _item.Name);

        if (foundItem == null)
        {
            inventory.Add(_item);
        }
        else
        {
            foundItem.Amount += count;
        }
        DisplayItemsCanvas();
        DisplaySelectedItemOnCanvas(selectedItem);

    }

    public void RemoveItem(Item _item)
    {
        if (inventory.Contains(_item))
            inventory.Remove(_item);
        DisplayItemsCanvas();
        DisplaySelectedItemOnCanvas(selectedItem);
    }

    private void DisplayItemsCanvas()
    {
        DestroyAllChildren(inventoryContent.transform);
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGo = Instantiate<Button>(buttonPrefab, inventoryContent.transform);
                Text buttonText = buttonGo.GetComponentInChildren<Text>();
                buttonGo.name = inventory[i].Name + " button";
                buttonText.text = inventory[i].Name;

                Item item = inventory[i];                
                buttonGo.onClick.AddListener(delegate { DisplaySelectedItemOnCanvas(item); });
            }
        }
    }
      
    void DisplaySelectedItemOnCanvas(Item _item)
    {
        selectedItem = _item;

        if (_item == null)
        {
            itemImage.texture = null;
            itemName.text = "";
            itemDescription.text = "";
        }
        else
        {
            itemImage.texture = selectedItem.Icon;
            itemName.text = selectedItem.Name;
            itemDescription.text =$" {selectedItem.Description} \n cost: {selectedItem.Value} \n amount: {selectedItem.Amount} ";

        }

        
    }
    #region On GUI
    
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
                    selectedItem.OnClicked();
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }
    #endregion







}
