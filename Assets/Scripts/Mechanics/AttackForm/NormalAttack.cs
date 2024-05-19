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
            print("Õ¶¿Õ²¨");
            dir = model.player.left;
        }
        protected override void Handle()
        {
            transform.localPosition += 0.1f*dir*Vector3.right;
            if(tick > 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
