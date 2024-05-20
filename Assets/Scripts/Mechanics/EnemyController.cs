using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        internal SpriteRenderer spriteRenderer;
        internal Rigidbody2D body;

        public Bounds Bounds => _collider.bounds;

        protected virtual void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            body = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {

            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
            
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            AttackObject attact = null;
            foreach(var com in collision.gameObject.GetComponents<Component>())
            {
                foreach(var com1 in com.gameObject.GetComponentsInParent<Component>())
                {
                    if(com1 is AttackObject)
                    {
                        attact = (AttackObject)com1;
                    }
                }
                
            }
            if (attact != null&&((attact.type>>1)&1)==1)
            {
                var ev = Schedule<EnemyHurt>();
                ev.enemy = this;
                ev.hurtNum = attact.damage;
            }
        }

        protected virtual void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}