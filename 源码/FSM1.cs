using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateType1
{
    Idle1, Idle2, Idle3,Walk, Fly,FloorToFly,Load,WaterIdle1,WaterIdle2,WaterIdle3,Swim
}

[Serializable]
public class Parameter1
{
    public float moveSpeed;
    public float flySpeed;
    public float idleTime;
    public Transform[] patrolPoints;
    public Transform[] flyPoints;
    public Transform target;
    public Animator animator;
    public Rigidbody rigid;
    public bool collision;
    public bool othercolli;
}
public class FSM1 : MonoBehaviour
{
    public  Parameter parameter=new Parameter();
    [HideInInspector]
    public AudioSource duckAudio;
    public AudioClip duckSpeak;
    private Duck currentState;
    private Dictionary<StateType, Duck> states = new Dictionary<StateType, Duck>();
    // Start is called before the first frame update
    void Start()
    {
        parameter.animator = GetComponent<Animator>();
        parameter.rigid = GetComponent<Rigidbody>();

        states.Add(StateType.Idle1, new DuckIdle1State1(this));
        states.Add(StateType.Idle2, new DuckIdle2State1(this));
        states.Add(StateType.Walk, new DuckWalkState1(this));
        states.Add(StateType.FloorToFly, new DuckFloorToFlyState1(this));
        states.Add(StateType.Fly, new DuckFlyState1(this));
        states.Add(StateType.Load, new DuckLoadState1(this));
        states.Add(StateType.WaterIdle1, new DuckWaterIdle1State1(this));
        states.Add(StateType.WaterIdle2, new DuckWaterIdle2State1(this));
        states.Add(StateType.WaterIdle3, new DuckWaterIdle3State1(this));
        states.Add(StateType.Swim, new DuckSwimState1(this));

        TransitionState(StateType.Idle1);

        duckAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
        if(target != null)
        {
            if(target.position.x>target.position.x)
            {
                transform.localScale = new Vector3(-1, -1, -1);
            }
            else if(transform.position.x<target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
        else
        {
            parameter.target = null;
        }

        //if (other.CompareTag("Cube"))
        //{
        //    Debug.Log("避障");
        //    parameter.othercolli = true;
        //}
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            Debug.Log("已触碰地面");
            parameter.collision = true;
        }
        if (collision.gameObject.tag == "Cube")
        {
            parameter.othercolli = true;
        }

    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Cube")
        {
            parameter.othercolli = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag=="Respawn")
        {
            Debug.Log("离开地面");
            parameter.collision = false;
        }
        if (collision.gameObject.tag == "Cube")
        {
            parameter.othercolli = false;
        }
    }


    

}
