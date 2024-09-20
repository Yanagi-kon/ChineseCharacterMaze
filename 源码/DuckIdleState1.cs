using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckIdle1State1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;

    public DuckIdle1State1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("idle1");
    }

    public void OnUpdate()
    {
        if (parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }

        timer += Time.deltaTime;
        if(timer>=parameter.idleTime)
        {
            manager.TransitionState(StateType.Idle2);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}


public class DuckIdle2State1 : Duck
{
    private AnimatorStateInfo info;
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    public DuckIdle2State1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("idle2");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime>=.95f)
        {
            manager.TransitionState(StateType.Idle1);
        }

        if (parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }

        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            manager.TransitionState(StateType.Walk);
            timer = 0;
        }
    }

    public void OnExit()
    {

    }
}


public class DuckWalkState1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    private float timer1;

    private int patroPosition;
    public DuckWalkState1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("walk");
        //float direction = (float)Random.Range(0f, 360f);//在0--360之间随机生成一个单精度小数)
        //manager.transform.rotation = Quaternion.Euler(0, direction, 0);//旋转指定度数
    }

    public void OnUpdate()
    {

        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);

        if(parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }
        if (timer1 >= 3.0f)
        {
            float direction = (float)Random.Range(0f, 360f);//在0--360之间随机生成一个单精度小数)
            //manager.transform.rotation = Quaternion.Euler(0, direction, 0);//旋转指定度数
            manager.transform.Rotate(0, direction, 0);

            timer1 = 0;
        }

        //如果发生了碰撞
        if(parameter.othercolli)
        {
           // Debug.Log("转啦啦啦啦啦啦啦啦绿绿绿绿绿绿绿绿绿");
            manager.transform.Rotate(0, 20, 0);
            //parameter.othercolli = false;
        }

        if(timer>=6.0f)
        {
            manager.TransitionState(StateType.Idle1);
        }
        timer += Time.deltaTime;
        timer1 += Time.deltaTime;
        //timer += Time.deltaTime;
        //if (timer >= parameter.idleTime)
        //{
        //    manager.TransitionState(StateType.FloorToFly);
        //}
    }
    public void OnExit()
    {
        timer = 0;
        timer1 = 0;
    }
}


public class DuckFloorToFlyState1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    private AnimatorStateInfo info;
    public DuckFloorToFlyState1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("floorGoToFly");        
    }

    public void OnUpdate()
    {
        manager.parameter.rigid.AddForce(0, 4, 0);
        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);
        manager.transform.Translate(Vector3.up * parameter.flySpeed * Time.deltaTime);

        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(StateType.Fly);
        }
    }

    public void OnExit()
    {
    }
}


public class DuckFlyState1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    private float Flytimer;
    private float speakTimer = 0;
    public DuckFlyState1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        manager.parameter.rigid.useGravity = false;
        parameter.animator.Play("fly");       
    }

    public void OnUpdate()
    {
        speakTimer += Time.deltaTime;
        float randomspeak = Random.Range(1, 2);
        if(speakTimer>randomspeak && speakTimer<randomspeak+0.03 && !manager.duckAudio.isPlaying)
        {
            
            manager.duckAudio.PlayOneShot(manager.duckSpeak);
        }
        if (timer >= 3.0f)
        {
            float direction = (float)Random.Range(0f, 360f);//在0--360之间随机生成一个单精度小数)
            manager.transform.rotation = Quaternion.Euler(0, direction, 0);//旋转指定度数
            timer = 0;
        }
        Flytimer += Time.deltaTime;
        timer += Time.deltaTime;
        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);
        //manager.transform.Translate(Vector3.Lerp(manager.transform.forward*parameter.moveSpeed*Time.deltaTime,parameter.rigid.velocity.normalized,0.1f));
        manager.transform.Translate(Vector3.up * parameter.flySpeed*2 * Time.deltaTime);



        if (Flytimer>=5.0f)
        {
            manager.TransitionState(StateType.Load);
            Flytimer = 0;
            speakTimer = 0;
        }
    }
    public void OnExit()
    {
        Flytimer = 0;
    }
}

public class DuckLoadState1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    public DuckLoadState1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.target = null;
        parameter.animator.Play("fly");
        manager.parameter.rigid.useGravity = true;
    }

    public void OnUpdate()
    {
        Debug.Log("正在降落");
        if(parameter.target)
        {
            manager.TransitionState(StateType.Fly);
            parameter.target = null;
        }
        manager.parameter.rigid.AddForce(0, 0, 0);
        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);
        manager.transform.Translate(Vector3.down * parameter.flySpeed *2 * Time.deltaTime);
        if (parameter.collision)
        {
            manager.TransitionState(StateType.Idle1);
        }
    }
    public void OnExit()
    {
        
    }
}

public class DuckWaterIdle1State1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    public DuckWaterIdle1State1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("idleWater1");
    }

    public void OnUpdate()
    {
        if (parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }

        timer += Time.deltaTime;
        if (timer >= 2.0f)
        {
            manager.TransitionState(StateType.WaterIdle2);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class DuckWaterIdle2State1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;

    public DuckWaterIdle2State1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("idleWater2");
    }

    public void OnUpdate()
    {
        if (parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }

        timer += Time.deltaTime;

        if (timer>=3.0f)
        {
            manager.TransitionState(StateType.WaterIdle3);
            timer = 0;
        }
    }

    public void OnExit()
    {
    }
}


public class DuckSwimState1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    private float timer1;
    public DuckSwimState1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("swim");
    }

    public void OnUpdate()
    {
        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);

        if (parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }
        if (timer1 >= 3.0f)
        {
            float direction = (float)Random.Range(0f, 360f);//在0--360之间随机生成一个单精度小数)
            manager.transform.rotation = Quaternion.Euler(0, direction, 0);//旋转指定度数

            timer1 = 0;
        }
        if(timer>=6.0f)
        {
            manager.TransitionState(StateType.WaterIdle1);
        }
        timer += Time.deltaTime;
        timer1 += Time.deltaTime;
    }

    public void OnExit()
    {
        timer = 0;
        timer1 = 0;
    }
}

public class DuckWaterIdle3State1 : Duck
{
    private FSM1 manager;
    private Parameter parameter;
    private float timer;
    public DuckWaterIdle3State1(FSM1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("idleWater3");
    }

    public void OnUpdate()
    {
        if (parameter.target)
        {
            manager.TransitionState(StateType.FloorToFly);
            parameter.target = null;
        }

        timer += Time.deltaTime;
        if (timer >= 3.0f)
        {
            manager.TransitionState(StateType.Swim);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}