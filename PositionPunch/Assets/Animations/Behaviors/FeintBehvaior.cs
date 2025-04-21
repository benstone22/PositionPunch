using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeintBehvaior : StateMachineBehaviour
{
    // Start is called before the first frame update
    protected Fighter _fighter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        _fighter = animator.GetComponentInParent<Fighter>();
        animator.applyRootMotion=false;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _fighter = animator.GetComponentInParent<Fighter>();
        _fighter.ResetPosition();
        animator.applyRootMotion = true;
    }

}
