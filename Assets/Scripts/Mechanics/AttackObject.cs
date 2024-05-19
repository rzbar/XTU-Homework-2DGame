using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class AttackObject : MonoBehaviour
    {
        public float damage;
        public Collider2D[] attackAreas;

        public Dictionary<string, Collider2D> attackAreasDict = new Dictionary<string, Collider2D>();
        private void Awake()
        {
            attackAreas = GetComponentsInChildren<Collider2D>();
        }

        private void Start()
        {
            foreach(Collider2D area in attackAreas)
            {
                attackAreasDict.Add(area.name, area);
            }
            
        }
        public void Update()
        {

        }

        public virtual void Handle()
        {

        }
    }
}

