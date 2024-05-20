using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics.AttackForm
{
    public class BoomAttack : AttackObject
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.Decrement(3);
            }
        }

    }
}
