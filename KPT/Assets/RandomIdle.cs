using UnityEngine;
using System.Collections;

public class RandomIdle : StateMachineBehaviour {
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
        animator.SetInteger("IdleIndex", Random.Range(0, 3));
    }
}