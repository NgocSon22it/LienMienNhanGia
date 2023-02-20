using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayer_Walking : StateMachineBehaviour
{
    OfflinePlayer offlinePlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        offlinePlayer = animator.GetComponent<OfflinePlayer>();
        offlinePlayer.Amation_SetUpWalk(true);
    }
}
