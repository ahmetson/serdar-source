using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Ahmetson.Serdar.Systems.LevelSystem;
using Ahmetson.Serdar;

public class WarriorAnimationBehaviour : StateMachineBehaviour {

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
		//if (stateInfo.IsName ("Warrior Attack")) {
		//	if (SerdarApplication.Global.fightingSystem == null)
		//		Debug.LogError ("Fighting system is null");
		//	else
		//		SerdarApplication.Global.fightingSystem.FightAnimationEnd (animator.gameObject);
		//} 
		//if (stateInfo.IsName ("Warrior Die")) {
		//	if (SerdarApplication.Global.fightingSystem == null)
		//		Debug.LogError ("Fighting system is null");
		//	else
		//		SerdarApplication.Global.fightingSystem.DieAnimationEnd ();
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
