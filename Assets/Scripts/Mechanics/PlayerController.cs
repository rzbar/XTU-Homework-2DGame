using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Platformer.Mechanics.AttackForm;
using UnityEditor;
using Platformer.Mechanics.Skill;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
       
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public AttackScriptableObject attackScriptableObject;
        public SkillManager skillManager;
        public bool canHurt;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        public FightState fightState = FightState.Normal;
        private bool stopJump => jumpedTimes == maxJumpTime;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump = false;
        bool stopForce = true;
        private int jumpedTimes = 0;
        private int maxJumpTime = 2;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        [SerializeField]
        private int invincibility = 0;


        public Bounds Bounds => collider2d.bounds;
        public int left => (spriteRenderer.flipX?-1:1);

        public int Invincibility { get => invincibility; set => invincibility = value; }

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            skillManager = GetComponent<SkillManager>();
        }

        protected override void Update()
        {
            UpdataRenderer();
            if (controlEnabled)
            {
                #region move
                move.x = Input.GetAxis("Horizontal");

                if (Input.GetButtonUp("Jump"))
                {
                    stopForce = true;
                }
                #endregion move

                #region fight
                //普攻槽位
                if (Input.GetKeyDown(KeyCode.J))
                {
                    var obj = Instantiate(attackScriptableObject.attackObjects[0],transform.position,Quaternion.identity,null);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    if (skillManager.emitters[0].emitter != null && skillManager.emitters[0].currentCd <= 0)
                    {
                        skillManager.emitters[0].emitter.Emit();
                        skillManager.RenewCD(0);
                    }
                    
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (skillManager.emitters[1].emitter != null && skillManager.emitters[1].currentCd <= 0)
                    {
                        skillManager.emitters[1].emitter.Emit();
                        skillManager.RenewCD(1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (skillManager.emitters[2].emitter != null && skillManager.emitters[2].currentCd <= 0)
                    {
                        skillManager.emitters[2].emitter.Emit();
                        skillManager.RenewCD(2);
                    }
                }
                #endregion fight
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    jumpedTimes++;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (!IsGrounded && Input.GetButtonDown("Jump") && !stopJump)
                    {
                        jumpedTimes++;
                        jump = true;
                        stopForce = false;
                    }
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    jumpedTimes = 0;
                    break;
                case JumpState.Grounded:
                    if (!IsGrounded)
                    {
                        jumpState = JumpState.InFlight;
                    }
                    if (Input.GetButtonDown("Jump") && IsGrounded)
                    {
                        jumpState = JumpState.PrepareToJump;
                        stopForce = false;
                    }
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopForce)
            {
                if (velocity.y > 0)
                {
                    
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }
            //else if (stopJump)
            //{
            //    if (velocity.y > 0)
            //    {
            //        velocity.y = velocity.y * model.jumpDeceleration;
            //    }
            //}

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        public enum FightState
        {
            Hurt,
            Normal,
            Attack,
        }

        IEnumerator Hiraishinn()
        {
            for (int i = 0; i < 5; i++)
            {
                var obj = Instantiate(attackScriptableObject.attackObjects[1], transform.position, Quaternion.identity, null) as Hiraishinn;
                obj.dir = 144 * i;
                Teleport(transform.position + new Vector3(3.3f * Mathf.Cos(obj.dir * Mathf.Deg2Rad),3.3f *Mathf.Sin(obj.dir * Mathf.Deg2Rad) ,0));
                yield return new WaitForSeconds(0.05f);
            }
        }

        void UpdataRenderer()
        {
            if(invincibility > 0)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 1f);
            }
        }
    }
}