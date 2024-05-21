using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originaParent;
    public Inventroy myBag;
    int currentItemID; // 当前物品ID

    private void Awake()
    {
        // 初始化时获取物品原始的父级位置
        originaParent = transform.parent;
        currentItemID = originaParent.GetComponent<Slot>().slotID;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 将物品的父级设置为Canvas，禁用射线检测
        transform.SetParent(originaParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //物品跟随鼠标拖拽移动
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            // 拖拽到另一个物品上
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
                SwapItemsWithItemSlot(eventData.pointerCurrentRaycast.gameObject);
                return;
            }

            // 拖拽到一个空槽位上
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                SwapItemsWithEmptySlot(eventData.pointerCurrentRaycast.gameObject);
                return;
            }
        }

        // 拖拽到非法区域，将物品放回原始父级位置
        transform.SetParent(originaParent);
        transform.position = originaParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void SwapItemsWithItemSlot(GameObject itemSlot)
    {
        int targetID = itemSlot.transform.parent.parent.GetComponent<Slot>().slotID;
        var temp = myBag.itemList[targetID];

        // 交换物品父级和位置
        transform.SetParent(itemSlot.transform.parent.parent);
        transform.position = itemSlot.transform.parent.parent.position;
        itemSlot.transform.parent.position = originaParent.position;

        // 更新物品列表中的位置
        myBag.itemList[currentItemID] = myBag.itemList[targetID];
        myBag.itemList[targetID] = temp;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void SwapItemsWithEmptySlot(GameObject emptySlot)
    {
        int targetID = emptySlot.GetComponentInParent<Slot>().slotID;

        // 更新物品列表中的位置
        myBag.itemList[targetID] = myBag.itemList[currentItemID];
        if (targetID != currentItemID)
        {
            myBag.itemList[currentItemID] = null;
        }

        // 设置物品的父级和位置
        transform.SetParent(emptySlot.transform);
        transform.position = emptySlot.transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}