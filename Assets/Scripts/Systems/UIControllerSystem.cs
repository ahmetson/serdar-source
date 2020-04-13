using UnityEngine;
using UnityEngine.Events;
using EcsRx.Systems;
using EcsRx.Groups;
using Ahmetson.Serdar.Component;
using UniRx;
using System;
using EcsRx.Entities;
using EcsRx.Unity.Components;
using Zenject;
using Ahmetson.Serdar.Configuration;
using Ahmetson.Serdar.Events;
using EcsRx.Events;

namespace Ahmetson.Serdar.Systems.ApplicationSystem {
	public class UIControllerSystem : IReactToEntitySystem {

		public IGroup TargetGroup { get { return new GroupBuilder().WithComponent<UIInteractiveComponent>().WithComponent<ViewComponent>().Build();} }

		[Inject]
		UnityEngine.EventSystems.EventSystem eventSystem;
        [Inject]
        IEventSystem signalSystem;

		public UniRx.IObservable<IEntity> ReactToEntity(IEntity entity) {
			return Observable.EveryUpdate ().Where (x => {
				if (!Input.GetMouseButtonDown(0)) return false;
				if (null == eventSystem.currentSelectedGameObject) return false;
				if (eventSystem.currentSelectedGameObject.GetInstanceID() !=
					entity.GetComponent<ViewComponent>().View.GetInstanceID())
					return false;
				return true;
			}).Select (x => entity);
		}

		public void Execute(IEntity entity) {
			UIInteractiveComponent interactiveComponent = entity.GetComponent<UIInteractiveComponent> ();

			switch (interactiveComponent.Action) {
			case BUTTON_ACTION.NEW_GAME:
                Debug.Log("START A NEW GAME");
                //SerdarApplication.Global.NewGame ();
                //signalSystem.Publish(new SceneSwitchEvent(Enums.SCENE_TYPE.LEVEL, "Map", "Ahmetson.Serdar.Systems.LevelsSystem"));
                signalSystem.Publish(new SceneSwitchEvent(Enums.SCENE_TYPE.LEVEL, "001", "Ahmetson.Serdar.Systems.LevelSystem"));
                break;
			case BUTTON_ACTION.SKIP_SCENE:
				//SerdarApplication.sceneSwitcher.SwitchToNextScene ();
				break;
			case BUTTON_ACTION.CONTINUE:
                Debug.Log("CONTINUE");
				//SerdarApplication.sceneSwitcher.OpenMap ();
				break;
			default:
				Debug.Log ("Action to take: " + interactiveComponent.Action);
				break;
			}
		}
	}
}
