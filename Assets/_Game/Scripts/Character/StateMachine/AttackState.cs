using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.OnMoveStop();
        t.OnAttack();
        t.Throw();

        t.ChangeState(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
    }

    public void OnExecute(Bot t)
    {

    }

    public void OnExit(Bot t)
    {
    }


}
