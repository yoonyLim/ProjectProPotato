using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTranformingBehavior : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.state = GameManager.professorState.Transforming;

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.state = GameManager.professorState.Idle;
    }
}
