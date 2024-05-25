using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.BPSystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
    public class Inventroy : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
    }
}

