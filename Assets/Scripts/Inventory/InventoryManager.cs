using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class InventoryManager : MonoBehaviour
{
    public PlayerManager player;
    public static InventoryManager Instance;
    public List<Item> Items = new();

    public Transform itemContent;  // grid layout inventory content object
    public GameObject inventoryItem;  // prefab inventory item

    private void Awake()
    {
        Instance = this;

        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        if (player.data.items != null)
        {
            foreach (var item in player.data.items)
            {
                Items.Add(item);
            }
        }

    }

    public void ObjectPickedUp(Item item)
    {
        Items.Add(item);
        player.data.items.Add(item);
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform child in itemContent)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("ListItems function called");
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;    
        }
    }
}

