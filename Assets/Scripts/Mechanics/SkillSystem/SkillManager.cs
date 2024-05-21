using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    public class SkillManager : MonoBehaviour
    {
        public List<SkillInfo> emitters = new List<SkillInfo>();

        [System.Serializable]
        public class SkillInfo
        {
            public int id;
            public string name;
            public float cd;
            public float currentCd;
            public SkillEmitter emitter;

            public void EmitSkill()
            {
                if (currentCd <= 0)
                {
                    if (emitter != null)
                    {
                        currentCd = cd;
                        emitter.Emit();
                    }
                }
            }
        }

        private void Update()
        {
            foreach (SkillInfo info in emitters)
            {
                info.currentCd = Mathf.Clamp(info.currentCd - Time.deltaTime,0,info.cd);
            }
        }

        public void RenewCD(int id)
        {
            emitters[id].currentCd = emitters[id].cd;
        }

        public void ClearCD(int id)
        {
            emitters[id].currentCd = 0;
        }

        public void SetSkill(int where,SkillInfo what)
        {

        }
    }

    
}

