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
using EcsRx.Events;

namespace Ahmetson.Serdar {
	public class MainMenuSceneInformation : MonoBehaviour, ISceneInformation {


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
			
				

			// Select Buttons showing mode for new gamers or continuers
			//if (SerdarApplication.Global.currentGameProgress.Value.CurrentLevel >=
			//    SerdarApplication.Global.gameProgress.MinLevel &&
			//    SerdarApplication.Global.currentGameProgress.Value.CurrentLevel <=
			//    SerdarApplication.Global.gameProgress.MaxLevel) {
			//	ShowContinue ();
			//} else {
			//	ShowNewGame ();
			//}


			// Show the Hard Mode
			
			//if (SerdarApplication.Global.currentGameProgress.Value.HardModeEnabled) {
			//	hardModeToggleButton.First ().SetActive (true);
			//	hardModePurchaseButton.First().SetActive (false);
			//} else {
			//	hardModeToggleButton.First().SetActive (false);
			//	HardMode hardModeInformation = new HardMode ();
			//	if (SerdarApplication.Global.gameProgress.GetProgressByPercentage (SerdarApplication.Global.currentGameProgress.Value.CurrentLevel)
			//	    >= hardModeInformation.PurchaseButtonOnGameProgressByPercents) {
			//		hardModePurchaseButton.First().SetActive (true);
			//	} else {
			//		hardModePurchaseButton.First().SetActive (false);
			//	}
			//}

			// Set the Data on Sound & Exit buttons
			//SerdarApplication.Global.currentSceneInformation = this;
			//SerdarApplication.Global.InitNewScene ();
		}

		/// <summary>
		/// Continue Mode,	
		/// Show NEW GAME, CONTINUE buttons
		/// Hide PLAY button
		/// </summary>
		void ShowContinue() {
			Debug.Log ("Show Continue");
			foreach (Button button in buttons) {
				if (button.gameObject.name.Contains ("Continue Button") ||
				    button.gameObject.name.Contains ("New Game Button")) {
					button.gameObject.SetActive (true);
					continue;
				}
				if (button.gameObject.name.Contains ("Play Button")) {
					button.gameObject.SetActive (false);
				}
			}
		}

		void ShowNewGame() {
			foreach (Button button in buttons) {
 				if (button.gameObject.name.Contains ("Continue Button") ||
					button.gameObject.name.Contains ("New Game Button")) {
					button.gameObject.SetActive (false);
					continue;
				}
				if (button.gameObject.name.Contains ("Play Button")) {
					button.gameObject.SetActive (true);
				}
			}
		}

	}
}