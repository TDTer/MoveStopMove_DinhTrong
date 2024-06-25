using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.OnMoveStop();
        if (t.IsCanAttack)
        {
            t.OnAttack();
            t.Throw();
            t.IsCanAttack = false;
            t.StartCoroutine(CoolDownAttack(Character.TIME_ON_COOLDOWN));
        }
        t.ChangeState(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());

        IEnumerator CoolDownAttack(float time)
        {
            yield return new WaitForSeconds(time);

            t.IsCanAttack = true;
        }
    }

    public void OnExecute(Bot t)
    {

    }

    public void OnExit(Bot t)
    {
    }


}
