using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Slot
{
    public GameObject gameObject;
    public Item item;
    public Slot(GameObject _gameob, Item _item)
    {
        this.gameObject = _gameob;
        this.item = _item;
    }
}
public class InventoryScript : MonoBehaviour
{
    [Header("UI")]
    public int slotAmount = 9;

    [Header("Prefab")]
    public GameObject slotPrefab;
    //public int slotNo;
    public GameObject slotPanel;
    public GameObject itemPrefab;

    [Header("Items/Slots")]
    public List<Item> items = new List<Item>();
    public List<Slot> slots = new List<Slot>();


    private ItemDatabase itData;
    void Start()
    {
        itData = GetComponent<ItemDatabase>();

        for (int i = 0; i < slotAmount; i++)
        {
            GameObject clone = Instantiate(slotPrefab);
            clone.transform.SetParent(slotPanel.transform);
            Slot slot = new Slot(clone, null);

            slotData slotData = clone.GetComponent<slotData>();
            slotData.inventory = this;
            slotData.slot = slot;
            slots.Add(slot);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItem("Mono-blade", 1);
        }
    }
    public void AddItem(string itemName, int itemAmount = 1)
    {
        Item newItem = ItemDatabase.GetItem(itemName);
        Slot newSlot = GetEmptySlot();
        if (newItem != null && newSlot != null)
        {
            if(HasStacked(newItem,itemAmount) == true)
            {
                return;
            }
            newSlot.item = newItem;

            GameObject item = Instantiate(itemPrefab);
            item.transform.position = newSlot.gameObject.transform.position;
            item.transform.SetParent(newSlot.gameObject.transform);
            item.name = newItem.Title;

            newItem.gameobject = item;

            Image image = item.GetComponent<Image>();
            image.sprite = newItem.Sprite;

            ItemData itemdata = item.GetComponent<ItemData>();
            itemdata.item = newItem;
            itemdata.slot = newSlot;
        }
    }
    public Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }
    bool HasStacked(Item itemToAdd, int itemAmount)
    {
        if(itemToAdd.Stackable == true)
        {
            Slot occupied = GetSlotWithItem(itemToAdd);
            if (occupied != null)
            {
                Item item = occupied.item;
                ItemData itemData = item.gameobject.GetComponent<ItemData>();
                itemData.amount += itemAmount;
                Text textElement = item.gameobject.GetComponentInChildren<Text>();
                textElement.text = itemData.amount.ToString();
                return true;
            }
        }
        return false;
    }
    Slot GetSlotWithItem(Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Item curItem = slots[i].item;
            if (curItem != null && curItem.Title == item.Title)
            {
                return slots[i];
            }
        }
        return null;
    }
}
