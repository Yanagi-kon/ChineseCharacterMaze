using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckIdle1State : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;

    public DuckIdle1State(FSM manager)
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
        if (parameter.Water)
            manager.TransitionState(StateType.WaterIdle1);

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


public class DuckIdle2State : Duck
{
    private AnimatorStateInfo info;
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public DuckIdle2State(FSM manager)
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
        if (parameter.Water)
            manager.TransitionState(StateType.WaterIdle1);

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


public class DuckWalkState : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    private float timer1;

    private int patroPosition;
    public DuckWalkState(FSM manager)
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
        if (parameter.Water)
            manager.TransitionState(StateType.WaterIdle1);

        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);

        if(parameter.target)
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

        //如果发生了碰撞
        if(parameter.othercolli)
        {
            manager.transform.rotation = Quaternion.Euler(0, 180, 0);//旋转指定度数
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


public class DuckFloorToFlyState : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    private AnimatorStateInfo info;
    public DuckFloorToFlyState(FSM manager)
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


public class DuckFlyState : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    private float Flytimer;
    public DuckFlyState(FSM manager)
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

        if (timer >= 5.0f)
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



        if (Flytimer>=3.0f)
        {
            manager.TransitionState(StateType.Load);
            Flytimer = 0;
        }
    }
    public void OnExit()
    {
        Flytimer = 0;
    }
}

public class DuckLoadState : Duck
{
    private FSM manager;
    private Parameter parameter;
    public DuckLoadState(FSM manager)
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
        manager.parameter.rigid.AddForce(0, 8, 0);
        manager.transform.Translate(Vector3.forward * parameter.moveSpeed * Time.deltaTime);
        manager.transform.Translate(Vector3.down * parameter.flySpeed *2 * Time.deltaTime);
        if (parameter.collision)
        {
            manager.TransitionState(StateType.Idle1);
        }
        if (parameter.Water)
            manager.TransitionState(StateType.WaterIdle1);
    }
    public void OnExit()
    {
        
    }
}

public class DuckWaterIdle1State : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public DuckWaterIdle1State(FSM manager)
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

public class DuckWaterIdle2State : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;

    public DuckWaterIdle2State(FSM manager)
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


public class DuckSwimState : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    private float timer1;
    public DuckSwimState(FSM manager)
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

        if (!parameter.Water)
            manager.TransitionState(StateType.Walk);
    }

    public void OnExit()
    {
        timer = 0;
        timer1 = 0;
    }
}

public class DuckWaterIdle3State : Duck
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    public DuckWaterIdle3State(FSM manager)
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