using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.Item
{
    [RequireComponent(typeof(SkillEmitter))]
    public class Weapon : MonoBehaviour
    {
        private SkillEmitter skillEmitter;
        public string weaponName = "Weapon";
        void Start()
        {
            skillEmitter = GetComponent<SkillEmitter>();
        }

    }
}

