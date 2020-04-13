using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using EcsRx.Pools;
using EcsRx.Entities;
using Ahmetson.Serdar.Component;
using Assets.EcsRx.Unity.Extensions;
using Ahmetson.Serdar.Configuration;
using Ahmetson.Serdar.Events;
using EcsRx.Unity.Components;
using Ahmetson.Serdar.Component;
using Ahmetson.Serdar.Monobehaviours;
using EcsRx.Events;

/**
 * The Serdar's Scene Manager:
 * It doesn't depend's on the current scene.
 * It has MAIN_MENU
 *          OPTIONS
 *          CUTSCENE
 *          TUTORIAL
 *          LEVEL
 *          LEVEL_COMPLITION
 *          MAP
 *          CREDITS & ABOUT
 *          
 * It has LEVEL_BEGIN
 *        GAME_EXIT
 *        BACK_MENU
 * methods
 * 
 * It has Game Progress:
 *      Scenes list:
 *          Scene, scene type
 *          
 */

/**
 * Serdar's GameData Manager
 * 
 * Holds current User's progress:
 *      Completed Level Progress at GameProgress
 */

namespace Ahmetson.Serdar {
	public class LevelSceneInformation : MonoBehaviour, ISceneInformation {

		public LEVEL_STATE initialState;

		[SerializeField]
		string _nameSpace;
		public string NameSpace {
			get { return _nameSpace; }
			set { _nameSpace = value; }
		}

		[SerializeField]
		int _nextSceneId;
		public int NextSceneId {
			get { return _nextSceneId; }
			set { _nextSceneId = value; }
		}

		public List<RoadContinuation> InitialRoads;

        [Inject]
        PoolManager poolManager;

        [Inject]
        IEventSystem eventSystem;

		void Start() {
			//if (null == SerdarApplication.Global) {
			//	SceneSwitcher sceneSwitcher = new SceneSwitcher ();
			//	sceneSwitcher.OpenInitialScene();	// Switch to Begin, to initialize the Application
			//	return;
			//}

			// Mark objects on Screen as Interactive
			Road[] roads = GameObject.FindObjectsOfType<Road> ();
			IPool pool = poolManager.GetPool();
			foreach ( Road road in roads ) {
				IEntity entity = pool.CreateEntity ();
				entity.AddComponent<RoadComponent> (  );
				road.gameObject.LinkEntity (entity, pool);
			}

			// Mark all campuses as entities so systems may catch them
			Transform enemyCampusesList = GameObject.FindGameObjectWithTag ( "Enemy Campuses" ).transform;
			Transform[] enemyCampuses = enemyCampusesList.GetComponentsInChildren<Transform>();
			IPool campusesPool = poolManager.CreatePool ( "Level Campuses" );
			foreach ( Transform enemyCampus in enemyCampuses ) {
				if ( enemyCampusesList.GetInstanceID () == enemyCampus.GetInstanceID () ||
					enemyCampus.parent != enemyCampusesList ) {
					continue;
				}
				IEntity entity = campusesPool.CreateEntity ();
				entity.AddComponent<CampusComponent> (  );
				enemyCampus.gameObject.LinkEntity ( entity, campusesPool );

				// Prepare campus to fight
				Debug.Log( enemyCampus.childCount+" warriors!" );
			}


			// Mark all signs so Systems may catch them
			IPool signsPool = poolManager.CreatePool ( "Level Signs" );
			Sign[] signs = GameObject.FindObjectsOfType<Sign> ();
			foreach ( Sign sign in signs ) {
				IEntity entity = signsPool.CreateEntity ();
				entity.AddComponent<SignComponent> (  );
				sign.gameObject.LinkEntity ( entity, signsPool );
			}


				
			// Set the Data on Sound & Exit buttons
			//SerdarApplication.Global.currentSceneInformation = this;
			//SerdarApplication.Global.InitNewScene ();

			LevelStateEvent eventData = new LevelStateEvent ();
			eventData.state = initialState;
			eventData.roadsToContinue = InitialRoads;
			eventSystem.Publish ( eventData );
		}

	}
}