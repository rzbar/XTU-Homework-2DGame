using Platformer.Core;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class AttackObject : MonoBehaviour
    {
        [SerializeField]
        protected int tick = 0;
        protected readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public float damage;
        public Collider2D[] attackAreas;

        public Dictionary<string, Collider2D> attackAreasDict = new Dictionary<string, Collider2D>();

        
        public bool onetime = false;
        protected virtual void Awake()
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
        protected virtual void Update()
        {
            tick++;
            Handle();
        }

        protected virtual void Handle()
        {

        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            print(1);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                print("‘Ï≥……À∫¶¿≤£°");
                if (onetime)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

