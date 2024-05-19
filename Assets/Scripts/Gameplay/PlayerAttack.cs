using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player attacked.
    /// </summary>
    /// <typeparam name="PlayerAttack"></typeparam>
    public class PlayerAttack : Simulation.Event<PlayerAttack>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
        }
    }
}