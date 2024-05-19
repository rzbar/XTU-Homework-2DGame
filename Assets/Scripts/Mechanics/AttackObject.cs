using Platformer.Core;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class AttackObject : MonoBehaviour
    {
        protected readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public float damage;
        public Collider2D[] attackAreas;

        public Dictionary<string, Collider2D> attackAreasDict = new Dictionary<string, Collider2D>();

        protected int tick = 0;
        private bool onetime = false;
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
            tick++;
            Handle();
        }

        public virtual void Handle()
        {

        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (onetime)
                {
                    Destroy(this);
                }
            }
            
        }
    }
}

