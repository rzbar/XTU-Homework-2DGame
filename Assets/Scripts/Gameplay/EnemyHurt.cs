using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class EnemyHurt : Simulation.Event<EnemyHurt>
    {
        public EnemyController enemy;
        public float hurtNum;
        Health health;
        public override void Execute()
        {
            Debug.Log("我被打力");
            health=enemy.GetComponent<Health>();
            health.Decrement(hurtNum);
        }
    }
}