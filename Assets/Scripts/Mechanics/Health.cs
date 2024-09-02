using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public float maxHP = 1f;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => CurrentHP > 0;

        public float CurrentHP {
            get
            {
                return currentHP;
            }
            set
            {
                currentHP = value;
                if (hpBar != null)
                {
                    Vector3 v = hpBar.rectTransform.localScale;
                    hpBar.rectTransform.localScale = new Vector3((currentHP / maxHP), v.y, v.z);
                }
            }
        }

        private float currentHP;


        public Image hpBar;


        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment(float heal)
        {
            CurrentHP = Mathf.Clamp(CurrentHP + heal, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(float damage)
        {
            CurrentHP = Mathf.Clamp(CurrentHP - damage, 0, maxHP);
            if (CurrentHP == 0)
            {
                
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (CurrentHP > 0) Decrement(100f);
        }

        void Awake()
        {
            CurrentHP = maxHP;
        }
    }
}
