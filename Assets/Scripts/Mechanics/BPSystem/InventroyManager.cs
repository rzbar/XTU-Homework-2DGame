using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventroy myBag;
    public GameObject slotGrid;
    public GameObject emptySlot;
    public List<Item> slots = new List<Item>();
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    //public static void CreateNewItem(Item item)
    //{
    //    Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform);
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform,false);
    //    newItem.slotItem = item;
    //    newItem.slotImage.sprite = item.itemImage;
    //    newItem.slotNum.text = item.itemHeld.ToString();
    //}

}
