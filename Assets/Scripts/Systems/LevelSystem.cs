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

namespace Ahmetson.Serdar.Systems.LevelSystem {
	public class LevelSystem : EventReactionSystem<LevelStateEvent> {

		public LevelSystem( IEventSystem eventSystem ) : base( eventSystem ) { }

		List<RoadContinuation> currentActiveRoads;

		//[Inject]
		public splineMove _tribeMoveManager;

		[Inject]
		IPoolManager poolManager;

		[Inject]
		FightingSystem fightingSystem;

		private void InitSystem() {
			// Get the list of Tribes
			// Get the list of s
		}

		/*private void InitSigns() {
			IPool poolCampus = poolManager.GetPool ( "Level Campuses" );
			if ( null == poolCampus ) {
				Debug.LogError ( "Pool for campus containing is missing!" );
			}

			IPool poolSigns = poolManager.GetPool ( "Level Signs" );
			if (null == poolSigns ) {
				Debug.LogError ( "Pool for signs containing is missing!" );
			}

			List<Entity> foundEntities = new List<Entity>();	float minDistance = -1f;	Entity foundEntity = null;
			List<Entity> foundZones = new List<Entity> ();
			foreach ( Entity signEntity in poolSigns.Entities ) {
				Sign sign = signEntity.GetComponent<ViewComponent> ().View.GetComponent<Sign> ();

				if ( SIGN_TYPE.CAMP != sign.SignType ) {
					continue;
				}

				foreach ( Entity campusEntity in poolCampus.Entities ) {
					Vector3 campusPosition = campusEntity.GetComponent<ViewComponent> ().View.transform.position;

					float distance = Vector3.Distance ( sign.gameObject.transform.position, campusPosition );

					if ( -1f == minDistance || minDistance > distance ) {
						minDistance = distance;
						foundEntity = campusEntity;
					}
				}

				if (null == foundEntity) {
					break;				// There are no campuses in the Level
				}

				// Signs cannot attach to the same Campus. So track the binded campuses
				if ( foundEntities.IndexOf ( foundEntity ) < 0 ) {
					foundEntities.Add ( foundEntity );
				}

				// All of this campus finding is did to assign closest campus to the sign.
				sign.campus = foundEntity.GetComponent<ViewComponent> ().View;
				minDistance = -1f;
				foundEntity = null;
			}
		}*/


		public override void EventTriggered ( LevelStateEvent eventData ){
			switch ( eventData.state ) {
			case LEVEL_STATE.SELECT_ROAD:
				RoadSelection ( eventData.roadsToContinue );
				break;
			case LEVEL_STATE.SERDAR_WALK:
				StartWalking ( eventData.road );
				break;
			case LEVEL_STATE.FIGHT:
				StartFighting ( eventData.campus, eventData.tribe );
				break;
			}
		}

		private void RoadSelection( List<RoadContinuation> roadsToContinue ) {

			currentActiveRoads = roadsToContinue;

			foreach ( RoadContinuation roadToContinue in roadsToContinue ) {
				// Show Hint
				// Make Animation on Road
				roadToContinue.continuation.StartRoadInteractivity ();
				// Run Interactive System on Active Roads
			}

		}

		private void StartWalking ( Road road ) {
			// Start walking on the road first time? or continue to walk
			if (road != null) {
				foreach (RoadContinuation currentActiveRoad in currentActiveRoads) {
					currentActiveRoad.continuation.StopRoadInteractivity ();
				}
				currentActiveRoads = road.Continuations;

				GetTribeMoveManager().pathContainer = road.pathContainer;
				GetTribeMoveManager().StartMove ();
			} else {

				GetTribeMoveManager ().Resume ();
			}
				
		}

		private splineMove GetTribeMoveManager () {
			if ( null == _tribeMoveManager ) {
				GameObject obj = GameObject.FindGameObjectWithTag ( "Player" );
				if ( null == obj ) {
					Debug.LogError("Can not find tribe in the scene!");
				}
				_tribeMoveManager = obj.GetComponent<splineMove> ();
			}

			return _tribeMoveManager;
		}

		private void StartFighting ( GameObject campus, GameObject tribe ) {
			GetTribeMoveManager ().Pause ();
			GameObject fighting = GameObject.Instantiate ( Resources.Load ( "FightingEnemy" ) as GameObject, campus.transform.position, campus.transform.rotation );

			Debug.Log ("Start Fighting! "+tribe.name+" at "+campus.name);
			fightingSystem.StartFighting (tribe, campus);

		}
	}

}
