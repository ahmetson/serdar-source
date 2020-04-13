using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using System;
using Ahmetson.Serdar.Enums;
using EcsRx.Systems.Custom;
using Ahmetson.Serdar.Events;
using EcsRx.Events;
using EcsRx.Unity;

namespace Ahmetson.Serdar.Systems.ApplicationSystem {

    [Serializable]
    public class GameFlowScene
    {
        public string Name;         // Scene Name;
        public SCENE_TYPE Type;     // Scene Type;
        public GameFlowScene(string n, SCENE_TYPE t)
        {
            Name = n;
            Type = t;
        }
    }

	/// <summary>
	/// Scene switcher.
	/// Manages the Scene Switching
	/// </summary>
	public class SceneSwitchSystem : EventReactionSystem<SceneSwitchEvent> {

		public string InitialScene;
		public string MainMenu;
        public string Options;
        public string Credits;
        public string Map;
        public string LevelCompletition;
        //public string[] Cutscenes 	= 2;
        //public string[] FullTutorials	= new int[2] {5, 6};
        //      public string[] Levels;

        [Inject]
        EcsRxApplication SerdarApplication;

        public SceneSwitchSystem(IEventSystem eventSystem) : base(eventSystem)
        {
        }

        public override void EventTriggered(SceneSwitchEvent eventData)
        {
            Debug.Log("Switching Scenes");
            SwitchTo(eventData.SceneName);
        }

        [SerializeField]
        public GameFlowScene[] GameFlow;

        private SCENE_TYPE current_scene_type;

        //public SceneSwitcher ()
        //{
        //    current_scene_type = SCENE_TYPE.INITIAL_SCENE;
        //}

		private void PrepareToSwitch() {
			//if (null != SerdarApplication.Global)
			//	SerdarApplication.Global.UnregisterSceneSystems ();
		}

		/// <summary>
		/// Switchs to next scene.
		/// </summary>
		public void SwitchToNextScene() {
            //if (IsGameProgressSceneType(current_scene_type))
			//SwitchTo (GetNextSceneName(GetCurrentSceneName()));
		}

		public void OpenInitialScene() {
            StartGame();
		}

		/// <summary>
		/// Switchs to home| main menu.
		/// </summary>
		public void OpenMainMenu() {
			SwitchTo (MainMenu);
		}

		public void OpenLevelCompletition() {
			SwitchTo (LevelCompletition);
		}

		/// <summary>
		/// Switchs to post level scene.
		/// </summary>
		public void EndGame() {
            Application.Quit();
		}

		public void StartGame() {
			SwitchTo (InitialScene);
		}

		public void OpenMap() {
			SwitchTo (Map);
		}

		public void MoveOnGameFlow(int level) {
			//SwitchTo (GetLevelSceneId(level));
		}

		private void SwitchTo(string sceneName) {
			PrepareToSwitch ();

			SceneManager.LoadScene (sceneName);
		}

		private string GetCurrentSceneName() {
			return SceneManager.GetActiveScene ().name;
		}

		private string GetSceneNameByType(SCENE_TYPE scene_type) {

            if (scene_type == SCENE_TYPE.LEVEL_COMPLETITION)
            {
                return LevelCompletition;
            }
            if (scene_type == SCENE_TYPE.CREDITS) {
                return Credits;
            }
            if (scene_type == SCENE_TYPE.OPTIONS) {
                return Options;
            }
            if (scene_type == SCENE_TYPE.INITIAL_SCENE)
            {
                return InitialScene;
            }

            return InitialScene;
		}

        private SCENE_TYPE GetNextSceneType(SCENE_TYPE scene_type)
        {
            if (scene_type == SCENE_TYPE.LEVEL_COMPLETITION)
            {
                return SCENE_TYPE.MAP;
            }
            if (scene_type == SCENE_TYPE.FULL_TUTORIAL || scene_type == SCENE_TYPE.CUTSCENE || scene_type == SCENE_TYPE.LEVEL)
            {
                return SCENE_TYPE.LEVEL_COMPLETITION;
            }
            if (scene_type == SCENE_TYPE.CREDITS || scene_type == SCENE_TYPE.OPTIONS || scene_type == SCENE_TYPE.INITIAL_SCENE)
            {
                return SCENE_TYPE.MAIN_MENU;
            }

            return SCENE_TYPE.INITIAL_SCENE;
        }

        private bool IsGameProgressSceneType(SCENE_TYPE scene_type)
        {
            if (scene_type == SCENE_TYPE.MAP)
            {
                return true;
            }
            return false;
        }
	}
}
