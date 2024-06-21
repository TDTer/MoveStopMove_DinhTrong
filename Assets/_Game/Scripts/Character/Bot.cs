using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] protected NavMeshAgent agent;

    protected IState<Bot> currentState;

    private bool isRunning = false;
    private Vector3 destination;

    public float walkRadius = 10.0f;

    public bool IsDestination => Vector3.Distance(TF.position, destination) - Mathf.Abs(TF.position.y - destination.y) < 0.1f;

    public override void OnInit()
    {
        base.OnInit();
        ResetAnim();
    }

    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    public void SetDestination(Vector3 point)
    {
        destination = point;
        agent.enabled = true;
        agent.SetDestination(destination);
        ChangeAnim(Constant.ANIM_RUN);
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }

    }

    public override void OnDeath()
    {
        OnMoveStop();
        base.OnDeath();
        ChangeState(null);
        Invoke(nameof(OnDespawn), TIME_ON_DEATH);
    }

    public override void OnMoveStop()
    {
        base.OnMoveStop();
        agent.enabled = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }
    // public Vector3 RandomPoint()
    // {
    //     Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
    //     randomDirection += transform.position;
    //     NavMeshHit hit;
    //     Vector3 finalPosition = Vector3.zero;
    //     if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
    //     {
    //         finalPosition = hit.position;
    //     }
    //     return finalPosition;
    // }

    public override void Throw()
    {
        base.Throw();
    }

    public override void WearClothes()
    {
        base.WearClothes();

        //random 
        ChangeWeapon(Utilities.RandomEnumValue<WeaponType>());
        ChangePant(Utilities.RandomEnumValue<PantType>());
    }
}
