using EcsRx.Systems;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;
using Ahmetson.Serdar.Events;
using Zenject;
using EcsRx.Events;

namespace Ahmetson.Serdar.Systems.InitSystem
{
	public class InitSceneSystem : IManualSystem {

        [Inject]
        IEventSystem eventSystem;

		public IGroup TargetGroup { get { return new EmptyGroup(); } }

		public void StartSystem(IGroupAccessor group) {
            //SerdarApplication.sceneSwitcher.SwitchToHomeScene ();
            Debug.Log("Start Init System");
            eventSystem.Publish( new SceneSwitchEvent( Enums.SCENE_TYPE.MAIN_MENU, "Main Menu", "Ahmetson.Serdar.Systems.MainMenuSystem" ) );
        }

		public void StopSystem(IGroupAccessor group) {
		}
	}

}
