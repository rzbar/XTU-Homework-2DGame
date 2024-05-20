using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Platformer.Mechanics
{
    public class BoomEnemy : FollowEnemy
    {
        public GameObject boomEffect;
        public float boomArea;
        protected override void Update()
        {
            base.Update();
            if (CanBoom())
            {
                var boom=Instantiate(boomEffect);
                boom.transform.position=this.transform.position;
                Destroy(boom, 2);
            }
        }
        internal bool CanBoom()
        {
            return Vector2.Distance(this.transform.position, player.transform.position) < boomArea;
        }
    }
}
