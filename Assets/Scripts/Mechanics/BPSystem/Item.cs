using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.Mechanics.BPSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite itemImage;
        [TextArea]
        public string itemInfo;

        public SkillInfo skillInfo;

        [System.Serializable]
        public class SkillInfo
        {
            public float cd;
            public SkillEmitter skillEmitter;
        }
    }
}

