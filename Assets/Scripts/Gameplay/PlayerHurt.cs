using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player was hurt.
    /// </summary>
    /// <typeparam name="PlayerHurt"></typeparam>
    public class PlayerHurt : Simulation.Event<PlayerHurt>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public float dmg = 1f;

        public override void Execute()
        {
            var player = model.player;
            if (player.health.IsAlive)
            {
                player.health.Decrement(dmg);
                //model.virtualCamera.m_Follow = null;
                //model.virtualCamera.m_LookAt = null;
                // player.collider.enabled = false;
                //player.controlEnabled = false;

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                //player.animator.SetBool("dead", true);
                //Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}