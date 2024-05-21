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
    public GameObject waringArea;
    float timer = 0;
    protected override void Awake()
    {
        base.Awake();
        skillManager = GetComponent<SkillManager>();
    }
    protected override void Update()
    {
        timer += Time.deltaTime;
        print(transform.position.x);

        fromPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(fromPlayer > distance && timer>waitTime)
        {
            timer = 0;
            //StartCoroutine(FallDown());
            //transform.position = new Vector3();
        }
        base.Update();

    }

    //IEnumerator FallDown()
    //{
    //    control.animator.SetTrigger("Jump!");
    //    yield return new WaitForSeconds(2);
    //    this.transform.position=new Vector3(player.transform.position.x,8,transform.position.z);
    //    while (transform.position.y > player.transform.position.y)
    //    {
            
    //        transform.position=new Vector3(transform.position.x,transform.position.y-0.04f,transform.position.z);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, jumpSpeed * Time.deltaTime);
    //    timer = 0;
    //}
}
