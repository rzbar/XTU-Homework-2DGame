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
using Platformer.Mechanics.BPSystem;

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
        public InventoryManager inventoryManager;

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
        
        public float invincibilityTime = 0.6f;


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
            inventoryManager = GetComponent<InventoryManager>();
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
                UpdateSkillList();
                //普攻槽位
                if (Input.GetKeyDown(KeyCode.J))
                {
                    var obj = Instantiate(attackScriptableObject.attackObjects[0],transform.position,Quaternion.identity,null);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    skillManager.emitters[0].EmitSkill();
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    skillManager.emitters[1].EmitSkill();
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    skillManager.emitters[2].EmitSkill();
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


        private void OnTriggerEnter2D(Collider2D collision)
        {
            AttackObject attact = null;
            foreach (var com in collision.gameObject.GetComponents<Component>())
            {
                foreach (var com1 in com.gameObject.GetComponentsInParent<Component>())
                {
                    if (com1 is AttackObject)
                    {
                        attact = (AttackObject)com1;
                    }
                }

            }
            if (attact != null && (attact.type & 1) == 1 && Invincibility==0)
            {
                var ev = Schedule<PlayerHurt>();
                ev.dmg = attact.damage;
                
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

        void UpdateSkillList()
        {
            skillManager.emitters[0].skillEmitter ??= inventoryManager.GetItem(1)?.skillInfo.skillEmitter;
            skillManager.emitters[1].skillEmitter ??= inventoryManager.GetItem(2)?.skillInfo.skillEmitter;
            skillManager.emitters[2].skillEmitter ??= inventoryManager.GetItem(3)?.skillInfo.skillEmitter;
        }

        public IEnumerator GiveInvincible(float time)
        {
            Invincibility++;
            yield return new WaitForSeconds(time);
            Invincibility--;
        }
    }
}