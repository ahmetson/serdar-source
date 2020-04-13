using EcsRx.Systems;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;

namespace Ahmetson.Serdar.Systems.LevelCompletitionSystem {
	public class LevelCompletitionSystem : IManualSystem {

		public IGroup TargetGroup { get { return new EmptyGroup(); } }

		public void StartSystem(IGroupAccessor group) {
			//SerdarApplication.sceneSwitcher.SwitchToNextScene (SerdarApplication.Global.currentSceneInformation.NextSceneId);

			// Get Game progress
			//CurrentGameProgress currentProgress = SerdarApplication.Global.currentGameProgress.Value;

			// Continue to play
			if (SerdarApplication.levelResults == null || SerdarApplication.levelResults.IsLevelSucceed == true) {
				
			}
			//Debug.Log("Level Completed");
		}

		public void StopSystem(IGroupAccessor group) {
			Debug.Log ("Scene Stop!!!!!");
		}
	}

}
