using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics.AttackForm
{
    public class Hiraishinn : AttackObject
    {
        public float dir;
        private void Start()
        {
            transform.localEulerAngles = new Vector3(0, 0, dir);
        }
        protected override void Handle()
        {
            if (tick > 10)
            {
                Destroy(gameObject);
            }
        }
    }
}
