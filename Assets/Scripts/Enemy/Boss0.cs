using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Mechanics.Skill;
using System.Collections;
using System.Collections.Generic;
using static Platformer.Core.Simulation;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(SkillManager))]

public class Boss0 : FollowEnemy
{
    public float waitTime;
    private float fromPlayer;
    public SkillManager skillManager;
    float timer = 0;
    public GameObject bossUI;
    protected override void Awake()
    {
        base.Awake();
        skillManager = GetComponent<SkillManager>();
        isStop = true;
    }
    protected override void Update()
    {
        if (player.transform.position.x > path.transform.position.x + path.startPosition.x && player.transform.position.x < path.transform.position.x + path.endPosition.x)
        {
            if (player.transform.position.y < path.transform.position.y + path.startPosition.y + 10 + 1 && player.transform.position.y > path.transform.position.y + path.startPosition.y - 1)
            {
                isStop = false;
            }
        }
        else
        {
            isStop = true;
        }
        if (!isStop)
        {
            bossUI.SetActive(true);
            timer += Time.deltaTime;
            fromPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (timer > waitTime)
            {
                if (fromPlayer > distance)
                {
                    RandomSkill(new int[1] { 1 });
                    timer = 0;
                }
                else if (fromPlayer < 4)
                {
                    RandomSkill(new int[2] { 0, 1 });
                    timer = 0;
                }
            }
        }
        else
        {
            bossUI.SetActive(!isStop);
        }
    }

    private void RandomSkill(int[] seed)
    {
        int total = seed.Sum();
        int randint = Random.Range(0, total - 1);
        for (int i = 1; i < seed.Length; i++)
        {
            seed[i] += seed[i - 1];
        }
        for (int i = 0; i < seed.Length; i++)
        {
            if (randint < seed[i])
            {

                skillManager.emitters[i].EmitSkill();
                break;
            }
        }

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            var ev = Schedule<PlayerEnemyCollision>();
            ev.player = player;
            ev.enemy = this;
        }
    }

}