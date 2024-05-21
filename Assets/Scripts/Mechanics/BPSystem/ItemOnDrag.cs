using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originaParent;
    public Inventroy myBag;
    int currentItemID; // ��ǰ��ƷID

    private void Awake()
    {
        // ��ʼ��ʱ��ȡ��Ʒԭʼ�ĸ���λ��
        originaParent = transform.parent;
        currentItemID = originaParent.GetComponent<Slot>().slotID;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ����Ʒ�ĸ�������ΪCanvas���������߼��
        transform.SetParent(originaParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //��Ʒ���������ק�ƶ�
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            // ��ק����һ����Ʒ��
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
                SwapItemsWithItemSlot(eventData.pointerCurrentRaycast.gameObject);
                return;
            }

            // ��ק��һ���ղ�λ��
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                SwapItemsWithEmptySlot(eventData.pointerCurrentRaycast.gameObject);
                return;
            }
        }

        // ��ק���Ƿ����򣬽���Ʒ�Ż�ԭʼ����λ��
        transform.SetParent(originaParent);
        transform.position = originaParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void SwapItemsWithItemSlot(GameObject itemSlot)
    {
        int targetID = itemSlot.transform.parent.parent.GetComponent<Slot>().slotID;
        var temp = myBag.itemList[targetID];

        // ������Ʒ������λ��
        transform.SetParent(itemSlot.transform.parent.parent);
        transform.position = itemSlot.transform.parent.parent.position;
        itemSlot.transform.parent.position = originaParent.position;

        // ������Ʒ�б��е�λ��
        myBag.itemList[currentItemID] = myBag.itemList[targetID];
        myBag.itemList[targetID] = temp;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void SwapItemsWithEmptySlot(GameObject emptySlot)
    {
        int targetID = emptySlot.GetComponentInParent<Slot>().slotID;

        // ������Ʒ�б��е�λ��
        myBag.itemList[targetID] = myBag.itemList[currentItemID];
        if (targetID != currentItemID)
        {
            myBag.itemList[currentItemID] = null;
        }

        // ������Ʒ�ĸ�����λ��
        transform.SetParent(emptySlot.transform);
        transform.position = emptySlot.transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}