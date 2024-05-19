using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class EnemyHurt : Simulation.Event<EnemyHurt>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            
        }
    }
}