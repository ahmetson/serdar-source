using System.Collections;
using UnityEngine;
using EcsRx.Unity;
using Zenject;
using System.IO;
using UniRx;
using Ahmetson.Serdar.Systems.LevelSystem;
using Ahmetson.Serdar.Events;
using UnityEngine.SceneManagement;
using System;

namespace Ahmetson.Serdar {
	public class SerdarApplication : EcsRxApplication {
		//[Inject]
		//public ISceneInformation currentSceneInformation;

		//[Inject]
		//public ApplicationLevelInformation applicationInformation;

		public static LevelResults levelResults;

		public FightingSystem fightingSystem = null;

        public string ApplicationLevelNamespace;
        public string InitialSceneNamespace;


		protected override void ApplicationStarting() {		// Alternative of Awake at MonoBehaviour
			// Get Scene Switcher
			//sceneSwitcher = new SceneSwitcher();

			// Run the Logic
			//RegisterApplicationSystems();
			//RegisterSceneSystems (currentSceneInformation.NameSpace);
            //this.BindAllSystemsWithinApplicationScope();

            //DontDestroyOnLoad (gameObject);
            eventSystem.Receive<SceneSwitchEvent>().Delay(TimeSpan.FromSeconds(0.1f)).Subscribe(eventData =>
            {
                Debug.LogError("Switching Scene in Application mode and registering "+eventData.SceneNameSpace);
                this.UnregisterSceneSystems();
                this.RegisterSceneSystems(eventData.SceneNameSpace);
            });
        }

		protected override void ApplicationStarted(){		// Alternative of Start at MonoBehaviour
			// Application Level Information

			// Scene Level Information 
			Debug.Log("Application started");

            //this.RegisterAllBoundSystems();
            RegisterApplicationSystems(ApplicationLevelNamespace);
            RegisterSceneSystems(InitialSceneNamespace);

        }

        public void InitNewScene() {
			//RegisterSceneSystems (currentSceneInformation.NameSpace);
		}

		public void NewGame() {
			//gameProgress.ClearCurrentProgress ();
            //sceneSwitcher.StartGame ();
            //eventSystem.Publish(new SceneSwitchEvent(Enums.SCENE_TYPE.INITIAL_SCENE, "Init"));
        }
	}
}
