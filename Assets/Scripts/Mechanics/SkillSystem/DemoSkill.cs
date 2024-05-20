using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    public class DemoSkill : SkillEmitter
    {
        public override void Emit()
        {
            var obj = Instantiate(model.attackScriptableObject.attackObjects[0], model.player.transform.position, Quaternion.identity, null);
            StartCoroutine(Invincibility());
        }

        IEnumerator Invincibility()
        {
            model.player.Invincibility++;
            yield return new WaitForSeconds(60*Time.deltaTime);
            model.player.Invincibility--;
        }
    }
}

