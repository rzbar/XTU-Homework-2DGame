using Platformer.Mechanics;
using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SkillManager))]
public class Boss0 : FollowEnemy
{
    public float waitTime;
    public SkillManager skillManager;

    protected override void Awake()
    {
        base.Awake();
        skillManager = GetComponent<SkillManager>();
    }
}
