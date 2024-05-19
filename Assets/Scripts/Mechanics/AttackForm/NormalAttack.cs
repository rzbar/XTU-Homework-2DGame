using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics.AttackForm
{
    public class NormalAttack : AttackObject
    {
        private void Start()
        {
            print("Õ¶¿Õ²¨");
        }
        protected override void Update()
        {
            base.Update();
            Handle();
        }
        public override void Handle()
        {
            transform.position += 0.01f*Vector3.right;
            if(tick > 120)
            {
                Destroy(gameObject);
            }
        }
    }
}
