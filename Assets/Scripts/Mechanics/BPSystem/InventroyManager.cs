using Platformer.Core;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Mechanics.BPSystem
{
    public class InventoryManager : MonoBehaviour
    {
        private static InventoryManager instance;
        public Inventroy myBag;
        public GameObject slotGrid;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public static InventoryManager Instance => instance;
        private void Start()
        {
            instance = this;
            UpdateBagpack();
        }
        public Item GetItem(int id)
        {
            return myBag.itemList[id];
        }

        public void RemoveItem(int id)
        {
            var obj = slotGrid.transform.GetChild(id / 6).gameObject.transform.GetChild(id % 6).gameObject.transform.GetChild(0);
            obj.GetComponent<Image>().sprite = null;
            myBag.itemList[id] = null;
        }
        public Slot GetSlot(int id)
        {
            var obj = slotGrid.transform.GetChild(id / 6).gameObject.transform.GetChild(id % 6);
            if (obj != null)
            {
                print(obj.name);
                return obj.GetComponent<Slot>();
            }
            else return null;
        }

        private void UpdateItem(int id, Item what)
        {
            if(what == null)
            {
                return;
            }
            var obj = slotGrid.transform.GetChild(id / 6).gameObject.transform.GetChild(id % 6).gameObject.transform.GetChild(0);
            if(obj!= null)
            {
                obj.GetComponent<Image>().sprite = what.itemImage;
            }
        }

        public void AddItem(Item what)
        {
            int id = 0;
            for (int i = 0; i < slotGrid.transform.childCount; i++)
            {
                var obj = slotGrid.transform.GetChild(id / 6).gameObject.transform.GetChild(id % 6).gameObject.transform.GetChild(0);
                if (obj.GetComponent<Image>().sprite != null)
                {
                    continue;
                }
                id = i;
            }
            if (id < 24)
            {
                UpdateItem(id, what);
            }
        }

        void UpdateBagpack()
        {
            for (int i = 0; i < myBag.itemList.Count; i++)
            {
                UpdateItem(i, myBag.itemList[i]);
            }
        }
    }

}
