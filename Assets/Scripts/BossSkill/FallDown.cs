using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    public class FallDown : SkillEmitter
    {
        public EnemyController enemy;
        public override void Emit()
        {
            var obj = Instantiate(model.attackScriptableObject.attackObjects[0],enemy.transform.position,Quaternion.identity,null);
            
            
        }

        IEnumerator Invincibility()
        {
            model.player.Invincibility++;
            yield return new WaitForSeconds(60 * Time.deltaTime);
            model.player.Invincibility--;
        }
    }
}

