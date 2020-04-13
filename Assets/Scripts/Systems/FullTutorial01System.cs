using EcsRx.Systems;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;

namespace Ahmetson.Serdar.Systems.FullTutorial {
	public class FullTutorial01System : IManualSystem {

		public IGroup TargetGroup { get { return new EmptyGroup(); } }

		public void StartSystem(IGroupAccessor group) {
			//SerdarApplication.sceneSwitcher.SwitchToNextScene (SerdarApplication.Global.currentSceneInformation.NextSceneId);
		}

		public void StopSystem(IGroupAccessor group) {
			Debug.Log ("Scene Stope");
		}
	}

}
