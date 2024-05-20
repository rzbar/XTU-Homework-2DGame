using Platformer.Mechanics.AttackForm;
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
        private bool isBoomed;

        protected override void Awake()
        {
            base.Awake();
            isBoomed = false;
        }
        protected override void Update()
        {
            base.Update();
            if (CanBoom()&&!isBoomed)
            {
                isBoomed = true;
                StartCoroutine(Boom());
            }
            
        }

        protected override void OnCollisionStay2D(Collision2D collision)
        {
            
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<BoomAttack>()!=null)
            {
                Health health = transform.GetComponent<Health>();
                health.Decrement(1f);
            }
        }

        IEnumerator Boom()
        {
            spriteRenderer= this.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1, 1, 1, 1);
            float col = 1;
            while (col > 0)
            {
                col -= 0.1f;
                spriteRenderer.color=new Color(col, col, col, 1);
                yield return new WaitForSeconds(0.2f);
            }
            var boom = Instantiate(boomEffect);
            boom.transform.position = this.transform.position;
            Destroy(boom, 2);
            
        }
        internal bool CanBoom()
        {
            return Vector2.Distance(this.transform.position, player.transform.position) < boomArea;
        }
    }
}
