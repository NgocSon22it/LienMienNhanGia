using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayer_Walking : StateMachineBehaviour
{
    OnlinePlayer onlinePlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onlinePlayer = animator.GetComponent<OnlinePlayer>();
        onlinePlayer.Amation_SetUpWalk(true);
    }
}
