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
	public class LevelsSceneInformation : MonoBehaviour, ISceneInformation {


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


		void Start() {
			//if (null == SerdarApplication.Global) {
			//	SceneSwitcher sceneSwitcher = new SceneSwitcher ();
			//	sceneSwitcher.OpenInitialScene();	// Switch to Begin, to initialize the Application
			//	return;
			//}

			// Set the Data on Sound & Exit buttons
			//SerdarApplication.Global.currentSceneInformation = this;
			//SerdarApplication.Global.InitNewScene ();
		}

	}
}