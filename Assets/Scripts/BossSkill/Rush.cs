using Platformer.Gameplay;
using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    public class Rush : SkillEmitter
    {
        private Boss0 enemy;
        private PlayerController player;

        public GameObject rushAtea;
        public float rushDis;
        public GameObject rushEffect;

        private float leftBound;
        private float rightBound;
        public override void Emit()
        {
            enemy = owner.GetComponent<Boss0>();
            player = model.player;
            leftBound = enemy.path.transform.position.x + enemy.path.startPosition.x + 1.2f;
            rightBound = enemy.path.transform.position.x + enemy.path.endPosition.x - 1.2f;
            StartCoroutine(RushSkill());
            
        }

        

        IEnumerator RushSkill()
        {
            var item = Instantiate(rushAtea);
            item.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y - 1.2f);
            Vector2 forword = player.transform.position - enemy.transform.position;
            var warn = item.transform.GetChild(0);
            var eff= Instantiate(rushEffect);
            eff.transform.position = enemy.transform.position;
            if (forword.x < 0)
            {
                float time = 1f;
                while (time > 0)
                {
                    warn.transform.localScale = new Vector3(1 - time, 1 - time, 1);
                    time -= 0.02f;
                    yield return null;
                }
                Destroy(eff);
                while (enemy.transform.position.x > Mathf.Clamp(item.transform.position.x - rushDis, leftBound, rightBound))
                {
                    enemy.control.move.x = -2;
                    yield return null;
                }
            }
            else
            {
                float time = 1f;
                item.transform.localScale = -item.transform.localScale;
                while (time > 0)
                {
                    warn.transform.localScale = new Vector3(1 - time, 1 - time, 1);
                    time -= 0.02f;
                    yield return null;
                }
                Destroy(eff);
                while (enemy.transform.position.x < Mathf.Clamp(item.transform.position.x + rushDis, leftBound, rightBound))
                {
                    enemy.control.move.x = 2;
                    yield return null;
                }
            }
            enemy.control.move.x = 0;
            Destroy(item);
            finished = true;
        }


    }
}

