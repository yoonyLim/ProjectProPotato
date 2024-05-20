using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackAnimation : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.state = GameManager.professorState.Attack;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.state = GameManager.professorState.Idle;
    }
}
