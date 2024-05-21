using Platformer.Mechanics.AttackForm;
using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    public class HiraishinnSkill : SkillEmitter
    {
        public override void Emit()
        {
            StartCoroutine(Hiraishinn());
        }
        IEnumerator Hiraishinn()
        {
            model.player.Invincibility++;
            for (int i = 0; i < 5; i++)
            {
                var obj = Instantiate(model.attackScriptableObject.attackObjects[1], model.player.transform.position, Quaternion.identity, null) as Hiraishinn;
                obj.dir = 144 * i;
                if (model.player.left == -1) obj.dir = 180 - obj.dir;
                model.player.Teleport(model.player.transform.position + new Vector3(3.3f * Mathf.Cos(obj.dir * Mathf.Deg2Rad), 3.3f * Mathf.Sin(obj.dir * Mathf.Deg2Rad), 0));
                yield return new WaitForSeconds(0.05f);
            }
            model.player.Invincibility--;
        }

    }
}

