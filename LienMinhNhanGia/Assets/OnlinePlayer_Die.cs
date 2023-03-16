using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayer_Die : StateMachineBehaviour
{
    OnlinePlayer onlinePlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onlinePlayer = animator.GetComponent<OnlinePlayer>();
        onlinePlayer.IsDie = true;
        onlinePlayer.rigidbody2d.isKinematic = true;
        onlinePlayer.capsuleCollider2D.enabled = false;
        onlinePlayer.SetUpSpeedAndJumpPower(0, 0);
    }




}
