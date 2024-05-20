using Platformer.Core;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics.Skill
{
    [System.Serializable]
    public class SkillEmitter : MonoBehaviour
    {
        protected readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        public virtual void Emit()
        {
            //TODO
        }
    }
}

