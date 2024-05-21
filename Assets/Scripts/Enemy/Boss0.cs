using Platformer.Mechanics;
using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SkillManager))]
public class Boss0 : FollowEnemy
{
    public float waitTime;
    private float fromPlayer;
    public float jumpHeight;
    public float jumpSpeed;
    public SkillManager skillManager;

    protected override void Awake()
    {
        base.Awake();
        skillManager = GetComponent<SkillManager>();
    }
    protected override void Update()
    {
        float timer = 0;
        timer += Time.deltaTime;
        fromPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if(fromPlayer > distance && timer>waitTime)
        {
            this.transform.position = player.transform.position;
            Vector3 myTarget = new Vector3(player.transform.position.x, jumpHeight, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, myTarget, jumpSpeed * Time.deltaTime);
            if(player.transform.position.x == this.transform.position.x)
            {
                Vector3 targetPos = new Vector3(player.transform.position.x, jumpHeight, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, jumpSpeed * Time.deltaTime);

            }
        }


        base.Update();
    }
    
}
