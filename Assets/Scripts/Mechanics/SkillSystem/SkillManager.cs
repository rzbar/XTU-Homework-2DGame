using Platformer.Mechanics.BPSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Mechanics.Skill
{
    public class SkillManager : MonoBehaviour
    {
        
        public List<SkillInfo> emitters = new List<SkillInfo>();
        private void Awake()
        {
            foreach(var emi in emitters)
            {
                if(emi.skillEmitter != null)
                    emi.skillEmitter.owner = gameObject;
            }
        }

        [System.Serializable]
        public class SkillInfo
        {
            public int id;
            public float cd;
            public float currentCd;
            public SkillEmitter skillEmitter;

            public void EmitSkill()
            {
                if (currentCd <= 0)
                {
                    if (skillEmitter != null)
                    {
                        Instantiate(skillEmitter);
                        currentCd = cd;
                    }
                }
            }
        }

        private void Update()
        {
            for(int i = 0; i< emitters.Count; i++)
            {
                SkillInfo info = emitters[i];
                info.currentCd = Mathf.Clamp(info.currentCd - Time.deltaTime, 0, info.cd);
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

