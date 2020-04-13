using UnityEngine;
using EcsRx.Systems;
using UniRx;
using UniRx.Triggers;
using EcsRx.Entities;
using EcsRx.Groups;
using EcsRx.Unity.Components;
using Zenject;
using Ahmetson.Serdar;
using Ahmetson.Serdar.Events;
using Ahmetson.Serdar.Configuration;
using EcsRx.Events;

namespace Ahmetson.Serdar.Systems.ApplicationSystem {
	public class InteractivitySystem : IReactToEntitySystem {

        [Inject]
        IEventSystem eventSystem;

		public IGroup TargetGroup { get { return new GroupBuilder().WithComponent<RoadComponent>().WithComponent<ViewComponent>().Build();} }

		public IObservable<IEntity> ReactToEntity(IEntity entity) {

			return Observable.EveryUpdate ().Where ( x => {
				if ( !Input.GetMouseButtonDown ( 0 ) ) return false;
	
				ViewComponent viewComponent = entity.GetComponent<ViewComponent> ();
				if ( null != viewComponent.View ) {
					RaycastHit2D hitInfo = Physics2D.Raycast ( Camera.main.ScreenToWorldPoint ( Input.mousePosition ), Vector2.zero );

					if ( null == hitInfo.collider ) {
						return false;

					} 

					if ( hitInfo.collider.name != viewComponent.View.name ) {
						return false;
					}

					Road road = hitInfo.collider.GetComponent<Road>();

					if ( null == road ) {
						return false;
					}

					if ( false == road.IsIntarictiveRoad() ) {
						Debug.Log (" Road is not interactive ");
						return false;
					} 
	
					LevelStateEvent eventData = new LevelStateEvent ();
					eventData.state = LEVEL_STATE.SERDAR_WALK;
					eventData.road = road;
					eventSystem.Publish ( eventData );

				} 
				return true;
			}).Select ( x => entity );

		}

		public void Execute(IEntity entity) {
			
		}
	}
}
