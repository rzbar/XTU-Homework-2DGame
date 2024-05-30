using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Mechanics.BPSystem
{
    public class Slot : MonoBehaviour
    {
        public Item slotItem;
        public bool locked = false;
    }

}
