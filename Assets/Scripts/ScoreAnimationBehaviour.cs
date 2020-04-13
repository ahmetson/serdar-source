using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ahmetson.Serdar;

public class ScoreAnimationBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//if (stateInfo.IsName ("Warrior Health Fade Out")) {
		//	if (SerdarApplication.Global.fightingSystem == null)
		//		Debug.LogError ("Fighting system is null");
		//	else {
		//		//Debug.LogError ("End of State");
		//		//animator.gameObject.SetActive (false);
		//		SerdarApplication.Global.fightingSystem.HealthReducingAnimationEnd ();
		//	}
		//} else if (stateInfo.IsName ("Warrior Health Change Begin")) {
		//	if (SerdarApplication.Global.fightingSystem == null)
		//		Debug.LogError ("Fighting system is null");
		//	else
		//		SerdarApplication.Global.fightingSystem.HealthChangeBeginningAnimationEnd ();
		//} else if (stateInfo.IsName ("Warrior Health Change End")) {
		//	if (SerdarApplication.Global.fightingSystem == null)
		//		Debug.LogError ("Fighting system is null");
		//	else
		//		SerdarApplication.Global.fightingSystem.HealthChangeEndingAnimationEnd ();
		//} 
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
