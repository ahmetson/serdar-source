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
using System.Linq;
using Ahmetson.Serdar.Helpers;

namespace Ahmetson.Serdar {
	public class LevelCompletitionSceneInformation : MonoBehaviour, ISceneInformation {


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

        [Inject]
        PoolManager poolManager;

		[HideInInspector]
		public Button[] buttons;


		void Start() {
			//if (null == SerdarApplication.Global) {
			//	SceneSwitcher sceneSwitcher = new SceneSwitcher ();
			//	sceneSwitcher.OpenInitialScene();	// Switch to Begin, to initialize the Application
			//	return;
			//}

			// Mark objects on Screen as Interactive
			buttons = GameObject.FindObjectsOfType<Button> ();
			IPool pool = poolManager.GetPool();
			UIInteractiveHelper helper = new UIInteractiveHelper ();
			foreach (Button button in buttons) {
				IEntity entity = pool.CreateEntity ();
				entity.AddComponent<UIInteractiveComponent> ();
				UIInteractiveComponent interactiveComponent = entity.GetComponent<UIInteractiveComponent> ();
				interactiveComponent.Action = helper.GetActionForButton (button.gameObject.name);
				button.gameObject.LinkEntity (entity, pool);
			}


			// Show Continue Hard Mode
			var restartButton = buttons.Where (x => x.gameObject.name.Contains("Restart Button")).Select (x => x.gameObject);
			var continueButton = buttons.Where (x => x.gameObject.name.Contains("Continue Button")).Select (x => x.gameObject);
			if (SerdarApplication.levelResults != null && SerdarApplication.levelResults.IsLevelSucceed == false) {
				restartButton.First ().SetActive (true);
				continueButton.First().SetActive (false);
				Debug.Log ("Game Over");

			} else {
				Debug.Log ("Game Succeed");
				restartButton.First().SetActive (false);
				continueButton.First().SetActive(true);
			}
				

			// Set the Data on Sound & Exit buttons
			//SerdarApplication.Global.currentSceneInformation = this;
			//SerdarApplication.Global.InitNewScene ();
		}

	}
}