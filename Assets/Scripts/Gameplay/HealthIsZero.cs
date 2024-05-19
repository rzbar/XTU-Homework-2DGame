using Platformer.Core;
using Platformer.Mechanics;
using System.Diagnostics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player health reaches 0. This usually would result in a 
    /// PlayerDeath event.
    /// </summary>
    /// <typeparam name="HealthIsZero"></typeparam>
    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {
        public Health health;
        public EnemyController enemy;

        public override void Execute()
        {
            enemy=health.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Schedule<EnemyDeath>().enemy=enemy;

            }
                
            else
                Schedule<PlayerDeath>();
            
        }
    }
}