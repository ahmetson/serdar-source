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
using Ahmetson.Serdar.Helpers;

namespace Ahmetson.Serdar {
	public class CutsceneIntroSceneInformation : MonoBehaviour, ISceneInformation {

        [Inject]
        private PoolManager PoolManager; 

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

		[HideInInspector]
		public Button[] buttons;



		void Start() {

			// Mark objects on Screen as Interactive
			buttons = GameObject.FindObjectsOfType<Button> ();
			IPool pool = PoolManager.GetPool();
			UIInteractiveHelper helper = new UIInteractiveHelper ();
			foreach (Button button in buttons) {
				IEntity entity = pool.CreateEntity ();
				entity.AddComponent<UIInteractiveComponent> ();
				UIInteractiveComponent interactiveComponent = entity.GetComponent<UIInteractiveComponent> ();
				interactiveComponent.Action = helper.GetActionForButton (button.gameObject.name);
				button.gameObject.LinkEntity (entity, pool);
			}


			// Set the Data on Sound & Exit buttons
			//SerdarApplication.Global.currentSceneInformation = this;
			//SerdarApplication.Global.InitNewScene ();
		}




	}
}