using JetBrains.Annotations;
using Platformer.Core;
using Platformer.Mechanics.Skill;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Mechanics.BPSystem
{
    public class Slot : MonoBehaviour
    {
        public int id;
        public Item slotItem;
        public bool locked = false;
        public Image cdBar;
        protected readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private void Start()
        {
            cdBar = transform.GetChild(1).GetChild(0).GetComponent<Image>();    
        }

        private void Update()
        {
            if (slotItem != null && id < 4 && id > 0)
            {
                float cd = model.player.skillManager.emitters[id - 1].cd;
                Vector3 v = cdBar.transform.localScale;
                float process = 0f;
                if (cd == 0f)
                {
                    process = 0f;
                }
                else
                {
                    process = model.player.skillManager.emitters[id - 1].currentCd / cd;
                }
                cdBar.transform.localScale = new Vector3(v.x, process, v.z);
            }
            
        }
    }

}
