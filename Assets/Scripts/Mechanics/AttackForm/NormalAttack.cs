using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics.AttackForm
{
    public class NormalAttack : AttackObject
    {
        public override void Handle()
        {
            transform.position += Vector3.right;
            if(tick > 120)
            {
                Destroy(this);
            }
        }
    }

}
