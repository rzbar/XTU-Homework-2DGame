using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
namespace Platformer.Mechanics
{
    public class FollowEnemy : EnemyController
    {
        #region ×´Ì¬ÅÐ¶¨
        public float distance;
        public PlayerController player;
        public float followSpeed;
        #endregion
        protected override void Awake()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            base.Awake();
        }
        protected override void Update()
        {
            if (isFindPlayer())
            {
                control.move.x = Mathf.Clamp(player.transform.localPosition.x - transform.localPosition.x, -followSpeed, followSpeed);
            }
            else
            {
                if (path != null)
                {
                    mover = path.CreateMover(control.maxSpeed * 0.5f);
                    control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
                }
            }
        }

        internal bool isFindPlayer()
        {
            return Vector2.Distance(this.transform.position, player.transform.position) < distance;
        }
    }
}
