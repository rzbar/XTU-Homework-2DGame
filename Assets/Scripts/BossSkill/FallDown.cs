using Platformer.Gameplay;
using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    public class FallDown : SkillEmitter
    {
        private Boss0 enemy;
        private PlayerController player;

        public GameObject waringArea;
        private float nowHeight;
        public float jumpHeight;
        public float jumpSpeed;

        private float leftBound;
        private float rightBound;
        public override void Emit()
        {
            enemy = owner.GetComponent<Boss0>();
            player=model.player;
            leftBound = enemy.path.transform.position.x + enemy.path.startPosition.x + 1.2f;
            rightBound = enemy.path.transform.position.x + enemy.path.endPosition.x - 1.2f;
            nowHeight = enemy.transform.position.y;
            jumpHeight += enemy.transform.position.y;
            StartCoroutine(FallDownSkill());
        }

        IEnumerator FallDownSkill()
        {
            while (enemy.transform.position.y < jumpHeight)
            {
                enemy.control.velocity.y = jumpSpeed;
                yield return new WaitForEndOfFrame();
            }
            enemy.transform.position = new Vector2(Mathf.Clamp(player.transform.position.x, leftBound, rightBound), jumpHeight);
            var attack = Instantiate(waringArea);
            attack.transform.position = new Vector2(enemy.transform.position.x, nowHeight - 1);
            var attackArea = attack.transform.GetChild(0);
            while (enemy.transform.position.y > nowHeight)
            {
                enemy.control.velocity.y = -jumpSpeed;
                float dis = enemy.transform.position.y - 0.1f;
                attackArea.transform.localScale = new Vector3(1 - dis / 20, 1 - dis / 20, 1);
                yield return new WaitForEndOfFrame();
            }
            Destroy(attack);
        }

        
    }
}

