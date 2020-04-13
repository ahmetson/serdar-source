using EcsRx.Systems;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;

namespace Ahmetson.Serdar.Systems.LevelsSystem {
	public class LevelManagerSystem : IManualSystem {

		public IGroup TargetGroup { get { return new EmptyGroup(); } }

        public void StartSystem(IGroupAccessor group) {
            //SerdarApplication.sceneSwitcher.SwitchToNextScene (SerdarApplication.Global.currentSceneInformation.NextSceneId);
            Debug.Log("Manage the Scene");

            // Get Game progress
   //         CurrentGameProgress currentGameProgress = SerdarApplication.Global.currentGameProgress.Value;
   //         if (currentGameProgress.CurrentLevel < SerdarApplication.Global.gameProgress.MinLevel) {
   //             currentGameProgress.CurrentLevel = SerdarApplication.Global.gameProgress.MinLevel;
   //             SerdarApplication.Global.currentGameProgress.SetValueAndForceNotify(currentGameProgress);
   //             //SerdarApplication.sceneSwitcher.MoveOnGameFlow (currentGameProgress.CurrentLevel);
   //         } else if (currentGameProgress.CurrentLevel == SerdarApplication.Global.gameProgress.MaxLevel) { 
   //             //SerdarApplication.sceneSwitcher.EndGame ();
   //         } else {
			//	currentGameProgress.CurrentLevel++;
			//	SerdarApplication.Global.currentGameProgress.SetValueAndForceNotify (currentGameProgress);
			//	//SerdarApplication.sceneSwitcher.MoveOnGameFlow (currentGameProgress.CurrentLevel);
			//}
		}

		public void StopSystem(IGroupAccessor group) {
			Debug.Log ("Stop Scene Manager!!!!!");
		}
	}

}
