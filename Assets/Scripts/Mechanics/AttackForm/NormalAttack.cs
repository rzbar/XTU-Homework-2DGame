using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics.AttackForm
{
    public class NormalAttack : AttackObject
    {
        private int dir;
        private void Start()
        {
            print("ն�ղ�");
            dir = model.player.left;
        }
        
    }
}
