using EcsRx.Systems;
using EcsRx.Systems.Custom;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;
using Ahmetson.Serdar.Events;
using EcsRx.Entities;
using Ahmetson.Serdar.Configuration;
using EcsRx.Events;
using System.Collections.Generic;
using SWS;
using Zenject;
using EcsRx.Pools;
using EcsRx.Unity.Components;
using Ahmetson.Serdar.Monobehaviours;
using TMPro;
using System.Text;

namespace Ahmetson.Serdar.Systems.LevelSystem {
	public class FightingSystem : IManualSystem {

		public IGroup TargetGroup { get { return new EmptyGroup();} }

		enum SIDE {
			ALLY,
			ENEMY
		};

		[Inject]
		IPoolManager poolManager;

		// Total number of lives
		List<GameObject> allyWarriors	= new List<GameObject>();
		List<GameObject> enemyWarriors	= new List<GameObject> ();

		GameObject currentFightingAlly	= null;
		GameObject currentFightingEnemy	= null;
		SIDE 		currentFighter		= SIDE.ALLY;

		SIDE firstAttacker = SIDE.ALLY;

		public int defaultDamageAmount = 1;

		private GameObject reduceAnimationScore;

        [Inject]
        IEventSystem eventSystem;

		public  void StartSystem ( IGroupAccessor @group ) { 
			//SerdarApplication.Global.fightingSystem = this;
			reduceAnimationScore = GameObject.Instantiate(Resources.Load("Warrior UI")) as GameObject;
			reduceAnimationScore.SetActive (false);
		}
		public void StopSystem ( IGroupAccessor group ) {}

		// Shows fighting animation of character
		public void FightAnimationEnd(GameObject warrior) {
			PostAttackEffectStart ();
		}

		public void FightAnimationStart(GameObject warrior) {
			SetWarriorAnimation(warrior, "Normal Attack");
		}

		void HealthReducingAnimationStart() {
			// Set the damage amount on score
			// Start the Health reduce animation
			GameObject warriorsScore = GetChildGameObject (GetOpponentFightingWarrior(), "Score", true);
			reduceAnimationScore.transform.position = warriorsScore.transform.parent.parent.transform.position;		// Parent (Canvas), Parent->Parent (Canvas -> Canvas Wrapper in Warrior)
			reduceAnimationScore.SetActive (true);

			SetWarriorScoreUIValue (GetChildGameObject(reduceAnimationScore, "Score", true), defaultDamageAmount, true);
			SetWarriorScoreAnimation (GetChildGameObject(reduceAnimationScore, "Score", true), "Health Fade Out");
		}

		//		Get the Score of Current active warrior
		//		Start the Health value change beginning animation
		public void HealthReducingAnimationEnd() {
			reduceAnimationScore.SetActive (false);

			GameObject score = GetChildGameObject (GetOpponentFightingWarrior(), "Score", true);
			SetWarriorScoreAnimation(score, "Health Change Begin");
		}

		//		Set the new score for health
		//		Start the Health value change ending animation
		public void HealthChangeBeginningAnimationEnd() {
			GameObject score = GetChildGameObject (GetOpponentFightingWarrior(), "Score", true);
			int scoreValue = GetHealthValue(score.GetComponent<TextMeshProUGUI> ().text);
			scoreValue -= defaultDamageAmount;

			SetWarriorScoreUIValue (score, scoreValue);
			SetWarriorScoreAnimation( score, "Health Change End");
		}

		//		Call Post Attack Effect End
		public void HealthChangeEndingAnimationEnd() {
			PostAttackEffectEnd ();
		}

		private void PostAttackEffectStart() {
			HealthReducingAnimationStart ();
		}

		public void PostAttackEffectEnd() {
			FightCalculation ();
		}

		public void StartFighting(GameObject tribe, GameObject campus) {
			// Get the list of warriors of tribe
			allyWarriors = GetWarriorsList(tribe);
			// Get the list of warriors of campus
			enemyWarriors = GetWarriorsList(campus);

			// Set the order of warriors in the tribe, so the Serdar will die at the end.
			// TODO

			if (allyWarriors.Count == 0 || enemyWarriors.Count == 0) {
				Debug.LogError("Allies: "+allyWarriors.Count+" and enemies: "+enemyWarriors.Count);
				CheckGameOver ();
				return;
			}

			// Get the fighting man from tribe
			currentFightingAlly		= allyWarriors[0];
			// Get the fighting man from campus
			currentFightingEnemy	= enemyWarriors[0];

			// Start Preparing to Fight
			PrepareFighting();
		}

		//		Plays sound that indicates about beginning of fight?
		//		Start Fighting Animation Start
		void PrepareFighting() {
			// Set the link to the fighting man (Either equal to tribe's man or campus man)
			// And choose the starter of fight from settings?
			currentFighter = firstAttacker;

			FightAnimationStart (GetCurrentFightingWarrior());
		}

