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
    private float nowHeight;
    public SkillManager skillManager;
    public GameObject waringArea;
    public GameObject rushAtea;
    public float rushDis;
    float timer = 0;
    public GameObject bossUI;
    private float leftBound;
    private float rightBound;
    public GameObject rushEffect;
    protected override void Awake()
    {
        base.Awake();
        skillManager = GetComponent<SkillManager>();
        rushEffect.SetActive(false);
        jumpHeight += transform.position.y;
        nowHeight = transform.position.y;
        isStop = true;
        leftBound =path.transform.position.x + path.startPosition.x + 1.2f;
        rightBound = path.transform.position.x + path.endPosition.x - 1.2f;
    }
    protected override void Update()
    {
        if (player.transform.position.x > path.transform.position.x + path.startPosition.x && player.transform.position.x < path.transform.position.x + path.endPosition.x)
        {
            if (player.transform.position.y < path.transform.position.y + path.startPosition.y + jumpHeight + 1 && player.transform.position.y > path.transform.position.y + path.startPosition.y - 1)
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
                    timer = 0;
                    StartCoroutine(FallDown());
                }
                else if (fromPlayer < 4)
                {
                    StartCoroutine(Rush());
                    timer = 0;
                }
            }
        }
        else
        {
            bossUI.SetActive(!isStop);
        }
    }

    IEnumerator FallDown()
    {
        while (transform.position.y <jumpHeight)
        {

            control.velocity.y = jumpSpeed;
            //transform.position = new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        this.transform.position = new Vector2(Mathf.Clamp(player.transform.position.x,leftBound,rightBound), jumpHeight);
        var attack = Instantiate(waringArea);
        attack.transform.position = new Vector2(transform.position.x, nowHeight-1);
        var attackArea = attack.transform.GetChild(0);
        while (transform.position.y > nowHeight)
        {
            //transform.position = new Vector2(transform.position.x, transform.position.y - 0.2f);
            control.velocity.y = -jumpSpeed;
            float dis=transform.position.y- 0.1f;
            attackArea.transform.localScale = new Vector3(1 - dis / 20, 1 - dis / 20, 1);
            yield return new WaitForEndOfFrame();
        }
        Destroy(attack);
        timer = 0;
    }

    IEnumerator Rush()
    {
        var item = Instantiate(rushAtea);
        item.transform.position=new Vector2(transform.position.x,transform.position.y-1); 
        Vector2 forword = player.transform.position - transform.position;
        var warn = item.transform.GetChild(0);
        rushEffect.SetActive(true);
        if (forword.x < 0)
        {
            float time = 1f;
            while(time > 0)
            {
                warn.transform.localScale = new Vector3(1 - time, 1 - time, 1);
                time -= 0.02f;
                yield return null;
            }
            rushEffect.SetActive(false);
            while(transform.position.x> Mathf.Clamp(item.transform.position.x - rushDis,leftBound,rightBound))
            {
                control.move.x = -3;
                yield return null;
            }
        }
        else
        {
            float time = 1f;
            item.transform.localScale = -item.transform.localScale;
            while (time > 0)
            {
                warn.transform.localScale = new Vector3(1 - time, 1 - time, 1);
                time -= 0.02f;
                yield return null;
            }
            rushEffect.SetActive(false);
            while (transform.position.x < Mathf.Clamp(item.transform.position.x + rushDis, leftBound,rightBound))
            {
                control.move.x = 3;
                yield return null;
            }
        }
        control.move.x = 0;
        Destroy(item);
        timer = 0;
    }
}
