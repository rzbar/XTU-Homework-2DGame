using JetBrains.Annotations;
using Platformer.Core;
using Platformer.Model;
using System;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public GameObject boss0;
        public bool boss0Fight;

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void OnEnable()
        {
            Instance = this;
            boss0.SetActive(false);
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
            if (boss0Fight)
            {
                EnterBoss0();
            }
            else
            {
                OutBoss0();
            }
        }

        private void OutBoss0()
        {
            if (boss0.activeSelf)
            {
                boss0.SetActive(false);
            }
        }

        private void EnterBoss0()
        {
            if (!boss0.activeSelf)
            {
                boss0.SetActive(true);
            }

        }
    }
}