		void FightCalculation( ) {
			//Debug.LogError ("End of fighting calculation");
			//	If the life of warrior is 0? 
			//		Show KillWarrior method
			//	else
			//		Update the life of Warrior
			//		if warrior is second attacker?
			//			@FightAgain
			//		else
			//			Set the second warrior as active
			//			Start Fight Animation Start
			if (GetWarriorHealth (GetOpponentFightingWarrior ()) <= 0) {
				KillOpponentWarrior ();
			} else {
				if (currentFighter != firstAttacker) {
					FightAgain ();
				} else {
					if (firstAttacker == SIDE.ALLY) {
						currentFighter = SIDE.ENEMY;
					} else {
						currentFighter = SIDE.ALLY;
					}
					FightAnimationStart (GetCurrentFightingWarrior());
				}
			}			
		}

		void KillOpponentWarrior() {
			// Kill Warrior method:
			//		(Later count the scores)
			//		Delete the warrior from the active fighting persona
			//		Start the DieAnimationStart method
			GameObject dying;
			if (SIDE.ALLY == currentFighter) {
				dying = currentFightingEnemy;
				currentFightingEnemy = null;
			} else {
				dying = currentFightingAlly;
				currentFightingAlly = null;
			}
			if (dying == null) {
				Debug.LogError ("NULLL");
			} 
			SetWarriorAnimation (dying, "Die");
		}

		void FightAgain() {
			currentFighter = firstAttacker;
			FightAnimationStart (GetCurrentFightingWarrior());
		}

		public void DieAnimationEnd() {
			// DieAnimationEnd:
			//		Detect the warrior's side (Check tribe's warrior or campuses based on checking active fighters
			//		If in the list of warriors is empty:
			//			If the Calculation is for second warrior?
			//				@CheckGameOver
			//			Else
			//				Result Calculation (Side = 1);
			//		Else
			//			Get from list another warrior as active persona
			//			if result calculation for second warrior
			//				@FightAgain
			//			else
			//				Result Calculation (Side = 1)
			List<GameObject> dyingSide;
			if (SIDE.ALLY == currentFighter) {
				dyingSide = enemyWarriors;
			} else {
				dyingSide = allyWarriors;
			}

			dyingSide.RemoveAt (0);


			if (0 == dyingSide.Count) {
				CheckGameOver ();
			} else {
				if (SIDE.ALLY == currentFighter) {
					currentFightingEnemy = dyingSide [0];
				} else {
					currentFightingAlly = dyingSide [0];
				}

				if (firstAttacker != currentFighter) {
					FightAgain ();
				} else {
					SetLastAttacker ();
					FightAnimationStart (GetCurrentFightingWarrior());
					//FightCalculation ();
				}
			}
		}

		void CheckGameOver() {
			// If the user's side is losed fight
			//	TODO get the information that should be send to another scene
			//	Switch scene to game over
			// Else
			//	Continue walking
			if (0 == allyWarriors.Count) {
				Debug.LogError ("Game Over(");
			} else {
				LevelStateEvent eventData = new LevelStateEvent ();
				eventData.state = LEVEL_STATE.SERDAR_WALK;
				eventData.road = null;
				eventSystem.Publish ( eventData );
			}
		}

		// Helper function
		List<GameObject> GetWarriorsList ( GameObject campus ) {
			List<GameObject> warriorsList = new List<GameObject>();
			Transform[] warriors = campus.GetComponentsInChildren<Transform>();
			foreach ( Transform warrior in warriors ) {
				if ( warrior.parent != campus.transform ) {
					continue;
				}
				warriorsList.Add (warrior.gameObject);
			}
			return warriorsList;
		}

		GameObject GetChildGameObject ( GameObject parent, string childName, bool descendantOK = false ) {
			GameObject childObject = null;
			Transform[] children = parent.GetComponentsInChildren<Transform>();
			foreach ( Transform child in children ) {
				if ( descendantOK == false && child.parent != parent.transform ) {
					continue;
				}
				if (child.gameObject.name.Contains (childName)) {
					childObject = child.gameObject;
					break;
				}
			}
			return childObject;
		}

		GameObject GetCurrentFightingWarrior() {
			if (currentFighter == SIDE.ALLY) {
				return currentFightingAlly;
			}
			return currentFightingEnemy;
		}

		GameObject GetOpponentFightingWarrior() {
			if (currentFighter == SIDE.ALLY) {
				return currentFightingEnemy;
			}
			return currentFightingAlly;
		}

		int GetHealthValue(string value) {
			int result = 0;
			int.TryParse (value, out result);
			return result;
		}

		int GetWarriorHealth (GameObject warrior) {
			GameObject score = GetChildGameObject (warrior, "Score", true);
			return GetHealthValue(score.GetComponent<TextMeshProUGUI> ().text);
		}

		void SetWarriorAnimation(GameObject warrior, string state) {
			GetChildGameObject(warrior, "Body").GetComponent<Animator> ().SetTrigger(state);
		}

		void SetWarriorScoreAnimation(GameObject score, string state) {
			score.GetComponent<Animator> ().SetTrigger (state);
		}

		void SetWarriorScoreUIValue(GameObject score, int value, bool reducing = false) {
			StringBuilder newValue = new StringBuilder ();
			if (reducing) {
				newValue.Append ("-");
			}
			newValue.Append (value);
			score.GetComponent<TextMeshProUGUI> ().text = newValue.ToString();
		}

		void SetLastAttacker () {
			if (firstAttacker == SIDE.ALLY) {
				currentFighter = SIDE.ENEMY;
			} else {
				currentFighter = SIDE.ALLY;
			}
		}
	}

}
