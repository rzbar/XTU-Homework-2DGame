using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics.AttackForm
{
    public class NormalAttack : AttackObject
    {
        private int dir;
        private void Start()
        {
            dir = model.player.left;
        }

        protected override void Handle()
        {
            transform.position += dir * new Vector3(0.1f, 0, 0);
            if(tick > 30)
            {
                Destroy(gameObject);
            }
        }

    }
}
