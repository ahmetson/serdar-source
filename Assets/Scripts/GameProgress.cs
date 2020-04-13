using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Ahmetson.Serdar.Enums;
using Zenject;
using EcsRx.Events;
using Ahmetson.Serdar.Events;
using System.IO;

namespace Ahmetson.Serdar {

    [Serializable]
    public class GameProgressSceneInformation
    {
        public SCENE_TYPE SceneType;
        public string SceneName;

        public GameProgressSceneInformation(SCENE_TYPE sceneType, string sceneName)
        {
            SceneType = sceneType;
            SceneName = sceneName;
        }
    }

	public class GameProgress : MonoBehaviour{
		private string _gameProgressPath = "GameProgress.json";

        public GameProgressSceneInformation[] GameFlow;

        [Inject]
        IEventSystem EventSystem;
        private IDisposable _subscription;

        private IntReactiveProperty _currentGameProgress = new IntReactiveProperty();

        public void OnStart()
        {
            _subscription = EventSystem.Receive<SceneSwitchEvent>().Subscribe(UpdateGameProgress);

            // Convert to Promise, then subscribe observe every change
            LoadGameProgress();

            // TODO
            //_currentGameProgress.ObserveEveryValueChanged(SaveGameProgress);
        }

        private void UpdateGameProgress(SceneSwitchEvent eventData)
        {
            if (eventData.SceneType == SCENE_TYPE.LEVEL_COMPLETITION)
            {
                Debug.Log("Update Game Progress");

                if (_currentGameProgress.Value == GameFlow.Length - 1)
                {
                    _currentGameProgress.SetValueAndForceNotify(0);
                } else
                {
                    _currentGameProgress.SetValueAndForceNotify(_currentGameProgress.Value + 1);
                }
            }
        }

        public void OnDestroy()
        {
            if (_subscription != null)
            _subscription.Dispose();
        }

        /// <summary>
        /// Clears the progress.
        /// </summary>
        public void ClearCurrentProgress() {
			//SerdarApplication.Global.currentGameProgress = new ReactiveProperty<CurrentGameProgress> (new CurrentGameProgress(InitialLevel));
			//SerdarApplication.Global.SaveGameProgress ();
		}

		public int GetProgressByPercentage(int currentPercentage) {
            return 0;// (MaxLevel / 100) * currentPercentage;
		}

   
        public void LoadGameProgress()
        {
            string path = Path.Combine(Application.streamingAssetsPath, _gameProgressPath);

            if (File.Exists(path))
            {
                string progressAsJson = File.ReadAllText(path);
                int i = int.Parse(progressAsJson);// JsonUtility.FromJson<int>(progressAsJson);
                //currentGameProgress = new ReactiveProperty<CurrentGameProgress>(_gameProgress);
                _currentGameProgress.Value = i;
            }
            else
            {
                //currentGameProgress = new ReactiveProperty<CurrentGameProgress>(new CurrentGameProgress(gameProgress.InitialLevel));
                _currentGameProgress.Value = 0;
            }
        }

        public void SaveGameProgress(int newValue)
        {
            string path = Path.Combine(Application.streamingAssetsPath, _gameProgressPath);

            //string progressAsJson = JsonUtility.ToJson(currentGameProgress.Value);
            string progressAsJson = _currentGameProgress.Value.ToString();
            File.WriteAllText(path, progressAsJson);
        }
    }
}
